using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallGuyMatchHistory.Contracts
{
	public class HistorySettings
	{
		public string LogPath { get; set; }

		public int MillisecondSameFrameThreshold { get; set; }
	}
}
