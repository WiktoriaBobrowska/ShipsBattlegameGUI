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
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void changeWindow()
        {
            this.Hide();
            Battle battle = new Battle();
            battle.Show();
        }

        private void Loading_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                changeWindow();
            }
        }
    }
}
