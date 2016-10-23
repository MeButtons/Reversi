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
        int beurt = 1;
       
        public Veld()
        {
            int n = 4;
            Button[,] veld = new Button[n, n];
            bool[,] gespeeld = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    veld[i, j] = new Button();
                    veld[i, j].BackColor = Color.LightGray;
                    veld[i, j].Size = new Size(50, 50);
                    veld[i, j].Location = new Point(i * 50, j * 50);

                    this.Controls.Add(veld[i, j]);
                    veld[i, j].Click += veldkleur;
                    
                 }


            }
            Button settings = new Button();
            settings.Size = new Size(80, 50);
            settings.Location = new Point((n + 1) * 50);
            settings.Text = "Settings";
            this.Controls.Add(settings);
 
        }

        public void veldkleur(object sender, EventArgs e)
        {

            
            Button myButton = sender as Button;

            if (myButton.BackColor != Color.Red && myButton.BackColor != Color.Blue)
            {
                if (beurt == 1)
                {
                    myButton.BackColor = Color.Red;

                    beurt = 2;
                }
                else if (beurt == 2)
                {
                    myButton.BackColor = Color.Blue;
                    beurt = 1;
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
