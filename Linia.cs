using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Linia : TFigura
    {
        int pj_szerokosc;
        int pj_wysokosc;

        public int pj_Szerokosc
        {
            get
            {
                return pj_szerokosc;
            }

            set
            {
                this.pj_szerokosc = value;
            }
        }

        public int pj_Wysokosc
        {
            get
            {
                return pj_wysokosc;
            }

            set
            {
                this.pj_wysokosc = value;
            }
        }


        override public bool dotyka(int x, int y)
        {

            return false;

        }
    }
}
