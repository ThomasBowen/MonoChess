using MonoChess.Enums;
using MonoChess.Helpers;
using System;
using System.Collections.Generic;

namespace MonoChess.Models
{
    public class Bishop : Piece
    {
        public override int Value => 3;

        public override List<Tuple<int, int>> PossibleMoves
        {
            get
            {
                var moves = MovesHelper.GetDiagonalMoves(Board, Row, Column, PieceColour);

                RemoveImpossibleMoves(moves);

                return moves;
            }
        }

        public Bishop(Board board, int row, int column, PieceColour pieceColour) : base(board, row, column, pieceColour) { }

        public override Piece Clone(Board board)
        {
            return new Bishop(board, Row, Column, PieceColour);
        }
    }
}