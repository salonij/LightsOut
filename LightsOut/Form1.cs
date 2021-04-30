using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class Form1 : Form
    {
        public const int GRID_OFFSET = 25; // Distance from upper-left side of window
        public const int GRID_LENGTH = 200; // Size in pixels of grid
        public const int NUM_CELLS = 5; // Number of cells in grid
        public bool[,] Grid; // Stores on/off state of cells in grid
        public Random Rand; // Used to generate random numbers
        public Form1()
        {
            InitializeComponent();
            Rand = new Random(); // Initializes random number generator
            Grid = new bool[NUM_CELLS, NUM_CELLS];
            // Randomly on the lights
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    Grid[r, c] = Rand.Next(2) == 1 ? true : false;
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    // Get proper pen and brush for on/off
                    // grid section
                    Brush brush;
                    Pen pen;

                    if (Grid[r, c])
                    {
                        pen = Pens.Green;
                        brush = Brushes.Yellow; // On
                    }
                    else
                    {
                        pen = Pens.Yellow;
                        brush = Brushes.Green; // Off
                    }

                    // Determine (x,y) coord of row and col to draw rectangle
                    int RectSize = GRID_LENGTH / NUM_CELLS;
                    int x = c * RectSize + GRID_OFFSET;
                    int y = r * RectSize + GRID_OFFSET;

                    g.DrawRectangle(pen, x, y, RectSize, RectSize); // Rectangle outline
                    g.FillRectangle(brush, x + 1, y + 1, RectSize - 1, RectSize - 1); // Solid rectangle
                }

        }

        public void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
            // Find row, col of mouse press
            int RectSize = GRID_LENGTH / NUM_CELLS;
            int r = (e.Y - GRID_OFFSET) / RectSize;
            int c = (e.X - GRID_OFFSET) / RectSize;
            // Invert selected box and all surrounding boxes 

            changeState(Grid[r, c],r,c);
            
            // Check to see if puzzle has been solved
            if (Won())
            {
                // Display winner dialog box just inside window
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void changeState(object sender, int r, int c)
        {
            Grid[r, c] = !Grid[r, c];

            if (r > 0)
            {
                Grid[r - 1, c] = !Grid[r - 1, c]; //Above
            }
            if (r < (Grid.GetLength(1) - 1))
            {
                Grid[r + 1, c] = !Grid[r + 1, c]; //Below
            }
            if (c > 0)
            {
                Grid[r, c - 1] = !Grid[r, c - 1]; //Left
            }
            if (c < (Grid.GetLength(1) - 1))
            {
                Grid[r, c + 1] = !Grid[r, c + 1]; //Right
            }

            // Redraw grid
            this.Invalidate();
        }
        public bool Won()
        {
            //checks if all the squares in the grid are off.
            int count = 0;
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    if(Grid[r, c])
                    {
                        count++;
                    }
                }

            //true if all the squares in the grid are off, otherwise false.
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                   
        }

        public void NewGameButton_Click(object sender, EventArgs e)
        {
            // Fill grid with either green or yellow
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    Grid[r, c] = Rand.Next(2) == 1 ? true : false;
            // Redraw grid
            this.Invalidate();
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
