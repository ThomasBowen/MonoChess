using MonoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoChess.Models
{
    public class Pawn : Piece
    {
        private bool _hasMoved = false;

        public override int Value => 1;

        public bool ToBePromoted
        {
            get
            {
                var farRow = PieceColour == PieceColour.White ? 0 : 7;

                return Row == farRow;
            }
        }

        public override List<Tuple<int, int>> PossibleMoves
        {
            get
            {
                var moves = new List<Tuple<int, int>>();

                var rowPlusOne = PieceColour == PieceColour.Black ? Row + 1 : Row - 1;
                var rowPlusTwo = PieceColour == PieceColour.Black ? Row + 2 : Row - 2;
                var isAtLimit = PieceColour == PieceColour.Black && Row == 7 || PieceColour == PieceColour.White && Row == 0;
                var existingPieceAhead = Board.Pieces.Any(p => p.Row == rowPlusOne && p.Column == Column);
                var existingPieceTwoAhead = Board.Pieces.Any(p => p.Row == rowPlusTwo && p.Column == Column);

                if (!isAtLimit && !existingPieceAhead) moves.Add(new Tuple<int, int>(rowPlusOne, Column));

                if (!_hasMoved && !existingPieceAhead && !existingPieceTwoAhead) moves.Add(new Tuple<int, int>(rowPlusTwo, Column));

                var canTakeLeft = Board.Pieces.Any(p => p.Row == rowPlusOne && p.Column == Column - 1 && p.PieceColour != PieceColour);
                var canTakeRight = Board.Pieces.Any(p => p.Row == rowPlusOne && p.Column == Column + 1 && p.PieceColour != PieceColour);

                if (canTakeLeft) moves.Add(new Tuple<int, int>(rowPlusOne, Column - 1));
                if (canTakeRight) moves.Add(new Tuple<int, int>(rowPlusOne, Column + 1));

                RemoveImpossibleMoves(moves);

                return moves;
            }
        }

        public Pawn(Board board, int row, int column, PieceColour pieceColour) : base(board, row, column, pieceColour) { }

        public override Piece Clone(Board board)
        {
            return new Pawn(board, Row, Column, PieceColour);
        }

        protected override void PostMove()
        {
            _hasMoved = true;
        }      
    }
}