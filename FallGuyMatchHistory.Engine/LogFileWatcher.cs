using FallGuyMatchHistory.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
The MIT License (MIT)

Copyright (c) 2020 DevilSquirrel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

LogFileWatcher heavily adapted from https://github.com/ShootMe/FallGuysStats, but the guts are still basically the same.
*/

namespace FallGuyMatchHistory.Engine
{
    public class LogFileWatcher
    {
        const int UpdateDelay = 500;

        private string _filePath;
        private string _prevFilePath;
        private List<LogLine> _lines = new List<LogLine>();
        private bool _running;
        private bool _stop;
        private Thread _watcher, _parser;

        public event Action<DateTime> OnNewLogFileDate;
        public event Action<string> OnError;
        public event Action<GamePhase, Show> OnShowUpdate;
        public event Action<GamePhase, Show, ShowRound> OnRoundUpdate;

        public void Start(string logDirectory, string fileName)
        {
            if (_running) { return; }

            _filePath = Path.Combine(logDirectory, fileName);
            _prevFilePath = Path.Combine(logDirectory, Path.GetFileNameWithoutExtension(fileName) + "-prev.log");
            _stop = false;
            _watcher = new Thread(ReadLogFile) { IsBackground = true };
            _watcher.Start();
            _parser = new Thread(ParseLines) { IsBackground = true };
            _parser.Start();
        }

        public async Task Stop()
        {
            _stop = true;
            while (_running || _watcher == null || _watcher.ThreadState == ThreadState.Unstarted)
            {
                await Task.Delay(50);
            }
            _lines = new List<LogLine>();
            await Task.Factory.StartNew(() => _watcher?.Join());
            await Task.Factory.StartNew(() => _parser?.Join());
        }

        private void ReadLogFile()
        {
            _running = true;
            List<LogLine> tempLines = new List<LogLine>();
            DateTime lastDate = DateTime.MinValue;
            bool completed = false;
            string currentFilePath = _prevFilePath;
            long offset = 0;
            while (!_stop)
            {
                try
                {
                    if (File.Exists(currentFilePath))
                    {
                        using (FileStream fs = new FileStream(currentFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            tempLines.Clear();

                            if (fs.Length > offset)
                            {
                                fs.Seek(offset, SeekOrigin.Begin);

                                LineReader sr = new LineReader(fs);
                                string line;
                                DateTime currentDate = lastDate;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    LogLine logLine = new LogLine(line, sr.Position);

                                    if (logLine.IsValid)
                                    {
                                        int index;
                                        if ((index = line.IndexOf("[GlobalGameStateClient].PreStart called at ")) > 0)
                                        {
                                            currentDate = DateTime.SpecifyKind(DateTime.Parse(line.Substring(index + 43, 19)), DateTimeKind.Utc);
                                            OnNewLogFileDate?.Invoke(currentDate);
                                        }

                                        if (currentDate != DateTime.MinValue)
                                        {
                                            if (currentDate.TimeOfDay.TotalSeconds - logLine.Time.TotalSeconds > 60000)
                                            {
                                                currentDate = currentDate.AddDays(1);
                                            }
                                            currentDate = currentDate.AddSeconds(logLine.Time.TotalSeconds - currentDate.TimeOfDay.TotalSeconds);
                                            logLine.Date = currentDate;
                                        }

                                        tempLines.Add(logLine);
                                    }
                                    offset = sr.Position;
                                }
                            }
                            else if (offset > fs.Length)
                            {
                                offset = 0;
                            }
                        }
                    }

                    if (tempLines.Count > 0)
                    {
                        lock (_lines)
                        {
                            _lines.AddRange(tempLines);
                        }
                    }

                    if (!completed)
                    {
                        completed = true;
                        offset = 0;
                        currentFilePath = _filePath;
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex.ToString());
                }
                Thread.Sleep(UpdateDelay);
            }
            _running = false;
        }

        private void ParseLines()
        {
            LogParsingContext context = new LogParsingContext();
			context.Error += Context_Error;
			context.ShowUpdate += Context_ShowUpdate;
			context.RoundUpdate += Context_RoundUpdate;
            MatchLogParser parser = new MatchLogParser();

            while (!_stop)
            {
                try
                {
                    lock (_lines)
                    {
                        foreach (var line in _lines)
                        {
                            parser.ParseLine(line, context);
                        }

                        _lines.Clear();
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex.ToString());
                }
                Thread.Sleep(UpdateDelay);
            }
        }

		private void Context_RoundUpdate(GamePhase phase, Show show, ShowRound round)
		{
            OnRoundUpdate?.Invoke(phase, show, round);
		}

		private void Context_ShowUpdate(GamePhase phase, Show show)
		{
            OnShowUpdate?.Invoke(phase, show);
        }

		private void Context_Error(string errorMessage)
		{
            OnError?.Invoke(errorMessage);
		}
	}
}
