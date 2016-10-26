using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Veld : Form
    {
        bool zetMag = false;
        int beurt = 1, aantalblauw=2, aantalrood=2;
        int lengte = 400, breedte = 400;
        private Label LabelBlauw, LabelRood;
        Button[,] veld;

        public Veld()
        {
            this.Size = new Size(lengte,breedte);
            int n = 4;
          
            veld = new Button[n, n];

            bool[,] gespeeld = new bool[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i == (n / 2-1) && j == (n / 2-1)) || (i == n / 2 && j == n / 2))  // Rode beginstenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.Red;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                    }
                    else if ((i == n / 2 && j == (n / 2-1)) || (i == (n / 2-1) && j == n / 2)) // Blauwe beginstenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.Blue;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                    }
                    else                                                                       // "Neutrale" stenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.LightGray;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                    }
                    this.Controls.Add(veld[i, j]);
                    veld[i, j].Click += veldkleur;
                    
                 }


            }
            Button settings = new Button();
            settings.Size = new Size(80, 50);
            settings.Location = new Point((n + 1) * 50);
            settings.Text = "Settings";

            this.LabelBlauw = new Label();
            this.LabelBlauw.Location = new Point(25, 12);
            LabelBlauw.Text = "Blauw:" + aantalblauw;

            this.LabelRood = new Label();
            this.LabelRood.Location = new Point(200, 12);
            LabelRood.Text = "Rood:" + aantalrood;


            this.Controls.Add(settings);
            this.Controls.Add(LabelBlauw);
            this.Controls.Add(LabelRood);
 
        }

        public bool checkZet(int bekijkX, int bekijkY)
        {
            int lengteRij = 0;
            for (int i = 0; i < 8; i++)
            {
                checkBuur(i, bekijkX, bekijkY, lengteRij);
                lengteRij = 0;
            }
            if (zetMag == false)
                return false;
            return true;
        }

        public void checkBuur(int buur, int bekijkX, int bekijkY, int lengteRij)
        {
            Color KleurSpeler;
            Color kleurTegenstander;
            if (beurt % 2 == 1)
            {
                kleurTegenstander = Color.Blue;
                KleurSpeler = Color.Red;
            }
            else
            {
                kleurTegenstander = Color.Red;
                KleurSpeler = Color.Blue;
            }
            switch (buur)
            {
                case 0:
                    if (veld[bekijkX - 1, bekijkY - 1].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX - 1, bekijkY - 1, lengteRij);
                    }
                    if (veld[bekijkX - 1, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 1:
                    if (veld[bekijkX, bekijkY - 1].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX, bekijkY - 1, lengteRij);
                    }
                    if (veld[bekijkX, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 2:
                    if (veld[bekijkX + 1, bekijkY - 1].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX + 1, bekijkY - 1, lengteRij);
                    }
                    if (veld[bekijkX + 1, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 3:
                    if (veld[bekijkX + 1, bekijkY].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX + 1, bekijkY, lengteRij);
                    }
                    if (veld[bekijkX + 1, bekijkY].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 4:
                    if (veld[bekijkX + 1, bekijkY + 1].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX + 1, bekijkY + 1, lengteRij);
                    }
                    if (veld[bekijkX + 1, bekijkY + 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 5:
                    if (veld[bekijkX, bekijkY + 1].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX, bekijkY + 1, lengteRij);
                    }
                    if (veld[bekijkX, bekijkY + 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 6:
                    if (veld[bekijkX - 1, bekijkY + 1].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX - 1, bekijkY + 1, lengteRij);
                    }
                    if (veld[bekijkX - 1, bekijkY + 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
                case 7:
                    if (veld[bekijkX - 1, bekijkY].BackColor == kleurTegenstander)
                    {
                        lengteRij++;
                        checkBuur(buur, bekijkX - 1, bekijkY, lengteRij);
                    }
                    if (veld[bekijkX - 1, bekijkY].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
                    break;
            }

        }
       
        public void veldkleur(object sender, EventArgs e)
        {

            
            Button myButton = sender as Button;

            if (myButton.BackColor != Color.Red && myButton.BackColor != Color.Blue)            // controle of de steen al een keer gespeeld is
            {
                if (beurt%2 == 1)
                {
                    myButton.BackColor = Color.Red;
                    aantalrood += 1;
                    beurt++;
                    LabelRood.Text = "Rood:" + aantalrood;
                }
                else if (beurt%2 == 0)
                {
                    myButton.BackColor = Color.Blue;
                    aantalblauw += 1;
                    beurt++;
                    LabelBlauw.Text = "Blauw:" + aantalblauw;
                    
                }
            }
        }
                    
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Veld bord;
            bord = new Veld();
            Application.Run(bord);
        }
    }
}
