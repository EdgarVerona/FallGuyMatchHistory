using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallGuyMatchHistory.Engine
{
	/*
	Observations about lifetime of game/players according to logs, and some interesting caveats and issues to work around:

	(SHOW START:) [StateConnectToGame] We're connected to the server!
	...
		(ROUND LOAD:) [StateGameLoading] Loading game level scene

			...
			(ROUND LOAD END:) [StateGameLoading] Finished loading game level

			...
			(SPAWN MESSAGES:) 
				19:17:36.350: [CameraDirector] Adding Spectator target win_USERNAME (win) with Party ID: PARTY_ID  Squad ID: 0 and playerID: 34
				!!! NOT GUARANTEED! 19:17:36.351: [StateGameLoading] OnPlayerSpawned - name=FallGuy [7] SILLY NAME (win) ID=34 was spawned (NOTE: IT MAY NOT ALWAYS HAVE ID!)
				19:17:36.351: [ClientGameManager] Finalising spawn for player FallGuy [7] Fluffy Flying Burger (win) (FG.Common.MPGNetObject)

				**** CONCLUSIONS ****
					- Because we can't guarantee we'll get the second message, we can neither guarantee that we'll get what appears to be the player's "show-wide" ID (7 in this example) nor their SILLY NAME.
					- We lucked out however, because all the messages we care about use the "PlayerId" which changes per round but we can at least associate with their gamertag due to log message #1 above.

			...
			(ROUND START:) [StateGameLoading] Starting the game.

				...
				(EVENTUALLY SUCCESS OR FAILURE MESSAGES:)
					19:19:10.069: [ClientGameManager] Handling unspawn for player FallGuy [4] SILLY NAME (win) (FG.Common.MPGNetObject)
					19:19:10.069: ClientGameManager::HandleServerPlayerProgress PlayerId=35 is succeeded=True
					19:19:10.069: [GameRules] marking player with playerId 35 as successful
					19:19:10.071: [ClientGameSession] NumPlayersAchievingObjective=1
				**** NOTES ****
					- If a player neither gets succeeded=true or false, it was a round where after the timer expires everyone who didn't meet certain qualifications (being on the winning team, crossing the finish line) fails at the same time
					- If a player gets succeeded=false, it was an elimination round and they were eliminated
					- If it's a survival round, everyone gets "succeeded=True" that survived at the end
						- They seem to get added in the same microsecond (19:22:18.876) but if we care about that we should put in a buffer
					- For 1v1 or team games, there are scores: but they're using the [#] identifier which we can't guarantee got logged alongside the player ID at any point
				**** CONCLUSIONS ****
					- We can determine order of completion for "cross the finish line" rounds
					- We can determine order of elimination for "elimination" rounds
					- We cannot determine order for:
						- Team or 1v1 matches (they all get reported at once at the end, no way to know specifically who did better than who else in a guaranteed way)
						- Survival matches (again, they all get reported at end)
						- Losers in Gauntlet/cross the finish line matches (implied loss at end, never actually logged!)
					- Because of this, the best we can probably do to determine rank is have some tolerance where if players die within that range of each other, they are considered tied.
						- Probably also good because I have no clue if the time a death message is logged in any way resembles the authoritative moment of their death, so some fudge factor is probably reasonable.

			... EVENTUALLY END OF ROUND MESSAGE
				19:20:25.799: [GlobalGameStateClient] Storing ticket for admission to stage 2 of episode (EPISODE ID) as a Player. Ticket sig=(SIGNATURE)
				19:20:25.799: SquadManager::GetSquadScore squadId0 not found return 0
				19:20:25.800: [ClientGameManager] Server notifying that the round is over.
				19:20:25.800: [GameSession] Changing state from Playing to GameOver
				19:20:25.801: [ClientGameSession] NumPlayersAchievingObjective=28
				19:20:28.548: [ClientGameSession].SwitchToResultsState, replay is disabled, going to results
				19:20:28.548: [GameSession] Changing state from GameOver to Results

		...
		(EVENTUALLY WHEN THE GAME FULLY ENDS:)
			19:27:06.071: VictoryScene::winnerPlayerId:85 name:ps4_Username squadId:0 teamId:-1
	*/
	public enum GamePhase
    {
        NotInShow, // Can only go to ShowStartedNoRound
        ShowStartedNoRound, // Can only go to RoundLoaded
        RoundLoaded, // Can only go to RoundStarted, can process spawn messages
        RoundStarted, // Can only go to RoundEnded, can process success and failure messages
        RoundEnded // Can go to RoundLoaded or NotInShow
    }
}
