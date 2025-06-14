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
    public partial class Battle : Form
    {
        int dim = 10;
        int fieldsize=45;
        Point start_map1 = new Point(10, 10), start_map2=new Point(800, 10), point = new Point(10, 550);
        Point point2 = new Point();
        HumanPlayer humanPlayer;
        BotPlayer bot;
        ShipRectangle shipRectangle;
        bool flag_setShip = false;
        bool gameover =false;

        public Battle()
        {
            InitializeComponent();
            humanPlayer = new HumanPlayer(point, fieldsize);
            bot = new BotPlayer();
            button2.Visible = false;
            button3.Visible = false;
        }

        private void Battle_Load(object sender, EventArgs e)
        {
        }

        private void mapsBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (shipRectangle == null) return;
            int coordinates_x = (shipRectangle.r.X - start_map1.X) / fieldsize;
            int coordinates_y = (shipRectangle.r.Y - start_map1.Y) / fieldsize;

            if (coordinates_x >= 0 && coordinates_y >= 0 && coordinates_x < dim && coordinates_y < dim)
            {
                shipRectangle.x = coordinates_x;
                shipRectangle.y= coordinates_y;
                if(humanPlayer.putShipManually(shipRectangle)) flag_setShip=true;
                button1.Hide();
            }

            mapsBox.Invalidate();
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            humanPlayer.FleetMap.erase();
            humanPlayer.setShips_randomly();
            humanPlayer.deleteSR();
            button2.Visible = true;
            mapsBox.Invalidate();
        }

        private void humanPlayer_turn(int coordinates_x, int coordinates_y)
        {
            (bool success, byte x, byte y) = humanPlayer.Shoot((byte)coordinates_x, (byte)coordinates_y);
            
            if (success)
            {
                Ships s = bot.FleetMap.doShoot(x, y);
                if (s != null) humanPlayer.HitMap.setShoot(x, y);
                else humanPlayer.HitMap.setMiss(x, y);
                if (bot.ifsinked(s)) MessageBox.Show("Statek został zatopiony!");
                if (bot.ifsinkedALL())
                {
                    MessageBox.Show("Zatopiłeś wszystkie statki!");
                    gameover = true;
                    ifGameOver();
                }
            }
        }

        private void botPlayer_turn()
        {
            (bool success, byte x, byte y) = bot.Shoot();
            Ships s = humanPlayer.FleetMap.doShoot(x, y);
            if (s != null) bot.HitMap.setShoot(x, y);
            else bot.HitMap.setMiss(x, y);
            label2.Text = "Ostrzelono Cię w punkcie" + (x, y);
            if (humanPlayer.ifsinked(s) == true) MessageBox.Show("Twój statek został zatopiony!");
            if (humanPlayer.ifsinkedALL() == true) {
                MessageBox.Show("Przeciwnik zatopił wszystkie Twoje statki!");
                gameover = true;
                ifGameOver();
            }
        }

        private void ifGameOver()
        {
            if (gameover)
            {
                gameover = false;
                button3.Visible = true;
            }
        }
        private void mapsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!flag_setShip) return;


            int coordinates_x = (e.X - start_map2.X) / fieldsize;
            int coordinates_y = (e.Y - start_map2.Y) / fieldsize;
            if (coordinates_x >= 0 && coordinates_y >= 0 && coordinates_x < dim && coordinates_y < dim)
            {
                humanPlayer_turn(coordinates_x, coordinates_y);
                botPlayer_turn();
            }
            
            mapsBox.Invalidate();
            if (gameover) return;
        }

        private void mapBox_Paint(object sender, PaintEventArgs e)
        {
          humanPlayer.FleetMap.displayMap(start_map1, e, fieldsize);
          humanPlayer.HitMap.displayMap(start_map2, e, fieldsize);
          humanPlayer.drawShips(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag_setShip = true;
            button1.Visible=false;
            button2.Visible=false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameForm game = new GameForm();
            game.Show();
        }

        private void mapsBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                shipRectangle = humanPlayer.onClick(e.Location);
                if (shipRectangle == null) return;
                point2.X = e.Location.X - shipRectangle.r.X;
                point2.Y = e.Location.Y - shipRectangle.r.Y;
            }
            else
            {
                if (shipRectangle == null) return;
                shipRectangle.rotate();
                int temp = point2.X;
                point2.X = point2.Y;
                point2.Y=temp;
            }
        }

        private void mapsBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (shipRectangle == null) return;
            if (e.Button == MouseButtons.Left)
            {
                shipRectangle.r.X = e.X - point2.X;
                shipRectangle.r.Y = e.Y - point2.Y;

                mapsBox.Invalidate();
            }

        }

     /*   private void ships_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox someship = sender as PictureBox;
            if (e.Button == MouseButtons.Left)
            {
                someship.Left += e.X - point2.X;
                someship.Top += e.Y - point.Y;
            }
        }

        private void ships_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox somehip = sender as PictureBox;
            if (e.Button == MouseButtons.Left) return;
            rotateShip(somehip);
        }


        private void rotateShip(PictureBox B)
        {
            bmb = (Bitmap)B.BackgroundImage;
            bmb.RotateFlip(RotateFlipType.Rotate90FlipNone);
            B.BackgroundImage = bmb;
            int tempHigh = B.Width;
            B.Width = B.Height;
            B.Height = tempHigh;
        }*/
    }
}
