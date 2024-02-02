using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    class MyAmoeba : MyCell
    {
        public MyAmoeba(int sX, int sY, int wdth, int hgt, int DX, int i, int j) : base(sX, sY, wdth, hgt, DX, i, j)
        {
            base.chanceOfBirth = 0.3;
            base.chanceOfDeath = 0.6;
        }
        public override int Move()
        {
            return (Rand.GetRandom(1, 8));
        }
        public override void Draw(Panel p1)
        {
            graf = p1.CreateGraphics();
            graf.FillEllipse(new SolidBrush(Color.BurlyWood), base.rect);
        }
    }
}
