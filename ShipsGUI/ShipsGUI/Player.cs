using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipsGUI
{
    public abstract class Player
    {
        protected Map fleetMap;
        protected Map hitMap;
        protected List<Ships> listOfShips;
        protected byte[] shipsSizes = { 6, 4, 4, 3, 3, 2, 2 };

        public Player()
        {
            listOfShips = new List<Ships>();
            fleetMap = new Map(10);
            hitMap = new Map(10);
        }
        public byte[] ShipsSizes { get { return shipsSizes; } }
        public Map FleetMap { get { return fleetMap; } }
        public Map HitMap { get { return  hitMap; } }
        public List<Ships> ListOfShips { get { return listOfShips; } }
        public abstract (bool, byte, byte) Shoot(byte x, byte y);

        public void setShips_randomly()
        {
            Random r = new Random();
            Direction d;
            int x, y;
            for (int i = 0; i < shipsSizes.Length; i++)
            {
                while (true)
                {
                    x = r.Next(10);
                    y = r.Next(10);

                    if (r.Next(2) == 0) d = Direction.VERTICAL;
                    else d = Direction.HORIZONTAL;

                    Ships s = new Ships((byte)x, (byte)y, shipsSizes[i], d);
                    if (fleetMap.putShip(s))
                    {
                        listOfShips.Add(s);
                        break;
                    }
                    else continue;
                }
            }
        }
        public bool ifsinked(Ships s)
        {
            if (s == null) return false;

            int Size = 0;
            for (int i = 0; i < s.Size; i++)
            {
                if (s.mast[i] == true) Size++;
            }
            if (s.Size == Size)
            {
                listOfShips.Remove(s);
                return true;
            }
            else return false;
        }
        public bool ifsinkedALL()
        {
            if (listOfShips.Count == 0) return true;
            else return false;
        }

    }
}
