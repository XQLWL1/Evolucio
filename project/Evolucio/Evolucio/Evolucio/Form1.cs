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

        //győztes kiválasztása
        Brain winnerBrain;

        public Form1()
        {
            InitializeComponent();
            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);


            /*egy játékos hozzáadása
            gc.AddPlayer();

            játék elindítása, gépi vezérlés
            gc.Start();

            játék indítása. Ha mi akarjuk iránytani a játékost:
            gc.Start(true);*/


            //a játék végéhez eseménykezelő létrehozása
            gc.GameOver += Gc_GameOver;

            //több játékos hozzáadása és elindítása
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }

            gc.Start();
        }

        private void Gc_GameOver(object sender)
        {
            //generációk számának növelése
            generation++;
            //hányadik generációról van szó? labelre kiírni:
            generationLabel.Text = string.Format("{0}. generáció", generation);

            //lista létrehozása,mely tartalmazza, hogy ki a legjobb játékos?. Csökkenő sorrendbe kell rendezni
            var playerList = (from p in gc.GetCurrentPlayers()
                              orderby p.GetFitness() descending
                              select p);

            //hogyan lehet a legjobbakat megkapni?
            var topplayers = playerList.Take(populationSize / 2).ToList();

            /*az 50 legjobb játékos megy a kövi körbe
             * az új körben is 100-an indulnak, akik közül 50-en úgy viselkedik, mint az előző körben lévő legjobb 50*/
            gc.ResetCurrentLevel();

            foreach (Player p in topplayers)
            {
                //játékosok agyának klónozása
                Brain b = p.Brain.Clone();


                /*Ha a generáció 3-al osztva 0 maradékot ad, akkor meghívja a ExpandBrain (létrehoz egy új játékost és kiterjeszti az agyát) függvényt
                 * ha hamis, akkor csak hozzáadjuk az agy alapjá*/
                if (generation % 3 == 0)
                {
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                }

                else
                {
                    gc.AddPlayer(b);
                }

                /*Ha a generáció 3-al osztva 0 maradékot ad, akkor meghívja a ExpandBrain (létrehoz egy új játékost és kiterjeszti az agyát) függvényt
                 * ha nem osztható 3-mal, akkor nem lesz kiterjesztve az agya */

                if (generation % 3 == 0)
                {
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                }
                else
                {
                    gc.AddPlayer(b.Mutate());
                }
                    
            }

            //újabb kör indítása
            gc.Start();

            //hányan nyerték meg?
            var winners = from p in topplayers
                          where p.IsWinner
                          select p;

            if (winners.Count()>0)
            {
                winnerBrain = winners.FirstOrDefault().Brain.Clone();
                gc.GameOver -= Gc_GameOver;
                return;
            }
        }

    }
}
