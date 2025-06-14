using ShipsGUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipsGUI
{
    public class MapField
    {
        public Field field { get; set; }
        public Ships ship { get; set; }
        public MapField()
        {
            field = Field.EMPTY;
            ship = null;
        }

    }

    public class Map
    {
        private MapField[,] map;
        private int dimension;
       public MapField[,] _map => map;

        public Map(int dim) 
        {
            dimension = dim;
            map = new MapField[dimension, dimension];

            for (int i = 0; i < dimension; i++)
                for (int j = 0; j < dimension; j++)
                    map[i, j] = new MapField();
        }
        public void erase()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j].ship = null;
                    map[i, j].field = Field.EMPTY;

                }

        }

        private void putShipInner(Ships s)
        {
            if(s.direct == Direction.HORIZONTAL)
                for(int i = 0; i < s.Size; i++)
                {
                    map[s.X + i, s.Y].field = Field.PRESENT;
                    map[s.X + i, s.Y].ship = s;
                }
            else
                for (int i = 0; i < s.Size; i++)
                {
                    map[s.X, s.Y + i].field = Field.PRESENT;
                    map[s.X, s.Y + i].ship = s;
                }
        }

        public bool putShip(Ships s)
        {
            if (s.direct == Direction.VERTICAL)
            {
                if (s.Y + s.Size > map.GetLength(1))
                    return false;

                for (int i = s.X - 1; i <= s.X + 1; i++)
                    for (int j = s.Y - 1; j <= s.Y + s.Size + 1; j++)
                        if (i >= 0 && j >= 0 && i < map.GetLength(0) && j < map.GetLength(1) && map[i, j].field == Field.PRESENT)
                            return false;
            }
            else //poziomo
            {
                if (s.X + s.Size > map.GetLength(0))
                    return false;

                for (int i = s.X - 1; i <= s.X + s.Size + 1; i++)
                    for (int j = s.Y - 1; j <= s.Y + 1; j++)
                    
                        if (i>=0 && j>=0 && i< map.GetLength(0) && j<map.GetLength(1) && map[i, j].field == Field.PRESENT)
                            return false;
            }
            putShipInner(s);
            return true;
        }

        public void displayMap(Point p, PaintEventArgs e, int fieldsize)
        {
            Pen pen = new Pen(Color.Black, 3);
            SolidBrush brush = new SolidBrush(Color.Transparent);
            for (int i=0; i< map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Rectangle r = new Rectangle(p.X + i * fieldsize, p.Y + j * fieldsize, fieldsize, fieldsize);

                    if (map[i, j].field == Field.EMPTY) {
                        brush.Color = Color.White;
                        e.Graphics.FillRectangle(brush, r);
                    }
                    else if(map[i, j].field == Field.PRESENT)
                    {
                        brush.Color = Color.YellowGreen;
                        e.Graphics.FillRectangle(brush, r);
                    }
                    else if (map[i, j].field == Field.SHOOTED)
                    {
                        brush.Color = Color.Red;
                        e.Graphics.FillRectangle(brush, r);
                    }
                    else if (map[i, j].field == Field.MISSED)
                    {
                        brush.Color = Color.Gray;
                        e.Graphics.FillRectangle(brush, r);
                    }
                    e.Graphics.DrawRectangle(pen, r);
                }
            }
        }
        public (bool isdone, Field where) pointShoot(byte x, byte y)
        {
            if (map[x, y].field == Field.EMPTY) map[x, y].field=Field.MISSED;
            else if (map[x, y].field == Field.PRESENT) map[x, y].field=Field.SHOOTED;
            else if (map[x,y].field == Field.MISSED || map[x, y].field == Field.SHOOTED) return(false, map[x, y].field);

            return (true, map[x, y].field);
        }

        public Ships doShoot(byte x, byte y)
        {
            if (map[x, y].ship == null) return null;
            
             map[x, y].field = Field.SHOOTED;

                if (x == map[x, y].ship.X)
                map[x, y].ship.mast[y - map[x, y].ship.Y] = true;
                else if (y == map[x, y].ship.Y)
                map[x, y].ship.mast[x - map[x, y].ship.X] = true;


            return map[x, y].ship;
        }

        public void setShoot(byte x, byte y)
        {
            map[x, y].field = Field.SHOOTED;
        }

        public void setMiss(byte x, byte y)
        {
            map[x, y].field |= Field.MISSED;
        }
    }
}