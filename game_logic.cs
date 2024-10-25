using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class CheckersGame
    {
        public Piece[,] board = new Piece[8, 8];  

        public CheckersGame()
        {
            
            InitializeBoard();
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
