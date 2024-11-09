using System;

using System.Drawing;

using System.Windows.Forms;

namespace checkers
{

    public partial class Form1 : Form
    {
        private CheckersGame game;
        private Button[,] boardButtons = new Button[8, 8];
        private (int row, int col)? selectedPiece;

        
        private Label currentPlayerLabel = new Label();

        public Form1()
        {
            InitializeComponent();
            game = new CheckersGame();
            CreateBoardUI();
            RenderBoard();

            
            currentPlayerLabel.Text = "Current Player: Player " + game.CurrentPlayer;
            currentPlayerLabel.Location = new System.Drawing.Point(10, 420);  
            currentPlayerLabel.AutoSize = true;
            this.Controls.Add(currentPlayerLabel);
        }

        private void CreateBoardUI()
        {
            int tileSize = 50;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button button = new Button
                    {
                        Width = tileSize,
                        Height = tileSize,
                        FlatStyle = FlatStyle.Flat,
                        Location = new System.Drawing.Point(col * tileSize, row * tileSize),
                        Tag = (row, col)
                    };

                    if ((row + col) % 2 == 1)
                    {
                        button.BackColor = Color.Black;
                        button.ForeColor = Color.White;
                    }
                    else
                    {
                        button.BackColor = Color.White;
                        button.Enabled = false;
                    }

                    button.Click += OnBoardButtonClick;
                    boardButtons[row, col] = button;
                    this.Controls.Add(button);
                }
            }
        }

        private void OnBoardButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            var (row, col) = ((int row, int col))clickedButton.Tag;

            if (selectedPiece == null)
            {
               
                if (game.board[row, col] != null && game.board[row, col].Player == game.CurrentPlayer)
                {
                    selectedPiece = (row, col);
                    clickedButton.BackColor = Color.Yellow;
                }
            }
            else
            {
                var (selectedRow, selectedCol) = selectedPiece.Value;

               
                if (IsMoveValid(selectedRow, selectedCol, row, col))
                {
                    game.board[row, col] = game.board[selectedRow, selectedCol];  
                    game.board[selectedRow, selectedCol] = null;  

                    selectedPiece = null;
                    RenderBoard();

                    
                    game.NextTurn();
                    currentPlayerLabel.Text = "Current Player: Player " + game.CurrentPlayer;

                    
                    int winner = game.CheckForWinner();
                    if (winner != 0)
                    {
                        MessageBox.Show("Player " + winner + " wins!");
                        ResetGame();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid move. Try again.");
                    selectedPiece = null;
                    RenderBoard();
                }
            }
        }

        private void RenderBoard()
        {
            var board = game.GetBoard();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null)
                    {
                        if (piece.Player == 1)
                            boardButtons[row, col].Text = "P1";
                        else if (piece.Player == 2)
                            boardButtons[row, col].Text = "P2";
                    }
                    else
                    {
                        boardButtons[row, col].Text = "";
                    }

                    if ((row + col) % 2 == 1)
                    {
                        boardButtons[row, col].BackColor = Color.Black;
                    }
                }
            }
        }

        
        private bool IsMoveValid(int startRow, int startCol, int endRow, int endCol)
        {
            
            return Math.Abs(startRow - endRow) == 1 && Math.Abs(startCol - endCol) == 1;
        }

        // Alla päivitetään  peli alkuasentoon
        private void ResetGame()
        {
            game = new CheckersGame();
            selectedPiece = null;
            currentPlayerLabel.Text = "Current Player: Player " + game.CurrentPlayer;
            RenderBoard();
        }
    }

}


