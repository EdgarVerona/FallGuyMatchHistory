using FallGuyMatchHistory.Contracts;
using FallGuyMatchHistory.Engine;
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
	public partial class FrmMain : Form
	{
		TrackHistorySettings _settings;
		Dictionary<string, Participant> _participants;
		LogFileWatcher _watcher;
		BindingList<Show> _shows = new BindingList<Show>();

		public FrmMain()
		{
			InitializeComponent();

			_watcher = new LogFileWatcher();

			_watcher.OnError += Watcher_OnError;
			_watcher.OnNewLogFileDate += Watcher_OnNewLogFileDate;
			_watcher.OnRoundUpdate += Watcher_OnRoundUpdate;
			_watcher.OnShowUpdate += Watcher_OnShowUpdate;

			dgShows.SelectionChanged += DgShows_SelectionChanged;
			
			ReloadSettings();

			dgShows.DataSource = _shows;
		}

		private void DgShows_SelectionChanged(object sender, EventArgs e)
		{
			if (dgShows.SelectedRows.Count > 0)
			{
				var firstRow = dgShows.SelectedRows[0];
				var showSelected = firstRow.DataBoundItem as Show;

				dgDetails.DataSource = new BindingSource(showSelected.PlayerRanks, "");
				dgErrors.DataSource = new BindingSource(showSelected.Errors, "");
			}
			else
			{
				dgDetails.DataSource = null;
				dgErrors.DataSource = null;
			}
		}

		private void ReloadSettings()
		{
			_settings = TrackHistorySettings.GetCurrentOrDefault();

			try
			{
				_participants = _settings.GetParticipants();
			}
			catch (Exception ex)
			{
				_participants = new Dictionary<string, Participant>();
				MessageBox.Show(ex.Message);
			}
		}

		private void Watcher_OnShowUpdate(GamePhase phase, Show show)
		{
			if (this.InvokeRequired)
			{
				this.Invoke((Action<GamePhase, Show>)Watcher_OnShowUpdate, phase, show);
			}
			else
			{
				if (show != null)
				{
					if (!_shows.Any(existing => existing.StartTime.Equals(show.StartTime)))
					{
						_shows.Add(show);
					}
					else
					{
						dgShows.Refresh();
					}
				}
			}
		}

		private void Watcher_OnRoundUpdate(GamePhase phase, Show show, ShowRound round)
		{
			if (this.InvokeRequired)
			{
				this.Invoke((Action<GamePhase, Show, ShowRound>)Watcher_OnRoundUpdate, phase, show, round);
			}
			else
			{
				var firstRow = dgShows.SelectedRows[0];
				var showSelected = firstRow.DataBoundItem as Show;

				if (showSelected.StartTime.Equals(show.StartTime))
				{
					dgShows.Refresh();
					dgDetails.Refresh();
					dgErrors.Refresh();
				}
			}
		}

		private void Watcher_OnNewLogFileDate(DateTime logFileDate)
		{
			if (this.InvokeRequired)
			{
				this.Invoke((Action<DateTime>)Watcher_OnNewLogFileDate, logFileDate);
			}
			else
			{
				Console.WriteLine($"New Log File Date: {logFileDate.ToString()}");
			}
		}

		private void Watcher_OnError(string errorMessage)
		{
			if (this.InvokeRequired)
			{
				this.Invoke((Action<string>)Watcher_OnError, errorMessage);
			}
			else
			{
				MessageBox.Show($"ERROR: {errorMessage}");
			}
		}

		private async void btnPlay_Click(object sender, EventArgs e)
		{
			await ToggleProcessing(true);
		}

		private async void btnPause_Click(object sender, EventArgs e)
		{
			await ToggleProcessing(false);
		}

		private void btnParticipants_Click(object sender, EventArgs e)
		{
			// Nothing for now, no time to make a real editor
		}

		private async void btnSettings_Click(object sender, EventArgs e)
		{
			await ToggleProcessing(false);

			using (var frmSettings = new FrmSettings())
			{
				frmSettings.Initialize();

				frmSettings.ShowDialog(this);

				ReloadSettings();
			}
		}

		private async Task ToggleProcessing(bool enable)
		{
			btnPlay.Enabled = !enable;
			btnPause.Enabled = enable;
			progressBar.Visible = enable;
			
			if (enable)
			{
				progressBar.Value = 100;
				statusLabel.Text = "Parsing logs...";
				_watcher.Start(_settings, "Player.log");
			}
			else
			{
				progressBar.Value = 0;
				statusLabel.Text = "PAUSED: Press play to parse logs.";
				await _watcher.Stop();
			}
		}
	}
}
