using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FallGuyMatchHistory.Engine
{
    public class MatchLogParser
	{
        public void ParseLine(LogLine rawLine, LogParsingContext context)
        {
            int index = -1;

            // New Show - If we ever see this before the "Victory" dialog, we left prematurely!
            if (rawLine.TryIndexOf("[StateConnectToGame] We're connected to the server!", ref index))
            {
                context.SetGamePhase(GamePhase.ShowStartedNoRound, rawLine.Date);
			}
            // New Round loading - player spawning will occur between this and round start
            else if (rawLine.TryIndexOf("[StateGameLoading] Loading game level scene", ref index))
            {
                context.SetGamePhase(GamePhase.RoundLoaded, rawLine.Date);
                context.GetCurrentRoundData().SceneName = rawLine.SubstringBetweenFragments("[StateGameLoading] Loading game level scene ", " -");
            }
            // Spawn Message Handling - Spectator message, the one we seem to be guaranteed to get at the start of each round,
            // that gives us the Gamertag, Platform, and *per round* player ID.  It does NOT give us what appears to be
            // the overall Show-wide player ID.
            else if (rawLine.TryIndexOf("[CameraDirector] Adding Spectator target ", ref index))
            {
                int playerIdForRound = int.Parse(rawLine.SubstringAfter("playerID: "));
                string platformType = rawLine.SubstringBetweenFragments("Adding Spectator target ", "_");
                string gamertag = rawLine.SubstringBetweenFragments("_", " (");
                context.UpdatePlayerRoundSpawnData(playerIdForRound, platformType, gamertag);
			}
            // Spawn Message Handling - OnPlayerSpawned message, the one that we seem to not be guaranteed to get for non-local
            // players, but that gives us the relationship between the per-round player ID and what seems to be the show-wide player ID
            // (as well as the player's "friendly name" in game rather than their gamertag on platforms that support it)
            else if (rawLine.TryIndexOf("[CameraDirector] Adding Spectator target ", ref index))
            {
                // NOTE: Don't bother with this for now, no important data needs this.  But if it comes up later, this is how to grab it.
			}
            // Done loading players, round actually starting.
            else if (rawLine.TryIndexOf("[StateGameLoading] Starting the game.", ref index))
            {
                context.SetGamePhase(GamePhase.RoundStarted, rawLine.Date);
            }
            // Player Status update messages (eliminated or succeeded) - there's lots of interesting caveats about how and when we get these
            else if (rawLine.TryIndexOf("ClientGameManager::HandleServerPlayerProgress ", ref index))
            {
                int playerIdForRound = int.Parse(rawLine.SubstringBetweenFragments("PlayerId=", " is succeeded="));
                bool succeeded = bool.Parse(rawLine.SubstringAfter("is succeeded="));
                context.UpdatePlayerStatus(playerIdForRound, succeeded, rawLine.Date);
			}
            // Round ended.
            else if (rawLine.TryIndexOf("[ClientGameManager] Server notifying that the round is over.", ref index))
            {
                context.SetGamePhase(GamePhase.RoundEnded, rawLine.Date);
            }
            // Victory screen: means that the show is over and a winner was found.
            else if (rawLine.TryIndexOf("VictoryScene::", ref index))
            {
                int playerIdForRound = int.Parse(rawLine.SubstringBetweenFragments("winnerPlayerId:", " name:"));
                context.SetShowWinner(playerIdForRound, rawLine.Date);
                context.SetGamePhase(GamePhase.NotInShow, rawLine.Date);
            }
        }
	}
}