using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Paint
{
    class Tpunkt : TFigura
    {
        public Tpunkt(int x, int y)
        {


            this.pj_X = x;
            this.pj_Y = y;
            this.pj_Color = Color.Black;
            this.pj_Grubosc = 20;
        }


        override public void pj_Wykresl()
        {

            

        }
        override public void pj_Wymaz()
        {
            if (this.pj_Widoczny)
            { 
            }
        }


        override public bool dotyka(int x, int y)
        {


         
            if ((pj_X - pj_Grubosc / 2) <= x && (pj_X + pj_Grubosc / 2) >= x && pj_Y - pj_Grubosc / 2 <= y && pj_Y + pj_Grubosc / 2 >= y) return true;

            return false;
        }
    }
}
