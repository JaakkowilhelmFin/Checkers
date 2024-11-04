using System;
using System.Collections.Generic;

namespace checkers
{
    public class CheckersGame
    {
        public Piece[,] board = new Piece[8, 8];
        public int CurrentPlayer { get; private set; } = 1;  

        public CheckersGame()
        {
            InitializeBoard();
        }

        public void NextTurn()
        {
            CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;  
        }

        private void InitializeBoard()
        {
            
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        board[row, col] = new Piece(1);
                    }
                }
            }

           
            for (int row = 5; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        board[row, col] = new Piece(2);
                    }
                }
            }
        }

        public Piece[,] GetBoard()
        {
            return board;
        }

        public int CheckForWinner()
        {
            bool player1HasPieces = false;
            bool player2HasPieces = false;

            foreach (var piece in board)
            {
                if (piece != null)
                {
                    if (piece.Player == 1) player1HasPieces = true;
                    if (piece.Player == 2) player2HasPieces = true;
                }
            }

            if (!player1HasPieces) return 2;  // Player 2 wins if Player 1 has no pieces 
            if (!player2HasPieces) return 1;  // Player 1 wins if Player 2 has no pieces 

            return 0;  // No winner yet
        }
    }

    public class Piece
    {
        public int Player { get; private set; }
        public bool IsKing { get; set; }

        public Piece(int player)
        {
            Player = player;
            IsKing = false;
        }
    }
}
