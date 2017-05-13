using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace Paint
{
    public partial class PaintJaskula44185 : Form
    {
        int pj_Szerokość, pj_Wysokość;
        Graphics pj_Rysownica;
        Point pj_Punkt;
        Pen pj_Pióro;
        System.Drawing.SolidBrush pj_drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush pj_Pędzel = Brushes.Black;
        Random pj_LiczbaLosowa = new Random();
        List<TFigura> pj_lista_figur = new List<TFigura>();
        bool pj_show_console_logs;
        int pj_margin;
        int pj_mouse_x, pj_mouse_y;
        int pj_mouse_down_x, pj_mouse_down_y;
        int pj_wybrana_figura;

        private void pj_LOG(String tekst)
        {
            if (pj_show_console_logs)
            {
                Console.WriteLine("LOG: " + tekst);
            }
        }

        public PaintJaskula44185()
        {
            InitializeComponent();
            pj_rdbFigury.Checked = true;
            pj_txtGrubosc.Enabled = false;
            pj_txtGrubosc.Text = "1";
            pj_txtRozmiarGumki.Text = "5";
            pj_show_console_logs = true;

            this.pj_margin = 10;
            this.pj_mouse_x = 0;
            this.pj_mouse_y = 0;
            this.pj_mouse_down_x = 0;
            this.pj_mouse_down_y = 0;
            this.pj_wybrana_figura = 0;

            this.Left = 20;
            this.Top = 20;
            this.Width = 1000;
            this.Height = 660;

            this.SetAutoSizeMode(System.Windows.Forms.AutoSizeMode.GrowAndShrink);
            this.MaximizeBox = false;

            pj_imgKolorlinii.BackColor = Color.Black;


            pj_Pióro = new Pen(pj_imgKolorlinii.BackColor, 1);

            pj_imgRysownica.Location = new Point(30, 30);

            pj_imgRysownica.BackColor = Color.White;
            pj_imgKolortla.BackColor = pj_imgRysownica.BackColor;
            pj_imgRysownica.BorderStyle = BorderStyle.Fixed3D;
            pj_imgRysownica.Image = new Bitmap(pj_imgRysownica.Width, pj_imgRysownica.Height);
            pj_Rysownica = Graphics.FromImage(pj_imgRysownica.Image);
            pj_cbStyl.SelectedIndex = 4;
        }

        private void pj_trbGrubosc_Scroll(object sender, EventArgs e)
        {
            pj_txtGrubosc.Text = pj_trbGrubosc.Value.ToString();
            pj_WykreslWziernikLinii();
        }

        private void pj_btnZapisanie_Click(object sender, EventArgs e)
        {
            SaveFileDialog pj_OknoZapisuBitmapy = new SaveFileDialog();
            pj_OknoZapisuBitmapy.Filter = "Pliko rozszerzeniu: bmp|*.bmp";
            if (pj_OknoZapisuBitmapy.ShowDialog() == DialogResult.OK)
            {
                if (pj_OknoZapisuBitmapy.FileName != "")
                    pj_imgRysownica.Image.Save(pj_OknoZapisuBitmapy.FileName);
            }
        }

        private void pj_btnWczytanie_Click(object sender, EventArgs e)
        {
            OpenFileDialog pj_OknoOtwarciaMapyBitowej = new OpenFileDialog();
            pj_OknoOtwarciaMapyBitowej.Filter = "Pliki o rozszerzeniu bmp|*.bmp";
            if (pj_OknoOtwarciaMapyBitowej.ShowDialog() == DialogResult.OK)
            {
                string NazwaPlikuZmapąBitową = pj_OknoOtwarciaMapyBitowej.FileName;
                Bitmap BitMapaObrazu = new Bitmap(NazwaPlikuZmapąBitową);
                pj_Rysownica.DrawImage(BitMapaObrazu, pj_imgRysownica.Left, pj_imgRysownica.Top);
                pj_imgRysownica.Refresh();
            }
        }

        private void pj_imgRysownica_MouseMove(object sender, MouseEventArgs e)
        {
            
            pj_lblX.Text = e.Location.X.ToString();
            pj_lblY.Text = e.Location.Y.ToString();
            int LewyGórnyNarożnikX, LewyGórnyNarożnikY;
            if (pj_Punkt.X > e.Location.X)
                LewyGórnyNarożnikX = e.Location.X;
            else
                LewyGórnyNarożnikX = pj_Punkt.X;
            
            if (pj_Punkt.Y > e.Location.Y)
                LewyGórnyNarożnikY = e.Location.Y;
            else
                LewyGórnyNarożnikY = pj_Punkt.Y;
            Color kolor = pj_imgKolorlinii.BackColor;
            int grubosc = int.Parse(pj_txtGrubosc.Text);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (pj_rdbOlówek.Checked)
                {
                    pj_Rysownica.DrawLine(pj_Pióro, pj_Punkt, e.Location);
                    pj_Punkt = e.Location;
                    pj_imgRysownica.Invalidate();

                }
                if (pj_rdbGumka.Checked)
                {
                    pj_Pióro.Color = pj_imgKolortla.BackColor;
                    pj_Pióro.Width = trackBar1.Value;
                    pj_Rysownica.DrawLine(pj_Pióro, pj_Punkt, e.Location);
                    pj_Punkt = e.Location;
                    pj_imgRysownica.Invalidate();
                }
            }
            
        }
        private DashStyle pj_WybranyStylLinii(int pj_i)
        {
            switch (pj_i)
            {
                case 0:
                    return System.Drawing.Drawing2D.DashStyle.Dash;
                case 1:
                    return System.Drawing.Drawing2D.DashStyle.DashDot;
                case 2:
                    return System.Drawing.Drawing2D.DashStyle.DashDotDot;
                case 3:
                    return System.Drawing.Drawing2D.DashStyle.Dot;
                case 4:
                    return System.Drawing.Drawing2D.DashStyle.Solid;
                default:
                    MessageBox.Show("Nie ma takiego rodzaju linii!");
                    return System.Drawing.Drawing2D.DashStyle.Solid;
            }

        }
        
        private void pj_WykreslWziernikLinii()
        {
            int pj_gL;
            pj_gL = int.Parse(pj_txtGrubosc.Text);
            const int pj_Odstep = 5;
            Graphics pj_PowierzchniaGraf;
            pj_PowierzchniaGraf = this.pj_imgWziernikLini.CreateGraphics();

            pj_Pióro.DashStyle = pj_WybranyStylLinii(pj_cbStyl.SelectedIndex);
            pj_Pióro.Width = pj_gL;
            pj_Pióro.Color = pj_imgKolorlinii.BackColor;
            pj_PowierzchniaGraf.Clear(pj_imgKolortla.BackColor);
            pj_PowierzchniaGraf.DrawLine(pj_Pióro, 0 + pj_Odstep, pj_imgWziernikLini.Height / 2,
            pj_imgWziernikLini.Width / 2 +20, pj_imgWziernikLini.Height / 2);
            
            

        }

        private void PaintJaskula44185_Paint(object sender, PaintEventArgs e)
        {
            pj_WykreslWziernikLinii();
            
        }

        private void pj_cbStyl_SelectedIndexChanged(object sender, EventArgs e)
        {
            pj_WykreslWziernikLinii();
        }

        private void pj_btnKolorLinii_Click(object sender, EventArgs e)
        {
            ColorDialog pj_oknoKolorow = new ColorDialog();
            if (pj_oknoKolorow.ShowDialog() == DialogResult.OK)
            {
                pj_Pióro.Color = pj_oknoKolorow.Color;
                pj_Pędzel = new SolidBrush(pj_oknoKolorow.Color);
                pj_imgKolorlinii.BackColor = pj_oknoKolorow.Color;
                pj_WykreslWziernikLinii();
            }
        }

        private void pj_btnKolorTla_Click(object sender, EventArgs e)
        {
            
            ColorDialog pj_oknoKolorow = new ColorDialog();

            
            if (pj_oknoKolorow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pj_imgKolortla.BackColor = pj_oknoKolorow.Color;
                
                Refresh();pj_imgRysownica.BackColor = pj_imgKolortla.BackColor;
            }
                
            
        }

        private void pj_imgRysownica_MouseDown(object sender, MouseEventArgs e)
        {
            
            pj_lblX.Text = e.Location.X.ToString();
            pj_lblY.Text = e.Location.Y.ToString();
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                pj_Punkt = e.Location;
        }

        private void pj_imgRysownica_MouseUp(object sender, MouseEventArgs e)
        {
            pj_imgRysownica.Refresh();
            Color kolor = pj_imgKolorlinii.BackColor;
            int grubosc = int.Parse(pj_txtGrubosc.Text);
            pj_lblX.Text = e.Location.X.ToString();
            pj_lblY.Text = e.Location.Y.ToString();
            int pj_LewyGórnyNarożnikX, pj_LewyGórnyNarożnikY;
            if (pj_Punkt.X > e.Location.X)
                pj_LewyGórnyNarożnikX = e.Location.X;
            else
                pj_LewyGórnyNarożnikX = pj_Punkt.X;
            if (pj_Punkt.Y > e.Location.Y)
                pj_LewyGórnyNarożnikY = e.Location.Y;
            else
                pj_LewyGórnyNarożnikY = pj_Punkt.Y;
            int pj_Szerokość, pj_Wysokość;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (pj_rdbOlówek.Checked)
                {
                    pj_Rysownica.DrawLine(pj_Pióro, pj_Punkt, e.Location);
                    pj_imgRysownica.Invalidate();
                }
                if (pj_rdbFigury.Checked)
                {
                    if (pj_rdbOkrąg.Checked)
                    {

                        pj_Szerokość = e.Location.X - pj_Punkt.X;
                        pj_Wysokość = pj_Szerokość;
                        pj_Rysownica.DrawEllipse(pj_Pióro, new Rectangle(pj_Punkt.X, e.Location.Y, pj_Szerokość, pj_Szerokość));
                        pj_imgRysownica.Refresh();


                        // elipsa
                        Kolo noweKolo = new Kolo();
                        noweKolo.pj_Szerokosc = pj_Szerokość;
                        noweKolo.pj_Wysokosc = pj_Wysokość;
                        noweKolo.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        noweKolo.pj_Wypelniony = false;
                        noweKolo.pj_Pioro = pj_Pióro;
                        noweKolo.pj_Color = kolor;
                        noweKolo.pj_Grubosc = grubosc;
                        pj_lista_figur.Add(noweKolo);


                        pj_LOG("Dodano okrag");
                    }
                    if (pj_rdbElipsa.Checked)
                    {
                        pj_Szerokość = e.Location.X - pj_Punkt.X;
                        pj_Wysokość = e.Location.Y - pj_Punkt.Y;
                        pj_Rysownica.DrawEllipse(pj_Pióro, new Rectangle(pj_Punkt.X, pj_Punkt.Y, pj_Szerokość, pj_Wysokość));
                        pj_imgRysownica.Refresh();
                        Kolo noweKolo = new Kolo();
                        noweKolo.pj_Szerokosc = pj_Szerokość;
                        noweKolo.pj_Wysokosc = pj_Wysokość;
                        noweKolo.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        noweKolo.pj_Wypelniony = false;
                        noweKolo.pj_Pioro = pj_Pióro;
                        noweKolo.pj_Color = kolor;
                        noweKolo.pj_Grubosc = grubosc;
                        pj_lista_figur.Add(noweKolo);


                        pj_LOG("Dodano elipsa");
                    }
                    if (pj_rdbKwadrat.Checked)
                    {


                        pj_Szerokość = Math.Abs(e.Location.X - pj_Punkt.X);
                        pj_Wysokość = pj_Szerokość;

                        pj_Punkt.X = Math.Min(pj_Punkt.X, e.Location.X);
                        pj_Punkt.Y = Math.Min(pj_Punkt.Y, e.Location.Y);
                        pj_Rysownica.DrawRectangle(pj_Pióro, new Rectangle(pj_Punkt.X, pj_Punkt.Y, pj_Szerokość, pj_Wysokość));

                        Kwadrat nowyKwadrat = new Kwadrat();
                        nowyKwadrat.pj_Bok = pj_Szerokość;
                        nowyKwadrat.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);

                        nowyKwadrat.pj_Pioro = pj_Pióro;
                        nowyKwadrat.pj_Color = kolor;
                        nowyKwadrat.pj_Grubosc = grubosc;

                        pj_lista_figur.Add(nowyKwadrat);
                        pj_imgRysownica.Refresh();



                        pj_LOG("Dodano kwadrat");
                    }
                    if (pj_rdbProstokąt.Checked)
                    {
                        pj_Szerokość = Math.Abs(e.Location.X - pj_Punkt.X);
                        pj_Wysokość = Math.Abs(e.Location.Y - pj_Punkt.Y); ;
                        pj_Punkt.X = Math.Min(pj_Punkt.X, e.Location.X);
                        pj_Punkt.Y = Math.Min(pj_Punkt.Y, e.Location.Y);
                        pj_Rysownica.DrawRectangle(pj_Pióro, new Rectangle(pj_Punkt.X, pj_Punkt.Y, pj_Szerokość, pj_Wysokość));

                        Prostokat nowyProstokat = new Prostokat();
                        nowyProstokat.pj_Szerokosc = pj_Szerokość;
                        nowyProstokat.pj_Wysokosc = pj_Wysokość;

                        nowyProstokat.pj_Pioro = pj_Pióro;
                        nowyProstokat.pj_Color = kolor;
                        nowyProstokat.pj_Grubosc = grubosc;


                        nowyProstokat.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        pj_lista_figur.Add(nowyProstokat);
                        pj_imgRysownica.Refresh();


                        pj_LOG("Dodano prostokat");
                    }
                    if (pj_rdbLiniaProsta.Checked)
                    {
                        pj_Szerokość = e.Location.X - pj_Punkt.X;
                        pj_Wysokość = e.Location.Y - pj_Punkt.Y;
                        pj_Rysownica.DrawLine(pj_Pióro, pj_Punkt.X, pj_Punkt.Y, pj_Punkt.X + pj_Szerokość, pj_Punkt.Y + pj_Wysokość);




                        // prostokat
                        Linia linia = new Linia();

                        linia.pj_Szerokosc = pj_Szerokość;
                        linia.pj_Wysokosc = pj_Wysokość;

                        linia.pj_Pioro = pj_Pióro;
                        linia.pj_Color = kolor;
                        linia.pj_Grubosc = grubosc;

                        linia.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        pj_lista_figur.Add(linia);
                        pj_imgRysownica.Refresh();
                        pj_LOG("Dodano linia prosta");
                    }
                    if (pj_rdbPelnyOkrag.Checked)
                    {

                        pj_Szerokość = e.Location.X - pj_Punkt.X;
                        pj_Wysokość = pj_Szerokość;
                        pj_Rysownica.FillEllipse(pj_Pędzel, new Rectangle(pj_Punkt.X, e.Location.Y, pj_Szerokość, pj_Szerokość));

                        Kolo noweKolo = new Kolo();
                        noweKolo.pj_Szerokosc = pj_Szerokość;
                        noweKolo.pj_Wysokosc = pj_Wysokość;
                        noweKolo.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        noweKolo.pj_Wypelniony = true;
                        noweKolo.pj_Pedzel = pj_Pędzel;
                        noweKolo.pj_Color = kolor;
                        noweKolo.pj_Grubosc = grubosc;
                        pj_lista_figur.Add(noweKolo);
                        pj_imgRysownica.Refresh();

                        pj_LOG("Dodano wypelniony okrag");


                    }
                    if (pj_rdbFillElipsa.Checked)
                    {
                        pj_Szerokość = e.Location.X - pj_Punkt.X;
                        pj_Wysokość = e.Location.Y - pj_Punkt.Y;
                        pj_Rysownica.FillEllipse(pj_Pędzel, new Rectangle(pj_Punkt.X, pj_Punkt.Y, pj_Szerokość, pj_Wysokość));

                        Kolo noweKolo = new Kolo();
                        noweKolo.pj_Szerokosc = pj_Szerokość;
                        noweKolo.pj_Wysokosc = pj_Wysokość;
                        noweKolo.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        noweKolo.pj_Wypelniony = true;
                        noweKolo.pj_Pedzel = pj_Pędzel;
                        noweKolo.pj_Color = kolor;
                        pj_lista_figur.Add(noweKolo);

                        pj_imgRysownica.Refresh();

                        pj_LOG("Dodano wypelniona elipsa");
                    }
                    if (pj_rdbPelnyKwadrat.Checked)
                    {
                        pj_Szerokość = Math.Abs(e.Location.X - pj_Punkt.X);
                        pj_Wysokość = pj_Szerokość;
                        pj_Punkt.X = Math.Min(pj_Punkt.X, e.Location.X);
                        pj_Punkt.Y = Math.Min(pj_Punkt.Y, e.Location.Y);
                        pj_Rysownica.FillRectangle(pj_Pędzel, new Rectangle(pj_Punkt.X, pj_Punkt.Y, pj_Szerokość, pj_Szerokość));
                        pj_imgRysownica.Refresh();

                        Kwadrat nowyKwadrat = new Kwadrat();
                        nowyKwadrat.pj_Bok = pj_Szerokość;
                        nowyKwadrat.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        nowyKwadrat.pj_Wypelniony = true;
                        nowyKwadrat.pj_Pedzel = pj_Pędzel;
                        nowyKwadrat.pj_Color = kolor;
                        pj_lista_figur.Add(nowyKwadrat);

                        pj_imgRysownica.Refresh();

                        pj_LOG("Dodano wypelniony kwadrat");
                    }
                    if (pj_rdbPelnyProstokat.Checked)
                    {
                        pj_Szerokość = Math.Abs(e.Location.X - pj_Punkt.X);
                        pj_Wysokość = Math.Abs(e.Location.Y - pj_Punkt.Y); ;
                        pj_Punkt.X = Math.Min(pj_Punkt.X, e.Location.X);
                        pj_Punkt.Y = Math.Min(pj_Punkt.Y, e.Location.Y);
                        pj_Rysownica.FillRectangle(pj_Pędzel, new Rectangle(pj_Punkt.X, pj_Punkt.Y, pj_Szerokość, pj_Wysokość));

                        Prostokat nowyProstokat = new Prostokat();
                        nowyProstokat.pj_Szerokosc = pj_Szerokość;
                        nowyProstokat.pj_Wysokosc = pj_Wysokość;
                        nowyProstokat.pj_Pedzel = pj_Pędzel;
                        nowyProstokat.pj_Wypelniony = true;
                        nowyProstokat.pj_Color = kolor;
                        nowyProstokat.pj_UstawXY(pj_Punkt.X, pj_Punkt.Y);
                        pj_lista_figur.Add(nowyProstokat);
                        pj_imgRysownica.Refresh();


                        pj_LOG("Dodano wypelniony prostokat");

                    }
                }
                if (pj_rdbGumka.Checked)
                {

                }
                if (pj_rdbOlówek.Checked != true && pj_rdbFigury.Checked != true && pj_rdbGumka.Checked != true)
                {
                    pj_error.SetError(pj_grbNarzedzia, "Musisz wybrać opcję");
                }
                else
                    pj_error.Dispose();
                
            }
            
        }
        void pj_wyczysc_plotno()
        {
            pj_Rysownica.Clear(Color.White);
            pj_imgRysownica.Refresh();
        }
        private void pj_btnWymaz_Click(object sender, EventArgs e)
        {
            pj_imgKolortla.BackColor = Color.White;
            pj_imgWziernikLini.BackColor = Color.White;
            pj_wyczysc_plotno();
            pj_wyczysc_i_usun();
            
        }
        private static int pj_tick = Environment.TickCount;

        // blokuje watek przed zwracaniem takich samych wartosci
        private static ThreadLocal<Random> pj_losowacz = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref pj_tick)));
        public static Random pj_my_random()
        {
            return pj_losowacz.Value;
        }
        public int pj_getLosowaGrubosc()
        {
            return pj_my_random().Next(1, 11);

        }
        public System.Drawing.Drawing2D.DashStyle pj_get_RodzajLinii()
        {


            DashStyle rodzaj = DashStyle.Solid;

            switch (pj_my_random().Next(1, 6))
            {
                case 1:
                    rodzaj = System.Drawing.Drawing2D.DashStyle.Dash;
                    break;

                case 2:
                    rodzaj = System.Drawing.Drawing2D.DashStyle.DashDot;
                    break;

                case 3:
                    rodzaj = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                    break;
                case 4:
                    rodzaj = System.Drawing.Drawing2D.DashStyle.Dot;
                    break;
                case 5:
                    rodzaj = System.Drawing.Drawing2D.DashStyle.Solid;
                    break;
            }

            return rodzaj;
        }
        public Color pj_getLosowyKolor()
        {

            Color losowy;

            losowy = Color.FromArgb(

                      pj_my_random().Next(0, 255),
                      pj_my_random().Next(0, 255),
                      pj_my_random().Next(0, 255)
                );

            return losowy;

        }
        void pj_losowe_polozenie()
        {
            int Xmax = pj_imgRysownica.Width;
            int Ymax = pj_imgRysownica.Height;

            int Xp, Yp;

            foreach (TFigura figura in pj_lista_figur)
            {
                
                Xp = pj_my_random().Next(pj_margin, Xmax - pj_margin);
                Yp = pj_my_random().Next(pj_margin, Ymax - pj_margin);

                figura.pj_X = Xp;
                figura.pj_Y = Yp;

            } 


        }
        void pj_odswiez_i_odrysuj()
        {
            pj_Rysownica.Clear(pj_imgKolortla.BackColor);
            pj_imgRysownica.Refresh();

            

            foreach (TFigura o in pj_lista_figur)
            {

                // odrysuj figury zgodnie z parametrami 
                if (o is Kwadrat)
                {
                    Kwadrat p = (Kwadrat)o;
                    pj_LOG("rysuje kwadrat");

                    if (p.pj_Wypelniony)
                    {
                        pj_Rysownica.FillRectangle(new SolidBrush(o.pj_Color), 
                            new Rectangle(p.pj_X, p.pj_Y, p.pj_Bok, p.pj_Bok));
                    
                    }
                    else
                    {
                        pj_Pióro.DashStyle = pj_get_RodzajLinii();
                        o.pj_Rodzaj_Linii = pj_Pióro.DashStyle;
                        pj_Pióro.Color = o.pj_Color;
                        pj_Pióro.Width = o.pj_Grubosc;
                        pj_Rysownica.DrawRectangle(pj_Pióro, 
                            new Rectangle(p.pj_X, p.pj_Y, p.pj_Bok, p.pj_Bok));

                    }
                        
                }
                else
                    if (o is Prostokat)
                    {
                        Prostokat p = (Prostokat)o;
                        pj_LOG("rysuje prostokat " + o.pj_opisz());

                        if (o.pj_Wypelniony)
                        {
                            pj_Rysownica.FillRectangle(new SolidBrush(o.pj_Color), new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                        }
                        else
                        {
                            pj_Pióro.DashStyle = pj_get_RodzajLinii();
                            o.pj_Rodzaj_Linii = pj_Pióro.DashStyle;
                            pj_Pióro.Color = o.pj_Color;
                            pj_Pióro.Width = o.pj_Grubosc;
                            pj_Rysownica.DrawRectangle(pj_Pióro, new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                        }


                    }
                    else
                        if (o is Linia)
                        {
                            Linia p = (Linia)o;
                            pj_Pióro.DashStyle = pj_get_RodzajLinii();
                            o.pj_Rodzaj_Linii = pj_Pióro.DashStyle;
                            pj_Pióro.Color = o.pj_Color;
                            pj_Pióro.Width = o.pj_Grubosc;
                            pj_Rysownica.DrawLine(pj_Pióro, p.pj_X, p.pj_Y, p.pj_X + p.pj_Szerokosc, p.pj_Y + p.pj_Wysokosc);



                            pj_LOG("rysuje linia");
                        }
                        else
                            if (o is Kolo)
                            {
                                Kolo p = (Kolo)o;
                                pj_LOG("rysuje kolo");


                                if (o.pj_Wypelniony)
                                    pj_Rysownica.FillEllipse(new SolidBrush(o.pj_Color), new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                                else
                                {
                                    pj_Pióro.DashStyle = pj_get_RodzajLinii();
                                    o.pj_Rodzaj_Linii = pj_Pióro.DashStyle;
                                    pj_Pióro.Color = o.pj_Color;
                                    pj_Pióro.Width = o.pj_Grubosc;
                                    pj_Rysownica.DrawEllipse(pj_Pióro, 
                                        new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                                }
                                    

                            }
            }
            pj_imgRysownica.Refresh();



        }
        void pj_odswiez_i_odrysuj_1()
        {
            pj_Rysownica.Clear(pj_imgKolortla.BackColor);
            pj_imgRysownica.Refresh();
            Pen pen;
            pen = new Pen(Color.Red, 1);

            foreach (TFigura o in pj_lista_figur)
            {

                // odrysuj figury zgodnie z parametrami 
                if (o is Kwadrat)
                {
                    Kwadrat p = (Kwadrat)o;
                    pj_LOG("rysuje kwadrat");

                    if (p.pj_Wypelniony)
                    {
                        pj_Rysownica.FillRectangle(new SolidBrush(o.pj_Color),
                            new Rectangle(p.pj_X, p.pj_Y, p.pj_Bok, p.pj_Bok));

                    }
                    else
                    {
                        pen.DashStyle = o.pj_Rodzaj_Linii;
                        pen.Color = o.pj_Color;
                        pen.Width = o.pj_Grubosc;
                        pj_Rysownica.DrawRectangle(pen,
                            new Rectangle(p.pj_X, p.pj_Y, p.pj_Bok, p.pj_Bok));

                    }

                }
                else
                    if (o is Prostokat)
                    {
                        Prostokat p = (Prostokat)o;
                        pj_LOG("rysuje prostokat " + o.pj_opisz());

                        if (o.pj_Wypelniony)
                        {
                            pj_Rysownica.FillRectangle(new SolidBrush(o.pj_Color), new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                        }
                        else
                        {
                            pen.DashStyle = o.pj_Rodzaj_Linii;
                            pen.Color = o.pj_Color;
                            pen.Width = o.pj_Grubosc;
                            pj_Rysownica.DrawRectangle(pen, new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                        }


                    }
                    else
                        if (o is Linia)
                        {
                            Linia p = (Linia)o;
                            pen.DashStyle = o.pj_Rodzaj_Linii;
                            pen.Color = o.pj_Color;
                            pen.Width = o.pj_Grubosc;
                            pj_Rysownica.DrawLine(pen, p.pj_X, p.pj_Y, p.pj_X + p.pj_Szerokosc, p.pj_Y + p.pj_Wysokosc);



                            pj_LOG("rysuje linia");
                        }
                        else
                            if (o is Kolo)
                            {
                                Kolo p = (Kolo)o;
                                pj_LOG("rysuje kolo");


                                if (o.pj_Wypelniony)
                                    pj_Rysownica.FillEllipse(new SolidBrush(o.pj_Color), new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                                else
                                {
                                    pen.DashStyle = o.pj_Rodzaj_Linii;
                                    pen.Color = o.pj_Color;
                                    pen.Width = o.pj_Grubosc;
                                    pj_Rysownica.DrawEllipse(pen,
                                        new Rectangle(p.pj_X, p.pj_Y, p.pj_Szerokosc, p.pj_Wysokosc));
                                }


                            }
            }
            pj_imgRysownica.Refresh();

        }
        private void pj_btnNowePolozenie_Click(object sender, EventArgs e)
        {
            pj_losowe_polozenie();
            pj_odswiez_i_odrysuj_1();
        }
        void pj_losowe_atrybuty_graficzne()
        {


            foreach (TFigura o in pj_lista_figur)
            {
                Random los = new Random();
                DashStyle styllini;
                o.pj_Grubosc = pj_getLosowaGrubosc();
                o.pj_Color = pj_getLosowyKolor();
                
                o.pj_Rodzaj_Linii = pj_get_RodzajLinii();
            }
        }
        private void pj_btnNowePolozenieIAtrybuty_Click(object sender, EventArgs e)
        {
            pj_losowe_polozenie();
            pj_losowe_atrybuty_graficzne();
            pj_odswiez_i_odrysuj();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pj_txtRozmiarGumki.Text = trackBar1.Value.ToString();
        }

        private void pj_btnNoweAtrybuty_Click(object sender, EventArgs e)
        {
            pj_losowe_atrybuty_graficzne();
            pj_odswiez_i_odrysuj();
        }
        
        private void pj_btnWyjscie_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void pj_wyczysc_i_usun()
        {
            pj_wyczysc_plotno();

            pj_lista_figur.Clear();
        }
        private void PaintJaskula44185_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult Pytanie = MessageBox.Show("Jesteś pewien ze skończyłeś malować?",
                this.Text,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            if (Pytanie == DialogResult.Yes)
                e.Cancel = false;
            else
                if (Pytanie == DialogResult.No)
                    e.Cancel = true;
                else
                    e.Cancel = true;
        }

    }
}
