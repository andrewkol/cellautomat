using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    class MyInfusoria : MyCell
    {
        public MyInfusoria(int sX, int sY, int wdth, int hgt, int DX, int i, int j) : base(sX, sY, wdth, hgt, DX, i, j)
        {
            base.chanceOfBirth = 0.6;
            base.chanceOfDeath = 0.3;

        }
        public override void Draw(Panel p1)
        {
            graf = p1.CreateGraphics();
            graf.FillRectangle(new SolidBrush(Color.DarkMagenta), base.rect);
        }
        public override int Move()
        {
            return (Rand.GetRandom(1, 5));
        }
    }
}
