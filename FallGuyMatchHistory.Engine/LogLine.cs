using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallGuyMatchHistory.Engine
{
    public class LogLine
    {
        public TimeSpan Time { get; } = TimeSpan.Zero;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public string Line { get; set; }
        public bool IsValid { get; set; }
        public long Offset { get; set; }

        public LogLine(string line, long offset)
        {
            Offset = offset;
            Line = line;
            IsValid = line.IndexOf(':') == 2 && line.IndexOf(':', 3) == 5 && line.IndexOf(':', 6) == 12;
            if (IsValid)
            {
                Time = TimeSpan.Parse(line.Substring(0, 12));
            }
        }

        public int IndexOf(string fragment)
        {
            return this.Line.IndexOf(fragment, StringComparison.OrdinalIgnoreCase);
        }

        public int IndexAfter(string fragment)
        {
            int indexOfFragment = this.IndexOf(fragment);

            if (indexOfFragment >= 0)
            {
                indexOfFragment += fragment.Length;
			}

            return indexOfFragment;
		}

        public int LengthBetweenFragments(string fragment, string laterFragment)
        {
            int afterFirstIndex = this.IndexAfter(fragment);
            int secondIndex = this.Line.IndexOf(laterFragment, afterFirstIndex);

            return secondIndex >= 0 && afterFirstIndex >= 0
                ? secondIndex - afterFirstIndex
                : -1;
		}

        public string SubstringBetweenFragments(string fragment, string laterFragment)
        {
            int lengthBetweenFragments = this.LengthBetweenFragments(fragment, laterFragment);

            if (lengthBetweenFragments > 0)
            {
                return this.Line.Substring(this.IndexAfter(fragment), lengthBetweenFragments);
            }
            else
            {
                return string.Empty;
			}
        }

        public string SubstringAfter(string fragment)
        {
            int index = this.IndexAfter(fragment);

            if (index >= 0)
            {
                return this.Line.Substring(index);
            }
            else
            {
                return string.Empty;
			}
        }

        public bool TryIndexOf(string fragment, ref int index)
        {
            index = this.Line.IndexOf(fragment, StringComparison.OrdinalIgnoreCase);

            return index >= 0;
        }

        public override string ToString()
        {
            return $"{Time}: {Line} ({Offset})";
        }
    }
}
