using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{

    public partial class Form1 : Form
    {
        private const int BoardSize = 8;  // 8x8 checkers board
        private const int CellSize = 60;  // Size of each cell in pixels
        private Button[,] boardButtons = new Button[BoardSize, BoardSize];
        private CheckersGame game;

        public Form1()
        {
            InitializeComponent();
            InitializeBoard();
            game = new CheckersGame();  // Assuming this is part of your logic
            RenderBoard();
        }

        private void InitializeBoard()
        {
            this.ClientSize = new Size(BoardSize * CellSize, BoardSize * CellSize);  // Set the size of the form

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(CellSize, CellSize);
                    btn.Location = new Point(col * CellSize, row * CellSize);
                    btn.FlatStyle = FlatStyle.Flat;

                    // Alternate colors for the checkerboard
                    if ((row + col) % 2 == 0)
                    {
                        btn.BackColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.Gray;
                        btn.Click += OnBoardButtonClick;
                    }

                    boardButtons[row, col] = btn;
                    this.Controls.Add(btn);
                }
            }
        }

        private void OnBoardButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point boardPos = GetButtonPosition(clickedButton);

            // Handle the player's move logic (move selection, piece movement, etc.)
            HandleMove(boardPos);
        }

        private Point GetButtonPosition(Button button)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (boardButtons[row, col] == button)
                    {
                        return new Point(row, col);
                    }
                }
            }
            return Point.Empty;
        }

        private void HandleMove(Point boardPos)
        {
            // Integrate your game logic to handle move
            // game.MakeMove(boardPos);  // Example of interacting with game logic
            RenderBoard();
        }

        private void RenderBoard()
        {
            // Iterate through the game state and update the UI accordingly
            var board = game.GetBoard();  // Assuming game.GetBoard() returns the current state of the board

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var piece = board[row, col];

                    if (piece == null)
                    {
                        boardButtons[row, col].Text = "";
                    }
                    else if (piece.IsKing)
                    {
                        boardButtons[row, col].Text = piece.Player == 1 ? "K1" : "K2";  // King pieces for player 1 or 2
                    }
                    else
                    {
                        boardButtons[row, col].Text = piece.Player == 1 ? "1" : "2";  // Regular pieces for player 1 or 2
                    }
                }
            }
        }
    }
    }

