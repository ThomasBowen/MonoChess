using MonoChess.Enums;
using MonoChess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoChess.Helpers
{
    public static class MovesHelper
    {
        public static List<Tuple<int, int>> GetOrthogonalMoves(Board board, int row, int column, PieceColour pieceColour)
        {
            var moves = new List<Tuple<int, int>>();

            var rowToAdd = row + 1;
            Tuple<int, int> lastMove = null;

            while (rowToAdd < 8)
            {
                var move = new Tuple<int, int>(rowToAdd, column);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                rowToAdd++;
            }

            rowToAdd = row - 1;
            lastMove = null;

            while (rowToAdd > -1)
            {
                var move = new Tuple<int, int>(rowToAdd, column);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                rowToAdd--;
            }

            var columnToAdd = column + 1;
            lastMove = null;

            while (columnToAdd < 8)
            {
                var move = new Tuple<int, int>(row, columnToAdd);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                columnToAdd++;
            }

            columnToAdd = column - 1;
            lastMove = null;

            while (columnToAdd > -1)
            {
                var move = new Tuple<int, int>(row, columnToAdd);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                columnToAdd--;
            }

            return moves;
        }

        public static List<Tuple<int, int>> GetDiagonalMoves(Board board, int row, int column, PieceColour pieceColour)
        {
            var moves = new List<Tuple<int, int>>();

            var rowToAdd = row + 1;
            var columnToAdd = column + 1;
            Tuple<int, int> lastMove = null;

            while (rowToAdd < 8 && columnToAdd < 8)
            {
                var move = new Tuple<int, int>(rowToAdd, columnToAdd);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                rowToAdd++;
                columnToAdd++;
            }

            rowToAdd = row - 1;
            columnToAdd = column + 1;
            lastMove = null;

            while (rowToAdd > -1 && columnToAdd < 8)
            {
                var move = new Tuple<int, int>(rowToAdd, columnToAdd);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                rowToAdd--;
                columnToAdd++;
            }

            rowToAdd = row - 1;
            columnToAdd = column - 1;
            lastMove = null;

            while (rowToAdd > -1 && columnToAdd > -1)
            {
                var move = new Tuple<int, int>(rowToAdd, columnToAdd);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                rowToAdd--;
                columnToAdd--;
            }

            rowToAdd = row + 1;
            columnToAdd = column - 1;
            lastMove = null;

            while (rowToAdd < 8 && columnToAdd > -1)
            {
                var move = new Tuple<int, int>(rowToAdd, columnToAdd);

                if (BlockedByPiece(board, move, lastMove, pieceColour)) break;

                moves.Add(move);
                lastMove = move;

                rowToAdd++;
                columnToAdd--;
            }

            return moves;
        }

        private static bool BlockedByPiece(Board board, Tuple<int, int> move, Tuple<int, int> lastMove, PieceColour pieceColour)
        {
            if (lastMove != null && board.Pieces.Any(p => p.Row == lastMove.Item1 && p.Column == lastMove.Item2 && p.PieceColour != pieceColour))
            {
                return true;
            }

            return board.Pieces.Any(p => p.Row == move.Item1 && p.Column == move.Item2 && p.PieceColour == pieceColour);
        }
    }
}