using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipsGUI
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }


        private void title_Click(object sender, EventArgs e)
        {

        }


        private DialogResult menu_Window(String message, String title)
        {
            MessageBoxButtons button = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, button);
            return result;
        }

        private void StartGame_Click(object sender, EventArgs e)
        {

            this.Hide();
            GameForm game = new GameForm();
            game.Show();
                
        }

        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Battleship (also known as Battleships or Sea Battle) is a strategy type guessing game for two players. It is played on ruled grids (paper or board) on which each player's fleet of warships are marked. The locations of the fleets are concealed from the other player. Players alternate turns calling shots at the other player's ships, and the objective of the game is to destroy the opposing player's fleet.");
        }

    }
}
