using FallGuyMatchHistory.Contracts;
using FallGuyMatchHistory.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleMatchHistory
{
	class Program
	{
		static async Task Main(string[] args)
		{
			LogFileWatcher watcher = new LogFileWatcher();

			watcher.OnError += Watcher_OnError;
			watcher.OnNewLogFileDate += Watcher_OnNewLogFileDate;
			watcher.OnRoundUpdate += Watcher_OnRoundUpdate;
			watcher.OnShowUpdate += Watcher_OnShowUpdate;

			HistorySettings settings = new HistorySettings()
			{
				LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", "Mediatonic", "FallGuys_client"),
				MillisecondSameFrameThreshold = 100
			};

			watcher.Start(settings, "Player.log");

			while (true)
			{
				await Task.Delay(1000);
			}
		}

		static List<Show> _shows = new List<Show>();

		private static void Watcher_OnShowUpdate(GamePhase phase, Show show)
		{
			if (show != null)
			{
				switch (phase)
				{
					case GamePhase.NotInShow:
						Console.WriteLine($"Show Ended: {show.PlayerRanks.Count} players, Players in first round: {show.Rounds.First().PlayersByIdForRound.Count}, Winner: {show.PlayerRanks.First().Gamertag}");
						foreach (var rank in show.PlayerRanks)
						{
							Console.WriteLine($"\t{rank.Rank}, {rank.Gamertag} (Eliminated in Round {rank.RoundEliminated})");
						}
						_shows.Add(show);
						break;
					case GamePhase.ShowStartedNoRound:
						Console.WriteLine($"Show Started!");
						break;
				}
			}
		}

		private static void Watcher_OnRoundUpdate(GamePhase phase, Show show, ShowRound round)
		{
			Console.WriteLine($"\tRound Update: {phase.ToString()}, {round.RoundNumber}");

			if (phase == GamePhase.RoundEnded)
			{
				foreach (var player in round.PlayersByIdForRound.Values)
				{
					if (!player.IsSuccessful)
					{
						Console.WriteLine($"\t\tEliminated: {player.Gamertag}");
					}
				}
			}
		}

		private static void Watcher_OnNewLogFileDate(DateTime logFileDate)
		{
			Console.WriteLine($"New Log File Date: {logFileDate.ToString()}");
		}

		private static void Watcher_OnError(string errorMessage)
		{
			Console.WriteLine($"ERROR: {errorMessage}");
		}
	}
}
