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
        int beurt = 1, aantalblauw=2, aantalrood=2;
        int lengte = 400, breedte = 400;
        private Label LabelBlauw, LabelRood;
        public Veld()
        {
            this.Size = new Size(lengte,breedte);
            int n = 9;
            Button[,] veld = new Button[n, n];
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

        public void veldkleur(object sender, EventArgs e)
        {

            
            Button myButton = sender as Button;

            if (myButton.BackColor != Color.Red && myButton.BackColor != Color.Blue)            // controle of de steen al een keer gespeeld is
            {
                if (beurt == 1)
                {
                    myButton.BackColor = Color.Red;
                    aantalrood = aantalrood + 1;
                    beurt = 2;
                    LabelRood.Text = "Rood:" + aantalrood;
                }
                else if (beurt == 2)
                {
                    myButton.BackColor = Color.Blue;
                    aantalblauw = aantalblauw + 1;
                    beurt = 1;
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
