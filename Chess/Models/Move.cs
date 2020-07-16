using System;

namespace MonoChess.Models
{
    public class Move
    {
        public Piece Piece { get; set; }
        public Tuple<int, int> Location { get; set; }
    }
}