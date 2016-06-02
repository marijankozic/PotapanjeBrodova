using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PotapanjeBrodova;

namespace PotapanjeGUI
{
    public partial class FlotaNeprijatelj : UserControl
    {
        // Neprijateljska polja pratimo u dictionaryu: key=polje, value=rezultat
        Dictionary<Polje, rezultatGadjanja> gadjanaPolja;
        int redaka;
        int stupaca;
        public int sirina { get; set; }

        public FlotaNeprijatelj(int redaka, int stupaca) {
            this.redaka = redaka;
            this.stupaca = stupaca;
            gadjanaPolja = new Dictionary<Polje, rezultatGadjanja>();
            InitializeComponent();
        }

        public void DodajPolje(Polje p, rezultatGadjanja rez) {
            try { gadjanaPolja.Add(p, rez); }
            catch { gadjanaPolja[p] = rez; }
        }

        protected override void OnPaint(PaintEventArgs e) {
            NacrtajPozadinu(e);
            NacrtajMrezu(e);
            if (gadjanaPolja.Count>0)
                NacrtajPolja(e);
            base.OnPaint(e);
        }

        // kada je brod potopljen, treba pozvati ovu metodu izvana kako bi
        // neprijateljska flota znala retroaktivno postaviti brodska polja na potopljena
        public void PotopiBrod(Brod brod) {
            foreach (Polje polje in brod.Polja) {
                gadjanaPolja[polje] = rezultatGadjanja.potopljen;
            }
        }

        // ovo crta sva polja iz rijecnika do sada gadjanih polja
        private void NacrtajPolja(PaintEventArgs e) {
            foreach (var polje in gadjanaPolja) {
                NacrtajPolje(e, polje);
            }
        }

        private void NacrtajPolje(PaintEventArgs e,  KeyValuePair<Polje,rezultatGadjanja> polje) {
            Brush boja = new SolidBrush(SystemColors.ActiveBorder);
            switch (polje.Value) {
                case rezultatGadjanja.promasaj:
                    boja = new SolidBrush(Color.Gray);
                    break;
                case rezultatGadjanja.pogodak:
                    boja = new SolidBrush(Color.Red);
                    break;
                case rezultatGadjanja.potopljen:
                    boja = new SolidBrush(Color.DarkRed);
                    break;
                default:
                    break;
            }
            
            Pen pen = new Pen(SystemColors.ActiveBorder);
            int redak = polje.Key.Redak;
            int stupac = polje.Key.Stupac;
            int y = redak * (sirina / redaka);
            int x = stupac * (sirina / stupaca);
            e.Graphics.FillRectangle(boja, x, y, (sirina / redaka), (sirina / stupaca));
            e.Graphics.DrawRectangle(pen, x, y, (sirina / redaka), (sirina / stupaca));
        }

        private void NacrtajMrezu(PaintEventArgs e) {
            Pen pen = new Pen(SystemColors.ActiveBorder);
            int visinaPolja = sirina / redaka;
            int sirinaPolja = sirina / stupaca;
            int y1 = 0;
            for (int r = 0; r <= redaka; ++r) {
                e.Graphics.DrawLine(pen, 0, y1, sirina, y1);
                y1 += visinaPolja;
            }

            int x1 = 0;
            for (int s = 0; s <= stupaca; ++s) {
                e.Graphics.DrawLine(pen, x1, 0, x1, sirina);
                x1 += sirinaPolja;
            }
        }

        private void NacrtajPozadinu(PaintEventArgs e) {
            e.Graphics.FillRectangle(SystemBrushes.Window, 0, 0, sirina, sirina);
        }
    }
}
