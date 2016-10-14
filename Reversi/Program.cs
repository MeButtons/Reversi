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
            Button[,] veld = new Button[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {

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
