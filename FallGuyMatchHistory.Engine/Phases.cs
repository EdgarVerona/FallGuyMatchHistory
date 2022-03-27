using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallGuyMatchHistory.Engine
{

    public enum GamePhase
    {
        NotInShow, // Can only go to ShowStartedNoRound
        ShowStartedNoRound, // Can only go to RoundLoaded
        RoundLoaded, // Can only go to RoundStarted, can process spawn messages
        RoundStarted, // Can only go to RoundEnded, can process success and failure messages
        RoundEnded // Can go to RoundLoaded or NotInShow
    }
}
