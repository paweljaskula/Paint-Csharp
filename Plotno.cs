using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Plotno
    {

        // lista figur LFG
        private List<TFigura> pj_LFG = new List<TFigura>();

        private int margines;

        public Plotno()
        {
            margines = 10;
        }

        public void wyczysc()
        {
            pj_LFG.Clear();
        }
        public int getMargin()
        {
            return margines;
        }

        public List<TFigura> pj_get_LFG_lista()
        {
            return pj_LFG;
        }

        public int pj_Count_elements()
        {
            return pj_LFG.Count;

        }

        public void pj_Dodaj_Figure(TFigura figura)
        {
            pj_LFG.Add(figura);
        }

        public void pj_Usun_Figure(TFigura figura)
        {
            // usuwa wybrana figure
        }

        public void pj_ponumeruj_figury()
        {

        }


        public void pj_test_rysowania()
        {

        }
    }
}
