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

       public Veld()
        {
            int n = 4;
            Button[,] veld = new Button[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    veld[i, j] = new Button();
                    veld[i,j].Size = new Size(50,50);
                    veld[i,j].Location = new Point(i * 50, j * 50);

                    this.Controls.Add(veld[i,j]);
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
