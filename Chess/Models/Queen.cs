using MonoChess.Enums;
using MonoChess.Helpers;
using System;
using System.Collections.Generic;

namespace MonoChess.Models
{
    public class Queen : Piece
    {
        public override int Value => 9;

        public override List<Tuple<int, int>> PossibleMoves
        {
            get
            {
                var moves = MovesHelper.GetOrthogonalMoves(Board, Row, Column, PieceColour);
                moves.AddRange(MovesHelper.GetDiagonalMoves(Board, Row, Column, PieceColour));                

                RemoveImpossibleMoves(moves);

                return moves;
            }
        }
        public Queen(Board board, int row, int column, PieceColour pieceColour) : base(board, row, column, pieceColour) { }

        public override Piece Clone(Board board)
        {
            return new Queen(board, Row, Column, PieceColour);
        }
    }
}