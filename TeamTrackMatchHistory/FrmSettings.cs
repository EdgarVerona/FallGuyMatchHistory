using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamTrackMatchHistory.Settings;

namespace TeamTrackMatchHistory
{
	public partial class FrmSettings : Form
	{
		public FrmSettings()
		{
			InitializeComponent();
		}

		internal void Initialize()
		{
			var settings = TrackHistorySettings.GetCurrentOrDefault();

			txtParticipantsPath.Text = settings.ParticipantsFilePath;
			txtPlayerLogPath.Text = settings.LogPath;
			txtThreshold.Text = settings.MillisecondSameFrameThreshold.ToString();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var newSettings = new TrackHistorySettings()
			{
				ParticipantsFilePath = txtParticipantsPath.Text,
				LogPath = txtPlayerLogPath.Text,
				MillisecondSameFrameThreshold = Int32.Parse(txtThreshold.Text)
			};

			newSettings.Save();
			this.Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = Application.StartupPath;
				var result = dialog.ShowDialog(this);

				if (result == DialogResult.OK || result == DialogResult.Yes)
				{
					txtParticipantsPath.Text = Path.GetPathRoot(dialog.FileName);
				}
			}
		}
	}
}
