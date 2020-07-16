using MonoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoChess.Models
{
    public class King : Piece
    {
        public override int Value => 99999999;

        public bool CurrentlyInCheck { get; set; }
        public bool CheckMate => CurrentlyInCheck && !Board.GetPieces(PieceColour).Any(p => p.PossibleMoves.Any());

        public override List<Tuple<int, int>> PossibleMoves
        {
            get
            {
                var moves = PossibleMovesMinusCheck;

                moves.RemoveAll(m => PositionIsCheck(m.Item1, m.Item2));

                return moves;
            }
        }

        public King(Board board, int row, int column, PieceColour pieceColour) : base(board, row, column, pieceColour) { }

        public override Piece Clone(Board board)
        {
            return new King(board, Row, Column, PieceColour);
        }

        public void SetCurrentlyInCheck()
        {
            CurrentlyInCheck = PositionIsCheck(Row, Column);
            DangerHighlight = CurrentlyInCheck;
        }

        public bool PositionIsCheck(int row, int column)
        {
            var oldRow = Row;
            var oldColumn = Column;

            var pieceTaken = Board.Pieces.SingleOrDefault(p => p.Row == row && p.Column == column && p.PieceColour != PieceColour);

            if (pieceTaken != null) Board.Pieces.Remove(pieceTaken);

            Row = row;
            Column = column;

            var isCheck = Board.Pieces.Any(piece =>
            {
                if (piece.PieceColour == PieceColour) return false;

                if (piece is King king)
                {
                    return king.PossibleMovesMinusCheck.Any(m => m.Item1 == row && m.Item2 == column);
                }
                else
                {
                    return piece.PossibleMoves.Any(m => m.Item1 == row && m.Item2 == column);
                }
            });

            Row = oldRow;
            Column = oldColumn;

            if (pieceTaken != null) Board.Pieces.Add(pieceTaken);

            return isCheck;
        }

        private List<Tuple<int, int>> PossibleMovesMinusCheck
        {
            get
            {
                var moves = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(Row + 1, Column),
                    new Tuple<int, int>(Row + 1, Column + 1),
                    new Tuple<int, int>(Row + 1, Column - 1),
                    new Tuple<int, int>(Row - 1, Column),
                    new Tuple<int, int>(Row - 1, Column - 1),
                    new Tuple<int, int>(Row - 1, Column + 1),
                    new Tuple<int, int>(Row, Column + 1),
                    new Tuple<int, int>(Row, Column - 1)
                };

                RemoveImpossibleMoves(moves);

                return moves;
            }
        }
    }
}