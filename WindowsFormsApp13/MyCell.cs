using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    class MyCell
    {
        protected int movement, indexI, indexJ, checkIndexI, checkIndexJ;
        protected double chanceOfDeath, chanceOfBirth;
        protected Rectangle rect;
        protected Graphics graf;
        protected string indexString;
        protected Font indexFont;
        protected SolidBrush indexColor;
        public MyCell(int sX, int sY, int wdth, int hght, int DX, int i, int j)
        {
            movement = DX;
            indexI = i;
            indexJ = j;
            checkIndexI = i;
            checkIndexJ = j;
            indexString = $"{checkIndexI}, {checkIndexJ}";
            indexFont = new Font("Arial", 12);
            indexColor = new SolidBrush(Color.Red);
            rect = new Rectangle(sX, sY, wdth, hght);
        }
        public int _IndexI { get { return indexI; } set { indexI = value; } }
        public int _movement { get { return movement; } }
        public int _IndexJ { get { return indexJ; } set { indexJ = value; } }
        public double _chanceOfDeath { get { return chanceOfDeath; } }
        public double _chanceOfBirth { get { return chanceOfBirth; } }
        public virtual int Move() { return 0; }
        public virtual void Draw(Panel p1) { }
    }
}
