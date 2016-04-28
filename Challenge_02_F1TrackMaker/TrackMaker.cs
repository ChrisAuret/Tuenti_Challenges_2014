using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenge_02_F1TrackMaker
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class TrackPiece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Style { get; set; }
    }

    public class TrackMaker
    {
        private static int _x = 0;
        private static int _y = 0;

        public string MakeTrack(string input)
        {
            var circuit = new List<TrackPiece>();
            var direction = Direction.Right;

            foreach (char c in input)
            {
                var trackPiece = new TrackPiece { X = _x, Y = _y, Style = GetTrackPieceStyle(c, direction) };

                if (c == '-' || c == '#')
                {
                    switch (direction)
                    {
                        case Direction.Down:
                            _y++;
                            break;
                        case Direction.Up:
                            _y--;
                            break;
                        case Direction.Right:
                            _x++;
                            break;
                        case Direction.Left:
                            _x--;
                            break;
                    }
                }
                else if (c == '\\')
                {
                    trackPiece.Style = c;
                    if (direction == Direction.Down)
                    {
                        direction = Direction.Right;
                        _x++;
                    }
                    else if (direction == Direction.Left)
                    {
                        direction = Direction.Up;
                        _y--;
                    }
                    else if (direction == Direction.Right)
                    {
                        direction = Direction.Down;
                        _y++;
                    }
                    else if (direction == Direction.Up)
                    {
                        direction = Direction.Left;
                        _x--;
                    }
                }
                else if (c == '/')
                {
                    trackPiece.Style = c;
                    if (direction == Direction.Down)
                    {
                        direction = Direction.Left;
                        _x--;

                    }
                    else if (direction == Direction.Left)
                    {
                        direction = Direction.Down;
                        _y++;

                    }
                    else if (direction == Direction.Right)
                    {
                        direction = Direction.Up;
                        _y--;
                    }
                    else if (direction == Direction.Up)
                    {
                        direction = Direction.Right;
                        _x++;
                    }
                }

                circuit.Add(trackPiece);
            }

            // Adjust X and Y coords
            var minX = circuit.Min(trackPiece => trackPiece.X);
            var minY = circuit.Min(trackPiece => trackPiece.Y);
            circuit.ForEach(trackPiece => trackPiece.X = trackPiece.X - minX);
            circuit.ForEach(tracPiece => tracPiece.Y = tracPiece.Y - minY);

            var length = circuit.Max(trackPiece => trackPiece.X) + 1;
            var height = circuit.Max(trackPiece => trackPiece.Y) + 1;


            var output = "";

            for (var i = 0; i < height; i++)
            {
                var sb = new StringBuilder(new string(' ', length));
                var index = i;
                var trackPieces = circuit.Where(trackPiece => trackPiece.Y == index);

                foreach (var trackPiece in trackPieces)
                {
                    sb[trackPiece.X] = trackPiece.Style;
                }

                output += sb + Environment.NewLine;
            }

            return output;
        }

        private static char GetTrackPieceStyle(char c, Direction direction)
        {
            if (direction != Direction.Up && direction != Direction.Down) return c;
            if (c != '\\') return '|';
            return '-';
        }
    }
}
