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
        int beurt = 1, aantalblauw = 2, aantalrood = 2, richting;//begin toestanden, richting is nodig voor het veranderen van kleuren
        int lengte = 400, breedte = 400;//grootte in eerste instantie
        int Pass = 0;//aantal keer gepast
        private Label LabelBlauw, LabelRood;//labels voor scores
        int n;//grootte van bord
        Button[,] veld;//veld van knoppen
        bool[,] zetMag, knopBezet;//zetMag slaat op of een bepaalde knop een geldige zet is, knop bezet slaat op of er al een steen staat op een bepaalde knop
        bool plekMag, hulpZet;//plekMag = zetMag hulp, hulpZet = toggle voor help

        public Veld()//constructor
        {
            //toewijzen van variabelen
            this.Size = new Size(lengte, breedte);
            n = 8;
            richting = 0;
            veld = new Button[n, n];
            zetMag = new bool[n, n];
            knopBezet = new bool[n, n];
            plekMag = false;
            hulpZet = false;
            bool[,] gespeeld = new bool[n, n];


            //alle knoppen toekennen en goedzetten
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    knopBezet[i, j] = false;
                    zetMag[i, j] = false;
                    if ((i == (n / 2 - 1) && j == (n / 2 - 1)) || (i == n / 2 && j == n / 2))  // Rode beginstenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.Red;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                        knopBezet[i, j] = true;
                    }
                    else if ((i == n / 2 && j == (n / 2 - 1)) || (i == (n / 2 - 1) && j == n / 2)) // Blauwe beginstenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.Blue;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                        knopBezet[i, j] = true;
                    }
                    else                                                                       // "Neutrale" stenen plaatsen
                    {
                        veld[i, j] = new Button();
                        veld[i, j].BackColor = Color.LightGray;
                        veld[i, j].Size = new Size(50, 50);
                        veld[i, j].Location = new Point(i * 50 + 50, j * 50 + 50);
                        veld[i, j].Tag = i; //x - coordinaat in array
                        veld[i, j].Name = Convert.ToString(j); //y - coordinaat in array
                    }
                    this.Controls.Add(veld[i, j]);
                    veld[i, j].Click += veldkleur; //clickevent veldkleur

                }



            }

            checkZetten();//controleerd alle mogelijke zetten

            //settings button gegevens
            Button settings = new Button();
            settings.Size = new Size(80, 50);
            settings.Location = new Point((n + 1) * 50);
            settings.Text = "Settings";

            //help button gegevens
            Button hulp = new Button();
            hulp.Size = new Size(80, 50);
            hulp.Location = new Point((n + 1) * 50, 50);
            hulp.Text = "Help";

            //score blauw
            this.LabelBlauw = new Label();
            this.LabelBlauw.Location = new Point(25, 12);
            LabelBlauw.Text = "Blauw:" + aantalblauw;

            //score rood
            this.LabelRood = new Label();
            this.LabelRood.Location = new Point(200, 12);
            LabelRood.Text = "Rood:" + aantalrood;

            //controls
            this.Controls.Add(hulp);
            this.Controls.Add(settings);
            this.Controls.Add(LabelBlauw);
            this.Controls.Add(LabelRood);

            settings.Click += Settings_Click; //settingsknop clickevent
            hulp.Click += hulp_Click; //hulpknop clickevent
        }

        //gaat iedere direkte buur van een knop af
        public bool checkBuren(int bekijkX, int bekijkY)
        {
            plekMag = false;
            int lengteRij = 0;
            for (int i = 0; i < 8; i++)
            {
                if (checkBuur(i, bekijkX, bekijkY, lengteRij))
                    return true;
                lengteRij = 0;
            }
            return false;
        }

        //controleerd of een zet mogelijk is gekeken vanuit een bepaalde richting
        public bool checkBuur(int buur, int bekijkX, int bekijkY, int lengteRij)
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
                    if (bekijkX > 0 && bekijkY > 0)
                    {
                        if (veld[bekijkX - 1, bekijkY - 1].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX - 1, bekijkY - 1, lengteRij);
                        }
                        else if (veld[bekijkX - 1, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 0;
                        }
                    }
                    break;
                case 1:
                    if (bekijkY > 0)
                    {
                        if (veld[bekijkX, bekijkY - 1].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX, bekijkY - 1, lengteRij);
                        }
                        else if (veld[bekijkX, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 1;
                        }
                    }
                    break;
                case 2:
                    if (bekijkX < n - 1 && bekijkY > 0)
                    {
                        if (veld[bekijkX + 1, bekijkY - 1].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX + 1, bekijkY - 1, lengteRij);
                        }
                        else if (veld[bekijkX + 1, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 2;
                        }
                    }
                    break;
                case 3:
                    if (bekijkX < n - 1)
                    {
                        if (veld[bekijkX + 1, bekijkY].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX + 1, bekijkY, lengteRij);
                        }
                        else if (veld[bekijkX + 1, bekijkY].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 3;
                        }
                    }
                    break;
                case 4:
                    if (bekijkX < n - 1 && bekijkY < n - 1)
                    {
                        if (veld[bekijkX + 1, bekijkY + 1].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX + 1, bekijkY + 1, lengteRij);
                        }
                        else if (veld[bekijkX + 1, bekijkY + 1].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 4;
                        }
                    }
                    break;
                case 5:
                    if (bekijkY < n - 1)
                    {
                        if (veld[bekijkX, bekijkY + 1].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX, bekijkY + 1, lengteRij);
                        }
                        else if (veld[bekijkX, bekijkY + 1].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 5;
                        }
                    }
                    break;
                case 6:
                    if (bekijkX > 0 && bekijkY < n - 1)
                    {
                        if (veld[bekijkX - 1, bekijkY + 1].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX - 1, bekijkY + 1, lengteRij);
                        }
                        else if (veld[bekijkX - 1, bekijkY + 1].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 6;
                        }
                    }
                    break;
                case 7:
                    if (bekijkX > 0)
                    {
                        if (veld[bekijkX - 1, bekijkY].BackColor == kleurTegenstander)
                        {
                            lengteRij++;
                            checkBuur(buur, bekijkX - 1, bekijkY, lengteRij);
                        }
                        else if (veld[bekijkX - 1, bekijkY].BackColor == KleurSpeler && lengteRij > 0)
                        {
                            plekMag = true;
                            richting = 7;
                        }
                    }
                    break;
            }
            if (plekMag)
                return true;
            return false;

        }

        //checkt waar zetten mogelijk zijn voor de beurtspeler
        public void checkZetten()
        {
            Color mogelijkeZet;
            if (beurt % 2 == 1)
                mogelijkeZet = Color.Pink;
            else
                mogelijkeZet = Color.LightBlue;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (checkBuren(i, j) && knopBezet[i, j] == false)
                    {
                        zetMag[i, j] = true;
                        if (hulpZet)
                            veld[i, j].BackColor = mogelijkeZet;
                        else
                            veld[i, j].BackColor = Color.LightGray;
                    }
            if (!checkZetMogelijk() && Pass > 1)
                winst();
        }

        //clickevent voor de help button, toggled het aanzetten van de hulp functie
        public void hulp_Click(object sender, EventArgs e)
        {
            hulpZet = !hulpZet;
            checkZetten();
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
        //clickevent voor het klikken op een lege knop
        public void veldkleur(object sender, EventArgs e)
        {
            Button myButton = sender as Button;
            int j = int.Parse(myButton.Name);
            int i = Convert.ToInt32(myButton.Tag);

            if (zetMag[i, j])
            {

                if (myButton.BackColor != Color.Red && myButton.BackColor != Color.Blue)            // controle of de steen al een keer gespeeld is
                {
                    if (beurt % 2 == 1)
                    {
                        myButton.BackColor = Color.Red;
                        aantalrood += 1;
                        LabelRood.Text = "Rood:" + aantalrood;
                        knopBezet[i, j] = true;
                    }
                    else if (beurt % 2 == 0)
                    {
                        myButton.BackColor = Color.Blue;
                        aantalblauw += 1;
                        LabelBlauw.Text = "Blauw:" + aantalblauw;
                        knopBezet[i, j] = true;

                    }
                    veranderKleurBuur(i, j);
                    veranderKleurBuur(i, j);
                    beurt++;
                    for (int k = 0; k < n; k++)
                        for (int l = 0; l < n; l++)
                            if (!knopBezet[k, l])
                            {
                                zetMag[k, l] = false;
                                veld[k, l].BackColor = Color.LightGray;
                            }
                    checkZetten();
                }
            }
        }

        //laat zien wie er gewonnen heeft
        public void winst()
        {
            if (aantalrood > aantalblauw)
                MessageBox.Show("Rood heeft gewonnen");
            else if (aantalblauw > aantalrood)
                MessageBox.Show("Blauw heeft gewonnen");
            else
                MessageBox.Show("Gelijk spel");
        }

        //verandert stenen naar beurtspeler kleur waar toepasselijk
        public void veranderKleurBuur(int i, int j)
        {
            Color kleurSpeler;
            Color kleurTegenstander;

            checkBuren(i, j);

            if (beurt % 2 == 1)
            {
                kleurSpeler = Color.Red;
                kleurTegenstander = Color.Blue;
            }

            else
            {
                kleurSpeler = Color.Blue;
                kleurTegenstander = Color.Red;
            }

            switch (richting)
            {
                case 0:
                    if (veld[i - 1, j - 1].BackColor == kleurTegenstander)
                        veranderKleurBuur(i - 1, j - 1);
                    break;
                case 1:
                    if (veld[i, j - 1].BackColor == kleurTegenstander)
                        veranderKleurBuur(i, j - 1);
                    break;
                case 2:
                    if (veld[i + 1, j - 1].BackColor == kleurTegenstander)
                        veranderKleurBuur(i + 1, j - 1);
                    break;
                case 3:
                    if (veld[i + 1, j].BackColor == kleurTegenstander)
                        veranderKleurBuur(i + 1, j);
                    break;
                case 4:
                    if (veld[i + 1, j + 1].BackColor == kleurTegenstander)
                        veranderKleurBuur(i + 1, j + 1);
                    break;
                case 5:
                    if (veld[i, j + 1].BackColor == kleurTegenstander)
                        veranderKleurBuur(i, j + 1);
                    break;
                case 6:
                    if (veld[i - 1, j + 1].BackColor == kleurTegenstander)
                        veranderKleurBuur(i - 1, j + 1);
                    break;
                case 7:
                    if (veld[i - 1, j].BackColor == kleurTegenstander)
                        veranderKleurBuur(i - 1, j);
                    break;
            }
            if (veld[i, j].BackColor != kleurSpeler)
                veld[i, j].BackColor = kleurSpeler;
            else
                return;
        }

        //checkt of er nog zetten mogelijk zijn
        public bool checkZetMogelijk()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (zetMag[i, j])
                    {
                        Pass = 0;
                        return true;
                    }
            beurt++;
            Pass++;
            return false;
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

/*if (veld[bekijkX, bekijkY - 1].BackColor == KleurSpeler && lengteRij > 0)
                    {
                        lengteRij--;
                        veld[bekijkX, bekijkY].BackColor = KleurSpeler;
                    }
*/
