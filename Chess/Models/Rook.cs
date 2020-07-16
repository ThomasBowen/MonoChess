using MonoChess.Enums;
using MonoChess.Helpers;
using System;
using System.Collections.Generic;

namespace MonoChess.Models
{
    public class Rook : Piece
    {
        public override int Value => 5;

        public override List<Tuple<int, int>> PossibleMoves
        {
            get
            {
                var moves = MovesHelper.GetOrthogonalMoves(Board, Row, Column, PieceColour);

                RemoveImpossibleMoves(moves);

                return moves;
            }
        }
        public Rook(Board board, int row, int column, PieceColour pieceColour) : base(board, row, column, pieceColour) { }

        public override Piece Clone(Board board)
        {
            return new Rook(board, Row, Column, PieceColour);
        }
    }
}