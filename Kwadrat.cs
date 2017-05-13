using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Kwadrat : Prostokat
    {

        int pj_bok;

        public int pj_Bok
        {
            get
            {
                return pj_bok;

            }

            set
            {
                pj_bok = value;
                this.pj_Szerokosc = pj_bok;
                this.pj_Wysokosc = pj_bok;

            }
        }

    }
}
