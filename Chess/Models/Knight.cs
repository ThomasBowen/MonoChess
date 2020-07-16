using MonoChess.Enums;
using System;
using System.Collections.Generic;

namespace MonoChess.Models
{
    public class Knight : Piece
    {
        public override int Value => 3;

        public override List<Tuple<int, int>> PossibleMoves
        {
            get
            {
                var moves = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(Row + 2, Column + 1),
                    new Tuple<int, int>(Row + 2, Column - 1),
                    new Tuple<int, int>(Row + 1, Column + 2),
                    new Tuple<int, int>(Row - 1, Column + 2),
                    new Tuple<int, int>(Row - 2, Column - 1),
                    new Tuple<int, int>(Row - 2, Column + 1),
                    new Tuple<int, int>(Row + 1, Column - 2),
                    new Tuple<int, int>(Row - 1, Column - 2)
                };

                RemoveImpossibleMoves(moves);

                return moves;
            }
        }

        public Knight(Board board, int row, int column, PieceColour pieceColour) : base(board, row, column, pieceColour) { }

        public override Piece Clone(Board board)
        {
            return new Knight(board, Row, Column, PieceColour);
        }
    }
}