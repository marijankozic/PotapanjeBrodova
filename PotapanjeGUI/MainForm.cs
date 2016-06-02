using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PotapanjeBrodova;
using System.Threading;

namespace PotapanjeGUI
{
    public partial class MainForm : Form
    {
        // ja
        Flota flota;
        FlotaGUI mojaFlotaGUI;
        AITemplate ai;

        // protivnik
        FlotaNeprijatelj protivnikFlotaGUI;
        OOMPotapanje.Protivnik protivnik;

        public MainForm() {
            InitializeComponent();

        }

        private void btnFight_Click(object sender, EventArgs e) {
            // na pocetku ukloni stare flote ako postoje
            if (mojaFlotaGUI != null) this.Controls.Remove(mojaFlotaGUI);
            if (protivnikFlotaGUI != null) this.Controls.Remove(protivnikFlotaGUI);

            BrodograditeljTemplate brodograditelj = BrodograditeljFactory.DajBrodograditelja();
            int[] duljine = SastaviListuBrodova();
            int redaka = (int)numRedaka.Value;
            int stupaca = (int)numStupaca.Value;
            flota = brodograditelj.SloziFlotu(redaka, stupaca, duljine);
            mojaFlotaGUI = new FlotaGUI(redaka, stupaca);
            mojaFlotaGUI.sirina = 250;
            mojaFlotaGUI.Location = new Point(20, 170);
            mojaFlotaGUI.ZadajFlotu(flota);
            this.Controls.Add(mojaFlotaGUI);

            protivnikFlotaGUI = new FlotaNeprijatelj(redaka, stupaca);
            protivnikFlotaGUI.sirina = 250;
            protivnikFlotaGUI.Location = new Point(370, 170);
            this.Controls.Add(protivnikFlotaGUI);
            ai = AIFactory.DajAI();
            ai.Initialize(redaka, stupaca, duljine);

            // iniciraj protivnika
            protivnik = new OOMPotapanje.Protivnik(redaka, stupaca, duljine);

            // zapocni igru
            ProgressChangedEventArgs x;
            bgWorker.RunWorkerAsync();
        }

        public void Igraj() {
            rezultatGadjanja rez = rezultatGadjanja.nepoznato;
            while (true) {

                // za pocetak mi igramo prvi, dok ne smislimo bolji algoritam
                Polje p = ai.Gadjaj();
                rez = (rezultatGadjanja)protivnik.JaviRezultat(new Tuple<int, int>(p.Redak, p.Stupac));
                if (rez == rezultatGadjanja.PORAZ) {
                    this.tbDnevnik.Text = "Mi smo pobjedili!";
                    break;
                }
                this.Invoke((MethodInvoker)delegate { tbDnevnik.AppendText("(" + p.Redak + ", " + p.Stupac + "): " + rez.ToString()+"\n"); });
                Brod b = new Brod(ai.zap.trenutnaMeta);
                ai.ObradiPogodak(rez);
                protivnikFlotaGUI.DodajPolje(p, rez);
                if (rez == rezultatGadjanja.potopljen) {
                    protivnikFlotaGUI.PotopiBrod(b);
                }
                this.Invoke((MethodInvoker)delegate
                 { protivnikFlotaGUI.Invalidate(); });

                // sada igra protivnik
                Tuple<int, int> koord = protivnik.Gadjaj();
                p = new Polje(koord.Item1, koord.Item2);
                rez = flota.ObradiPogodak(p.Redak, p.Stupac);
                if (rez == rezultatGadjanja.PORAZ) {
                    this.tbDnevnik.Text = "Protivnik je pobjedio!";
                    break;
                }
                this.Invoke((MethodInvoker)delegate { tbDnevnik.AppendText("@@ (" + p.Redak + ", " + p.Stupac + "): " + rez.ToString() + "\n"); });
                protivnik.ObradiPogodak((OOMPotapanje.mojRezultatGadjanja)rez);
                this.Invoke((MethodInvoker)delegate { mojaFlotaGUI.ZadajFlotu(flota); });
                

                // spavaj da ne bude prebrzo
                Thread.Sleep(500);
            }
        }

        private int[] SastaviListuBrodova() {
            int redaka = (int)numRedaka.Value;
            int stupaca = (int)numStupaca.Value;
            List<int> duljine = new List<int>();
            for (int i = 0; i < numBrod5.Value; i++) {
                duljine.Add(5);
            }
            for (int i = 0; i < numBrod4.Value; i++) {
                duljine.Add(4);
            }
            for (int i = 0; i < numBrod3.Value; i++) {
                duljine.Add(3);
            }
            for (int i = 0; i < numBrod2.Value; i++) {
                duljine.Add(2);
            }
            return duljine.ToArray();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e) {
            Igraj();
        }
    }
}
