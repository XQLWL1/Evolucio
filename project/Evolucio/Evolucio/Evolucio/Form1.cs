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

        public Form1()
        {
            InitializeComponent();
            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);


            //játékos hozzáadása
            gc.AddPlayer();

            //játék elindítása
            //gc.Start();

            //játék indítása. Ha mi akarjuk iránytani a játékost:
            //gc.Start(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
