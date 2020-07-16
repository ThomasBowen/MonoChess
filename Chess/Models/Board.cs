using MonoChess.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MonoChess.Models
{
    public class Board
    {
        public List<Piece> Pieces { get; set; }

        public Piece GetPieceAtLocation(int row, int column)
        {
            return Pieces.SingleOrDefault(p => p.Row == row && p.Column == column);
        }

        public void SetUpPieces()
        {
            Pieces.Clear();

            AddPieces(PieceColour.White);
            AddPieces(PieceColour.Black);
        }

        public List<Piece> GetPieces(PieceColour pieceColour)
        {
            return Pieces.Where(p => p.PieceColour == pieceColour).ToList();
        }

        public void MakeMove(Move move)
        {
            move.Piece.Move(move.Location);

            var kings = Pieces.OfType<King>().ToList();

            foreach (var king in kings)
            {
                king.SetCurrentlyInCheck();
            }
        }

        public Board Clone()
        {
            var clone = new Board();

            clone.Pieces = Pieces.Select(p => p.Clone(clone)).ToList();

            return clone;
        }

        private void AddPieces(PieceColour colour)
        {
            var mainRow = colour == PieceColour.Black ? 0 : 7;
            var pawnRow = colour == PieceColour.Black ? 1 : 6;

            Pieces.Add(new Rook(this, mainRow, 0, colour));
            Pieces.Add(new Knight(this, mainRow, 1, colour));
            Pieces.Add(new Bishop(this, mainRow, 2, colour));
            Pieces.Add(new Queen(this, mainRow, 3, colour));
            Pieces.Add(new King(this, mainRow, 4, colour));
            Pieces.Add(new Bishop(this, mainRow, 5, colour));
            Pieces.Add(new Knight(this, mainRow, 6, colour));
            Pieces.Add(new Rook(this, mainRow, 7, colour));

            for (var i = 0; i < 8; i++)
            {
                Pieces.Add(new Pawn(this, pawnRow, i, colour));
            }
        }
    }
}