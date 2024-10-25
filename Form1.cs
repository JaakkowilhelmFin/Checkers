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
        private CheckersGame game;
        private Button[,] boardButtons = new Button[8, 8];  
        private (int row, int col)? selectedPiece;  

        public Form1()
        {
            InitializeComponent();
            game = new CheckersGame();  
            CreateBoardUI();            
            RenderBoard();              
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
               
                if (game.board[row, col] != null)
                {
                    selectedPiece = (row, col);  
                    clickedButton.BackColor = Color.Yellow;  
                }
            }
            else  
            {
                var (selectedRow, selectedCol) = selectedPiece.Value;

                
                game.board[row, col] = game.board[selectedRow, selectedCol];  // Move the piece
                game.board[selectedRow, selectedCol] = null;  // Clear the old position

                selectedPiece = null;  
                RenderBoard();  
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
    }
}


