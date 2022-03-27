using FallGuyMatchHistory.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallGuyMatchHistory.Engine
{
	public class LogParsingContext
	{
		public const int MILLISECOND_SAME_FRAME_THRESHOLD = 250;

		public event Action<GamePhase, Show> ShowUpdate;
		public event Action<GamePhase, Show, ShowRound> RoundUpdate;
		public event Action<string> Error;

		private Show _currentShow = null;
		private ShowRound _currentRound = null;

		private GamePhase _currentPhase = GamePhase.NotInShow;

		private HistorySettings _settings;

		public LogParsingContext(HistorySettings settings)
		{
			_settings = settings;
		}

		public void SetGamePhase(GamePhase newPhase, DateTime date)
		{
			switch (newPhase)
			{
				case GamePhase.NotInShow:
					_currentShow.EndTime = date;
					this.ShowUpdate?.Invoke(newPhase, _currentShow);
					_currentShow = null;
					_currentRound = null;
					break;
				case GamePhase.ShowStartedNoRound:
					_currentShow = new Show();
					_currentShow.StartTime = date;
					this.ShowUpdate?.Invoke(newPhase, _currentShow);
					break;
				case GamePhase.RoundLoaded:
					_currentRound = new ShowRound();
					_currentShow.Rounds.Add(_currentRound);
					_currentRound.RoundNumber = _currentShow.Rounds.Count;
					this.RoundUpdate?.Invoke(newPhase, _currentShow, _currentRound);
					break;
				case GamePhase.RoundStarted:
					_currentRound.StartTime = date;
					this.RoundUpdate?.Invoke(newPhase, _currentShow, _currentRound);
					break;
				case GamePhase.RoundEnded:
					_currentRound.EndTime = date;
					this.ComputeRoundRankings();
					this.RoundUpdate?.Invoke(newPhase, _currentShow, _currentRound);
					_currentRound = null;
					break;
			}

			_currentPhase = newPhase;
		}

		public Show GetCurrentShowData()
		{
			return _currentShow;
		}

		public ShowRound GetCurrentRoundData()
		{
			return _currentRound;
		}

		public void UpdatePlayerRoundSpawnData(
			int playerIdForRound,
			string platformType,
			string gamertag)
		{
			if (_currentPhase != GamePhase.RoundLoaded)
			{
				ThrowError($"ERROR: Player [{playerIdForRound}/{platformType}/{gamertag}] found spawn data, but not in RoundLoaded phase!");
			}

			if (_currentRound != null)
			{
				if (!_currentRound.PlayersByIdForRound.TryGetValue(playerIdForRound, out RoundPlayer roundPlayer))
				{
					roundPlayer = new RoundPlayer()
					{
						PlayerIdForRound = playerIdForRound
					};
					_currentRound.PlayersByIdForRound.Add(playerIdForRound, roundPlayer);
				}

				roundPlayer.Gamertag = gamertag;
				roundPlayer.PlatformType = platformType;
			}
		}

		private void ThrowError(string error)
		{
			_currentShow.Errors.Add(error);
			Error?.Invoke(error);
		}

		public void UpdatePlayerShowIdentifier(
			int playerIdForRound,
			int playerShowIdentifier)
		{
			// Only do this if we end up needing it!  For now, there's no useful data that requires the show identifier
			// (the one in square brackets such as the "7" in "name=FallGuy [7] Fluffy Flying Burger (win) ID=34 was spawned"
			// That identifier at least *appears* to be consistent between Rounds in the Show, but you don't always get the
			// [StateGameLoading] OnPlayerSpawned log message that associates it with the current round identifier, which means
			// if we don't get that at least once we can't guarantee that we will know the more important info for the player,
			// like if they won/lost, which is keyed on the *per round* PlayerID (ID=34 in the example above)
			// FallGuyStats seems to run on the assumption that they'll get this, because it seems like you always do for the current
			// player - but I've seen it not be logged for remote players.
			throw new NotImplementedException("Didn't think we'd need this, why are we calling it?  Do something similar to UpdatePlayerRoundSpawnData, but we shouldn't need it unless logging changed.");
		}

		public void UpdatePlayerStatus(
			int playerIdForRound, 
			bool succeeded, 
			DateTime date)
		{
			if (_currentPhase != GamePhase.RoundStarted)
			{
				ThrowError($"ERROR: Player [{playerIdForRound}] found Status Data, but not in RoundStarted phase!");
			}

			if (_currentRound != null)
			{
				if (_currentRound.PlayersByIdForRound.TryGetValue(playerIdForRound, out RoundPlayer roundPlayer))
				{
					roundPlayer.IsSuccessful = succeeded;
					roundPlayer.ResultTime = date;
					roundPlayer.OutcomeReason = OutcomeReason.Explicit;

					// If eliminated explicitly, we can add them to the player ranks immediately as we know when and in what order
					// they got eliminated (right as of this timestamp)
					if (!succeeded)
					{
						_currentShow.PlayerRanks.Add(new PlayerRank()
						{
							Gamertag = roundPlayer.Gamertag,
							PlatformType = roundPlayer.PlatformType,
							Rank = -1,
							ResultTime = roundPlayer.ResultTime,
							RoundEliminated = _currentRound.RoundNumber
						});
					}
				}
			}
		}

		public void SetShowWinner(
			int playerIdForRound,
			DateTime date)
		{
			if (_currentPhase != GamePhase.RoundEnded)
			{
				ThrowError($"ERROR: Player [{playerIdForRound}] won the match, but we're not in RoundEnded phase!");
			}

			// By this point, the final round should have been computed, and everyone other than the winner should have been either implicitly
			// or explicitly added to the show's PlayerRanks as losers.  And this person was already marked as the winner.  Add this one as the winner.
			var finalRound = _currentShow.Rounds.Last();
			if (finalRound != null)
			{
				if (finalRound.PlayersByIdForRound.TryGetValue(playerIdForRound, out RoundPlayer roundPlayer))
				{
					// Add the winner.
					List<PlayerRank> finalRanks = new List<PlayerRank>();
					finalRanks.Add(new PlayerRank()
						{
							Gamertag = roundPlayer.Gamertag,
							PlatformType = roundPlayer.PlatformType,
							Rank = 1,
							ResultTime = roundPlayer.ResultTime,
							RoundEliminated = -1
						});

					// Now organize the ranks and set ties etc... for the losers.
					int currentRank = 1;
					DateTime lastRankTime = DateTime.UnixEpoch;

					_currentShow.PlayerRanks.Reverse();

					foreach (var loserRank in _currentShow.PlayerRanks)
					{
						// To account for when many people get eliminated at the same time or client uncertainty,
						// consider two players to have the same rank if within a certain tolerance.
						if (Math.Abs(loserRank.ResultTime.Subtract(lastRankTime).TotalMilliseconds) >= _settings.MillisecondSameFrameThreshold)
						{
							currentRank++;
							// If this loser wasn't within the threshold of the first streak of losers we saw in this time range, then
							// consider any future loser blocks to be based off of this loser's time.  Loser.
							lastRankTime = loserRank.ResultTime;
						}

						loserRank.Rank = currentRank;
						finalRanks.Add(loserRank);
					}

					_currentShow.PlayerRanks = finalRanks;
					_currentShow.AreRanksFinalized = true;
				}
			}
		}

		private void ComputeRoundRankings()
		{
			// Find any players implicitly eliminated by the end of the activity and add them at once to the
			// player ranks of the show, with the same timestamp (end of activity)
			foreach (var roundPlayer in _currentRound.PlayersByIdForRound.Values)
			{
				if (!roundPlayer.IsSuccessful && roundPlayer.OutcomeReason == OutcomeReason.ImpliedEndOfActivity)
				{
					_currentShow.PlayerRanks.Add(new PlayerRank()
					{
						Gamertag = roundPlayer.Gamertag,
						PlatformType = roundPlayer.PlatformType,
						Rank = -1,
						ResultTime = _currentRound.EndTime,
						RoundEliminated = _currentRound.RoundNumber
					});
				}
			}
		}
	}
}
