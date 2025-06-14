using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsGUI
{
    public class Ships
    {
        public bool[] mast { get; set; }
        public byte X { get; private set; }
        public byte Y { get; private set; }
        public Direction direct { get; private set; }
        public int Size { get { return mast.Length; } }
        void setMast()
        {
            for (int i = 0; i < mast.Length; i++)
                mast[i] = false;
        }

        public Ships(byte x, byte y, int numberofmasts, Direction d)
        {
            this.X = x;
            this.Y = y;
            mast = new bool[numberofmasts];
            direct = d;
            setMast();
        }
        public Ships(int numberofmasts)
        {
            mast = new bool[numberofmasts];
        }
    }
}
