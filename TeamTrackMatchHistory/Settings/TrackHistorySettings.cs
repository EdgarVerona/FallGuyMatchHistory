using CsvHelper;
using FallGuyMatchHistory.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TeamTrackMatchHistory.Settings
{
	public class TrackHistorySettings : HistorySettings
	{
		public const string SETTINGS_FILE_NAME = "settings.json";

		public string ParticipantsFilePath { get; set; }

		public Dictionary<string, Participant> GetParticipants()
		{
			var results = new Dictionary<string, Participant>();

			if (File.Exists(this.ParticipantsFilePath))
			{
				try
				{
					using (var reader = new StreamReader(this.ParticipantsFilePath))
					{
						using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
						{
							var records = csv.GetRecords<Participant>();

							results = records.ToDictionary(part => part.Gamertag);
						}
					}
				}
				catch (Exception)
				{
					throw new Exception($"Error reading participants file at {this.ParticipantsFilePath}.  Make sure it is a .csv, with two columns: Gamertag and Team, and a header row at the top declaring those columns.");
				}
			}
			else
			{
				throw new Exception($"Participants file not found at {this.ParticipantsFilePath}.  Please change the participant path in settings.");
			}

			return results;
		}

		internal void Save()
		{
			var settingsSerialized = JsonSerializer.Serialize(this);

			File.WriteAllText(SETTINGS_FILE_NAME, settingsSerialized);
		}

		public static TrackHistorySettings GetDefault()
		{
			return new TrackHistorySettings()
			{
				LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", "Mediatonic", "FallGuys_client"),
				MillisecondSameFrameThreshold = 100,
				ParticipantsFilePath = "participants.csv"
			};
		}

		public static TrackHistorySettings GetCurrentOrDefault()
		{
			var result = TrackHistorySettings.GetDefault();

			if (File.Exists(SETTINGS_FILE_NAME))
			{
				result = JsonSerializer.Deserialize<TrackHistorySettings>(File.ReadAllText(SETTINGS_FILE_NAME));
			}

			return result;
		}
	}
}
