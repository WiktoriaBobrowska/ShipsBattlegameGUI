using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipsGUI
{
    public class ShipRectangle
    {
        public Rectangle r;
        public int size, x, y;
        public Direction direction = Direction.HORIZONTAL;
        public ShipRectangle(int x, int y, int width, int height, int siz) { 
            this.x = x;
            this.y = y;
            r= new Rectangle(x, y, width, height);
            size = siz;
        }
        public void rotate()
        {
            int temp = r.Width;
            r.Width = r.Height;
            r.Height = temp;
            if(direction == Direction.HORIZONTAL) direction=Direction.VERTICAL;
            else if(direction == Direction.VERTICAL) direction=Direction.HORIZONTAL;

        }
    }
    public class HumanPlayer : Player
    {
        public List<ShipRectangle> r;
        public HumanPlayer( Point p, int fieldsize ) : base()
        {
            r = new List<ShipRectangle>();
            placeShips(p, fieldsize);
        }
        public override (bool, byte, byte) Shoot(byte x, byte y)
        {
            (bool, byte, byte) shoot;
            if (!hitMap.pointShoot(x, y).isdone) {
            MessageBox.Show("Nie można strzelić w to samo pole");
            shoot = (false, x, y);
            }
            else shoot = (true, x, y);
            return shoot;
        }

       public void placeShips(Point p, int fieldsize)
        {
           /* Image img6 = Image.FromFile("6-masted.jpg");
            Image img4 = Image.FromFile("4-masted.jpg");
            Image img3 = Image.FromFile("3-masted.jpg");
            Image img2 = Image.FromFile("2-masted.jpg");*/

            int padding = 25;


            for (int i = 0; i < shipsSizes.Length; i++) {
              /*  Image ac;
                if (shipsSizes[i] == 6) ac = img6;
                else if (shipsSizes[i] == 4) ac = img4;
                else if (shipsSizes[i] == 3) ac = img3;
                else if (shipsSizes[i] == 2) ac = img2;
                else ac = null;*/
                int size = 0;
                for (int j = 0; j < i; j++) { 
                    size += (int)shipsSizes[j] * fieldsize + padding;
                }
                r.Add(new ShipRectangle(p.X + size, p.Y, shipsSizes[i] * fieldsize, fieldsize, shipsSizes[i]));
                //e.Graphics.DrawImage(ac, r[i]); 
            }
        }
         public void drawShips(PaintEventArgs e)
        {
            foreach (ShipRectangle rect in r) {
                Pen pen = new Pen(Color.Black);
                Brush brush = new SolidBrush(Color.GreenYellow);
                e.Graphics.FillRectangle(brush, rect.r);
                e.Graphics.DrawRectangle(pen,rect.r);
            }
        }

        public ShipRectangle onClick(Point p)
        {
            for (int i = 0; i < r.Count; i++)
                if (p.X >= r[i].r.X && p.Y >= r[i].r.Y && p.X <= r[i].r.X + r[i].r.Width && p.Y <= r[i].r.Y + r[i].r.Height)
                    return r[i];
            return null;
        }

        public bool putShipManually(ShipRectangle sr)
        {
            Ships s = new Ships((byte)sr.x, (byte)sr.y, sr.size, sr.direction);
            Console.WriteLine(sr.x.ToString()+ " " + sr.y.ToString());
            if (FleetMap.putShip(s) == true)
            {
                listOfShips.Add(s);
                r.Remove(sr);
            }

            if (r.Count == 0) return true;
            else return false;
        }

        public void deleteSR()
        {
            r.Clear();
        }
    }
}
