using System.IO;
using System.Text;

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

LineReader shamelessly stolen from https://github.com/ShootMe/FallGuysStats -
I'm guessing that particularly with my simplification, I could just use a StreamReader, but... meh
*/

namespace FallGuyMatchHistory.Engine
{
    public class LineReader
    {
        private byte[] buffer;
        private int bufferIndex, bufferSize;
        private Stream file;
        private StringBuilder currentLine;
        public long Position;
        public LineReader(Stream stream)
        {
            file = stream;
            buffer = new byte[1024];
            currentLine = new StringBuilder();
            Position = stream.Position;
        }

        public string ReadLine()
        {
            while (bufferIndex < bufferSize)
            {
                byte data = buffer[bufferIndex++];
                Position++;

                if (data == (byte)'\n' || data == (byte)'\r')
                {
                    if (data == '\r')
                    {
                        data = bufferIndex < buffer.Length ? buffer[bufferIndex] : (byte)0;
                        if (data == (byte)'\n')
                        {
                            bufferIndex++;
                            Position++;
                        }
                    }

                    string result = currentLine.ToString();
                    currentLine.Clear();
                    return result;
                }

                currentLine.Append((char)data);
            }

            while ((bufferSize = file.Read(buffer, 0, buffer.Length)) > 0)
            {
                bufferIndex = 0;
                while (bufferIndex < bufferSize)
                {
                    byte data = buffer[bufferIndex++];
                    Position++;

                    if (data == (byte)'\n' || data == (byte)'\r')
                    {
                        if (data == '\r')
                        {
                            data = bufferIndex < buffer.Length ? buffer[bufferIndex] : (byte)0;
                            if (data == (byte)'\n')
                            {
                                bufferIndex++;
                                Position++;
                            }
                        }

                        string result = currentLine.ToString();
                        currentLine.Clear();
                        return result;
                    }

                    currentLine.Append((char)data);
                }
            }

            if (currentLine.Length > 0)
            {
                string result = currentLine.ToString();
                currentLine.Clear();
                return result;
            }
            return null;
        }
    }
}