using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FallGuyMatchHistory.Contracts
{
	public class Show
	{
		public bool IsPrivateMatch { get; set; }

		public bool AreRanksFinalized { get; set; }

		public List<PlayerRank> PlayerRanks { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public List<ShowRound> Rounds { get; set; }

		public List<string> Errors { get; set; }

		[JsonIgnore]
		public string Description => $"{this.Rounds.Count} Rounds, {this.PlayerRanks.Count} Players";

		public Show()
		{
			this.PlayerRanks = new List<PlayerRank>();
			this.Rounds = new List<ShowRound>();
			this.StartTime = DateTime.MinValue;
			this.EndTime = DateTime.MinValue;
		}
	}

	public class PlayerRank
	{
		public int Rank { get; set; }

		public string Gamertag { get; set; }

		public string PlatformType { get; set; }

		public int RoundEliminated { get; set; }

		public DateTime ResultTime { get; set; }
	}

	public enum OutcomeReason
	{
		Explicit,
		ImpliedEndOfActivity
	}

	public class ShowRound
	{
		public int RoundNumber { get; set; }

		public string SceneName { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public Dictionary<int, RoundPlayer> PlayersByIdForRound { get; set; } = new();
	}

	public class RoundPlayer
	{
		public int PlayerIdForRound { get; set; } = -1;
		public string Gamertag { get; set; }
		public string PlatformType { get; set; }
		public bool IsSuccessful { get; set; } = false;
		public OutcomeReason OutcomeReason { get; set; } = OutcomeReason.ImpliedEndOfActivity;
		public DateTime ResultTime { get; set; }
	}
}
