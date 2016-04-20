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

namespace PotapanjeGUI
{
    public partial class MainForm : Form
    {
        
        Flota flota;
        FlotaGUI mojaFlotaGUI;

        public MainForm() {
            InitializeComponent();
            
        }

        private void btnFight_Click(object sender, EventArgs e) {
            if (mojaFlotaGUI != null) this.Controls.Remove(mojaFlotaGUI);
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
    }
}
