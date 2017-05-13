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
    class TFigura
    {
        private int pj_ID; // nr kolejny figury

        private static int pj_next_id = 0; // statyczny

        private Color pj_color_foreground; // kolor tla

        private Color pj_color; // kolor  

        private List<Tpunkt> pj_lista_pktow = new List<Tpunkt>();

        private System.Drawing.Drawing2D.DashStyle pj_rodzaj_linii;

        private int pj_grubosc;

        private bool pj_widoczny;

        private int pj_x, pj_y;

        private bool pj_wypelniony;

        private Pen pj_pioro;

        private Brush pj_pedzel;


        public TFigura()
        {
            pj_next_id++; // zwiekszenie id

            this.pj_ID = pj_next_id; // zapisanie id figury

            // przy tworzeniu figury 
            this.pj_Widoczny = false;
            this.pj_Grubosc = 1;
            this.pj_Rodzaj_Linii = System.Drawing.Drawing2D.DashStyle.Solid;
            this.pj_Kolor_tla = Color.Black;


            this.pj_wypelniony = false;


            pj_pioro = null;
            pj_pedzel = null;


            this.pj_X = 0;
            this.pj_Y = 0;

        }

        public String pj_opisz()
        {
            String txt = (pj_Pioro != null) ? " Pioro: " + pj_Pioro.Color.ToString() : "";
            String txt2 = (pj_Pedzel != null) ? " Pedzel: " + pj_Pedzel.ToString() : "";  // +" Pedzel: " + pc_Pedzel.ToString();

            return txt + txt2;
        }

        public Pen pj_Pioro
        {
            get
            {
                return pj_pioro;
            }
            set
            {
                this.pj_pioro = value;
            }
        }

        public Brush pj_Pedzel
        {
            get
            {
                return pj_pedzel;
            }
            set
            {
                this.pj_pedzel = value;
            }
        }





        public void pj_UstawXY(int x, int y)
        {
            this.pj_X = x;
            this.pj_Y = y;
        }

        public void pj_PrzesunDoNowegoXY(int _x, int _y)
        {
            this.pj_Wymaz();
            this.pj_UstawXY(_x, _y);
            this.pj_Wykresl();
        }


        public void pj_FormatujFG(Color _kolor, System.Drawing.Drawing2D.DashStyle _rodzaj_linii, int _grubosc)
        {
            this.pj_Rodzaj_Linii = _rodzaj_linii;
            this.pj_Color = _kolor;
            this.pj_Grubosc = _grubosc;
        }


        public int getID()
        {
            return pj_ID;

        }
        virtual public bool dotyka(int x, int y)
        {
            return false;
        }

        public bool pj_Wypelniony
        {
            get
            {
                return pj_wypelniony;
            }
            set
            {
                this.pj_wypelniony = value;
            }
        }

        public int pj_X
        {
            get
            {
                return pj_x;
            }
            set
            {
                this.pj_x = value;
            }
        }


        public int pj_Y
        {
            get
            {
                return pj_y;
            }
            set
            {
                this.pj_y = value;
            }
        }


        public Color pj_Color
        {
            get
            {
                return pj_color;
            }
            set
            {
                this.pj_color = value;
            }
        }

        public Color pj_Color_Foreground
        {
            get
            {
                return pj_color_foreground;
            }
            set
            {
                this.pj_color_foreground = value;
            }
        }


        public int pj_Grubosc
        {
            get
            {
                return pj_grubosc;
            }
            set
            {
                this.pj_grubosc = value;
            }
        }


        public bool pj_Widoczny
        {
            get
            {
                return pj_widoczny;
            }
            set
            {
                this.pj_widoczny = value;
            }
        }



        public System.Drawing.Drawing2D.DashStyle pj_Rodzaj_Linii
        {
            get
            {
                return pj_rodzaj_linii;
            }
            set
            {
                this.pj_rodzaj_linii = value;
            }
        }





        public void pj_Dodaj_pkt(Tpunkt pkt)
        {
            pj_lista_pktow.Add(pkt);
        }



        public Color pj_Kolor_tla
        {
            get
            {
                return pj_color_foreground;
            }
            set
            {
                this.pj_color_foreground = value;
            }
        }


        virtual public void pj_Wykresl()
        {

            this.pj_Widoczny = true;
        }

        virtual public void pj_Wymaz()
        {
            this.pj_Widoczny = false;
        }

        public TFigura getFigura()
        {
            return this;
        }
    }
}
