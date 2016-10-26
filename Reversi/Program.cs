using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Settings : Form
    {
        public int blokjes;
        private TextBox groote;
        private Label grooteLabel;
        public Settings()
        {

            this.Size = new Size(300, 150);

            Button klaar;
            klaar = new Button();
            klaar.Location = new Point(100, 50);
            klaar.Size = new Size(80, 60);
            klaar.Text = "Klaar";

            this.groote = new TextBox();
            this.groote.Location = new Point(100, 10);
            this.grooteLabel = new Label();
            this.grooteLabel.Location = new Point(30, 10);
            grooteLabel.Text = "Veld groote:";

            this.Controls.Add(klaar);
            this.Controls.Add(groote);
            this.Controls.Add(grooteLabel);

            klaar.Click += Klaar_Click;
        }

        public void storegroote()
        {
            try
            {
                if (int.Parse(groote.Text) < 25 && int.Parse(groote.Text) >= 3)
                {
                    blokjes = int.Parse(groote.Text);
                    this.Close();


                }

                else if (int.Parse(groote.Text) > 25)
                {
                    MessageBox.Show("Het getal dat je ingevoerd hebt is te groot!",
                       "Ooh nee! Wat nu?",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1);


                    Refresh();
                }
                else if (int.Parse(groote.Text) >= 0 && int.Parse(groote.Text) < 3)
                {
                    MessageBox.Show("Het getal is te klein!",
                      "Jij wilt zeker snel winnen?",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error,
                      MessageBoxDefaultButton.Button1);


                    Refresh();
                }

                else
                {
                    MessageBox.Show("Het getal dat je ingevoerd hebt is negatief!",
                    "Tjongejonge, Wat een negatieviteit!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);



                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Voer een getal in!",
                "Ooh the humanity",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void Klaar_Click(object sender, EventArgs e)
        {
            storegroote();

        }
    }
    class Veld : Form
    {
        public int n = 20;
        int beurt = 1, aantalblauw = 2, aantalrood = 2;
        int lengte = 400, breedte = 400;
        private Label LabelBlauw, LabelRood;
        Button[,] veld;
        public Veld()
        {
            veld = new Button[n, n];
            this.Size = new Size(lengte, breedte);


            bool[,] gespeeld = new bool[n, n];


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i == (n / 2 - 1) && j == (n / 2 - 1)) || (i == n / 2 && j == n / 2))  // Rode beginstenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.Red;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                    }
                    else if ((i == n / 2 && j == (n / 2 - 1)) || (i == (n / 2 - 1) && j == n / 2)) // Blauwe beginstenen plaatsen
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
            settings.Location = new Point(n / 2 * 50);
            settings.Text = "Settings";

            this.LabelBlauw = new Label();
            this.LabelBlauw.Location = new Point(n / 10 * 50, 12);
            LabelBlauw.Text = "Blauw:" + aantalblauw;

            this.LabelRood = new Label();
            this.LabelRood.Location = new Point(n * 40, 12);
            LabelRood.Text = "Rood:" + aantalrood;

            this.Size = new Size(n * 50 + 100, n * 50 + 100);

            this.Controls.Add(settings);
            this.Controls.Add(LabelBlauw);
            this.Controls.Add(LabelRood);

            settings.Click += Settings_Click;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Settings SettingsButton = new Settings();
            SettingsButton.ShowDialog();

            this.Invalidate();

            for (int i = 0; i < n; i++)                 // verwijderen van de knoppen
            {
                for (int j = 0; j < n; j++)
                {
                    Controls.Remove(veld[i, j]);
                }
            }
            n = SettingsButton.blokjes;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i == (n / 2 - 1) && j == (n / 2 - 1)) || (i == n / 2 && j == n / 2))  // Rode beginstenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.Red;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                    }
                    else if ((i == n / 2 && j == (n / 2 - 1)) || (i == (n / 2 - 1) && j == n / 2)) // Blauwe beginstenen plaatsen
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
        }

        public void veldkleur(object sender, EventArgs e)
        {


            Button myButton = sender as Button;

            if (myButton.BackColor != Color.Red && myButton.BackColor != Color.Blue)            // controle of de steen al een keer gespeeld is
            {
                if (beurt % 2 == 1)
                {
                    myButton.BackColor = Color.Red;
                    aantalrood += 1;
                    beurt++;
                    LabelRood.Text = "Rood:" + aantalrood;
                }
                else if (beurt % 2 == 0)
                {
                    myButton.BackColor = Color.Blue;
                    aantalblauw += 1;
                    beurt++;
                    LabelBlauw.Text = "Blauw:" + aantalblauw;

                }
            }
        }

    }

    class Spel
    {
        Spel()
        {

        }

        public bool checkBuren()
        {
            for (int i = 0; i < 8; i++)
            {
                checkBuur(i);
            }
            return true;
        }

        public void checkBuur(int buur)
        {
            switch (buur)
            {
                case 0:
                    //veldx-1, veldy-1
                    break;
                case 1:
                    //veldy-1
                    break;
                case 2:
                    //veldx+1, veldy-1
                    break;
                case 3:
                    //veldx+1
                    break;
                case 4:
                    //veldx+1, veldy+1
                    break;
                case 5:
                    //veldy+1
                    break;
                case 6:
                    //veldx-1, veldy+1
                    break;
                case 7:
                    //veldx-1
                    break;
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
