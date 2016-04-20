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
    public partial class FlotaGUI : UserControl
    {
        Flota flota;
        int redaka;
        int stupaca;
        public int sirina { get; set; }

        public FlotaGUI(int redaka, int stupaca) {
            this.redaka = redaka;
            this.stupaca = stupaca;
            InitializeComponent();
        }

        public void ZadajFlotu(Flota flota) {
            this.flota = flota;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            NacrtajPozadinu(e);
            NacrtajMrezu(e);
            if (flota != null)
                NacrtajFlotu(e);
            base.OnPaint(e);
        }

        private void NacrtajFlotu(PaintEventArgs e) {
            foreach (Brod b in this.flota.Brodovi) {
                foreach (Polje p in b.Polja) {
                    NacrtajPolje(e, p);
                }
            }
        }

        private void NacrtajPolje(PaintEventArgs e, Polje polje) {
            Brush boja = new SolidBrush(Color.Navy);
            Pen pen = new Pen(SystemColors.ActiveBorder);
            int redak = polje.Redak;
            int stupac = polje.Stupac;
            int x = redak * (sirina / redaka);
            int y = stupac * (sirina / stupaca);
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
