using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoChess.Models
{
    public abstract class Piece
    {
        public Board Board;

        public abstract int Value { get; }

        public abstract List<Tuple<int, int>> PossibleMoves { get; }

        public Rectangle Area
        {
            get
            {
                var squareSize = Engine.Instance.SquareSize;
                var pieceSize = Engine.Instance.PieceSize;

                var y = (Row * squareSize) + (squareSize / 4);
                var x = (Column * squareSize) + (squareSize / 4);

                return new Rectangle(x, y, pieceSize, pieceSize);
            }
        }

        public PieceColour PieceColour { get; set; }
        public Texture2D Texture { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Selected { get; set; }
        public bool DangerHighlight { get; set; }

        public Piece(Board board, int row, int column, PieceColour pieceColour)
        {
            Board = board;

            Row = row;
            Column = column;

            PieceColour = pieceColour;

            Texture = TextureManager.GetTexture($"Content/Sprites/{pieceColour.ToString().ToLower()}-{GetType().Name.ToLower()}.png");
        }

        public void Move(Tuple<int, int> move)
        {
            var row = move.Item1;
            var column = move.Item2;

            if (!PossibleMoves.Any(m => m.Item1 == row && m.Item2 == column)) throw new InvalidOperationException();

            var pieceToTake = Board.GetPieceAtLocation(row, column);

            if (pieceToTake != null) Board.Pieces.Remove(pieceToTake);

            Row = row;
            Column = column;

            PostMove();
        }

        public abstract Piece Clone(Board board);

        protected virtual void PostMove() { }

        protected void RemoveImpossibleMoves(List<Tuple<int, int>> moves)
        {
            if (GetType() != typeof(King))
            {
                var king = Board.Pieces.OfType<King>().Single(k => k.PieceColour == PieceColour);

                if (king.CurrentlyInCheck)
                {
                    // TODO: can you take the piece putting the king in check
                    moves.Clear();
                    return;
                }
            }

            moves.RemoveAll(m => m.Item1 == Row && m.Item2 == Column);

            moves.RemoveAll(m => m.Item1 > 7 || m.Item1 < 0 || m.Item2 > 7 || m.Item2 < 0);

            moves.RemoveAll(m =>
            {
                var existing = Board.GetPieceAtLocation(m.Item1, m.Item2);

                return existing?.PieceColour == PieceColour;
            });
        }
    }
}