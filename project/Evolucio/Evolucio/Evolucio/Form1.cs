using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace Evolucio
{
    public partial class Form1 : Form
    {

        //referenciához hozzá lett adva a dll, ezért itt fogunk tudni csinálni egy GameController osztályt a form 1 szintjén

        GameController gc = new GameController();
        GameArea ga = new GameArea();

        //Induló populáció felépítése
        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;

        public Form1()
        {
            InitializeComponent();
            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);


            //egy játékos hozzáadása
            //gc.AddPlayer();

            //játék elindítása, gépi vezérlés
            //gc.Start();

            //játék indítása. Ha mi akarjuk iránytani a játékost:
            //gc.Start(true);

            //több játékos hozzáadása
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }

            gc.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
