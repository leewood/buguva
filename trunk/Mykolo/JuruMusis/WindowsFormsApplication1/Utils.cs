using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public static class PanelUtils
    {
        private static SettleBattleShips settleBattleShips = new SettleBattleShips();
        private static Panel mainPanel = new Panel();
        private static Panel playerMap = new Panel();
        private static bool allowAddShip = false;
        private static Panel greetingPagePanel = new Panel();
        private static Client mainClient = new Client();
        private static Server mainServer = new Server();
        private static bool playerReady = false;
        private static bool otherPlayerReady = false;

        private static MenuStrip mainMenuStrip;
        private static Panel firstPanel;
        private static Button firstButton;
        private static Button secondButton;
        private static Panel mainEnemyPanel;
        //!!!!!!!!!!1
        public static RichTextBox message = new RichTextBox();
        //!!!!!!!!!!!!!!11

        public static void AddButtonsToPanel(Panel p_panelMain,
            int p_intRows, 
            int p_intColumns, 
            int p_intButtonHeiht,
            int p_intButtonWidth,
            bool p_booAddEnemyPanelEvent)
        {
            for (int i = 0; i < p_intColumns; i++)
            {
                for (int j = 0; j < p_intRows; j++)
                {
                    Panel panelSmallPanel = new Panel();
                    panelSmallPanel.BackColor = Color.Transparent;
                    panelSmallPanel.Height = p_intButtonHeiht;
                    panelSmallPanel.Width = p_intButtonWidth;

                    panelSmallPanel.Left = p_intButtonWidth * j + 25;
                    panelSmallPanel.Top = p_intButtonHeiht * i + 25;
                    if (p_booAddEnemyPanelEvent)
                    {
                        panelSmallPanel.Click += new EventHandler(ButtonEnemyMap_Click);
                    }
                    else
                    {
                        panelSmallPanel.MouseEnter += new EventHandler(playerMapMouseEnter);
                        panelSmallPanel.Click += new EventHandler(playerMapClick);
                        panelSmallPanel.MouseLeave += new EventHandler(panelSmallPanel_MouseLeave);
                    }

                    p_panelMain.Controls.Add(panelSmallPanel);
                }
            }
        }

        static void playerMapClick(object sender, EventArgs e)
        {
            
            if (allowAddShip)
            {
                Panel panelShipPanel = new Panel();
                panelShipPanel.BackColor = Color.Transparent;
                panelShipPanel.Top = ((Panel)sender).Top - 25;
                panelShipPanel.Height = 50;

                if ((settleBattleShips.getShipNo() < 4) && (settleBattleShips.getShipNo() > -1))
                {
                    panelShipPanel.Left = ((Panel)sender).Left - 5;
                    panelShipPanel.Width = 40;
                    panelShipPanel.BackgroundImage = Image.FromFile("1stShipFinal2.png");
                    placeShipOnMap((Panel)sender);
                }

                if ((settleBattleShips.getShipNo() >= 4) && (settleBattleShips.getShipNo() < 7))
                {
                    panelShipPanel.Left = ((Panel)sender).Left - 5;
                    panelShipPanel.Width = 70;

                    placeShipOnMap((Panel)sender);

                    if (!settleBattleShips.getReverse())
                    {
                        placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]);
                        panelShipPanel.BackgroundImage = Image.FromFile("2ndShipFinal3.png");
                    }
                    else
                    {
                        panelShipPanel.Width = 50;
                        panelShipPanel.Height = 70;
                        panelShipPanel.BackgroundImage = Image.FromFile("2ndShipFinal3_rotated.png");
                        placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]);
                    }
                }

                if ((settleBattleShips.getShipNo() >= 7) && (settleBattleShips.getShipNo() < 9))
                {
                    panelShipPanel.Left = ((Panel)sender).Left - 20;
                    panelShipPanel.Width = 105;

                    placeShipOnMap((Panel)sender);

                    if (!settleBattleShips.getReverse())
                    {
                        placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]);
                        placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]);

                        panelShipPanel.BackgroundImage = Image.FromFile("3rdShipFinal2.png");
                    }
                    else
                    {
                        panelShipPanel.Left = ((Panel)sender).Left;
                        panelShipPanel.Width = 50;
                        panelShipPanel.Height = 105;
                        panelShipPanel.BackgroundImage = Image.FromFile("3rdShipFinal2_rotated.png");
                        placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]);
                        placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]);
                    }

                  }
                    if (settleBattleShips.getShipNo() == 9)
                    {
                        panelShipPanel.Left = ((Panel)sender).Left - 15;
                        panelShipPanel.Width = 130;

                        placeShipOnMap((Panel)sender);

                        if (!settleBattleShips.getReverse())
                        {
                            placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]);
                            placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]);
                            placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 3]);
                            panelShipPanel.BackgroundImage = Image.FromFile("4thShipFinal2.png");
                        }
                        else
                        {
                            panelShipPanel.Left = ((Panel)sender).Left;
                            panelShipPanel.Width = 50;
                            panelShipPanel.Height = 130;
                            panelShipPanel.BackgroundImage = Image.FromFile("4thShipFinal2_rotated.png");
                            placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]);
                            placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]);
                            placeShipOnMap((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 30]);
                        }
                    }

                playerMap.Controls.Add(panelShipPanel);
                settleBattleShips.placeShip();
                allowAddShip = false;   
            }
        }

        static void panelSmallPanel_MouseLeave(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.Transparent;

            if (!settleBattleShips.getReverse())
            {

                for (int i = playerMap.Controls.IndexOf((Panel)sender); i <= (playerMap.Controls.IndexOf((Panel)sender) / 10) * 10 + 9; i++)
                {
                    if (((Panel)playerMap.Controls[i]).BackColor != Color.Blue)
                        ((Panel)playerMap.Controls[i]).BackColor = Color.Transparent;
                }
            }
            else
            {
                for (int i = playerMap.Controls.IndexOf((Panel)sender); 
                    i <= 90 + (playerMap.Controls.IndexOf((Panel)sender) % 10) ; 
                    i = i + 10)
                {
                    if (((Panel)playerMap.Controls[i]).BackColor != Color.Blue)
                        ((Panel)playerMap.Controls[i]).BackColor = Color.Transparent;
                }
            }

        }

        static void playerMapMouseEnter(object sender, EventArgs e)
        {
            if ((settleBattleShips.getShipNo() < 4) && (settleBattleShips.getShipNo() != -1))
            {
              ((Panel)sender).BackColor = Color.Green;
              allowAddShip = true;
            }

            if ((settleBattleShips.getShipNo() >= 4) && (settleBattleShips.getShipNo() < 7))
            {
                if (!settleBattleShips.getReverse())
                {
                    if (playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 9)
                    {
                        if (checkIfShipFits(playerMap.Controls.IndexOf((Panel)sender)))
                        {
                            ((Panel)sender).BackColor = Color.Green;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.LawnGreen;
                            allowAddShip = true;
                        }
                        else
                        {
                            if (((Panel)sender).BackColor != Color.Blue)
                                ((Panel)sender).BackColor = Color.Red;
                            allowAddShip = false;
                        }
                    }
                    else
                    {
                        if (((Panel)sender).BackColor != Color.Blue)
                            ((Panel)sender).BackColor = Color.Red;
                        allowAddShip = false;
                    }
                }
                else
                {
                    if (playerMap.Controls.IndexOf(((Panel)sender)) / 10 != 9)
                    {
                        if (checkIfShipFits(playerMap.Controls.IndexOf((Panel)sender)))
                        {
                            ((Panel)sender).BackColor = Color.Green;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.LawnGreen;
                            allowAddShip = true;
                        }
                        else
                        {
                            if (((Panel)sender).BackColor != Color.Blue)
                              ((Panel)sender).BackColor = Color.Red;
                            allowAddShip = false;
                        }
                    }
                    else
                    {
                        ((Panel)sender).BackColor = Color.Red;
                        allowAddShip = false;
                    }
                }

            }

            if ((settleBattleShips.getShipNo() == 7) ||
                (settleBattleShips.getShipNo() == 8))
            {
                if (!settleBattleShips.getReverse())
                {
                    if ((playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 8) &&
                        (playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 9))
                    {
                        if (checkIfShipFits(playerMap.Controls.IndexOf((Panel)sender)))
                        {
                            ((Panel)sender).BackColor = Color.Green;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.LawnGreen;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]).BackColor = Color.LawnGreen;

                            allowAddShip = true;
                        }
                        else
                        {
                           ((Panel)sender).BackColor = Color.Red;
                            if (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor != Color.Blue)
                                ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.Red;
                            allowAddShip = false;
                        }
                    }
                    else
                    {
                        if ((playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 9) && (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor != Color.Blue))
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.Red;
                        ((Panel)sender).BackColor = Color.Red;
                        allowAddShip = false;
                    }
                }
                else
                {
                    if ((playerMap.Controls.IndexOf(((Panel)sender)) / 10 != 8) &&
                        (playerMap.Controls.IndexOf(((Panel)sender)) / 10 != 9))
                    {
                        if (checkIfShipFits(playerMap.Controls.IndexOf((Panel)sender)))
                        {
                            ((Panel)sender).BackColor = Color.Green;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.LawnGreen;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]).BackColor = Color.LawnGreen;

                            allowAddShip = true;
                        }
                        else
                        {
                            ((Panel)sender).BackColor = Color.Red;
                            if (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor != Color.Blue)
                                ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.Red;
              
                            allowAddShip = false;
                        }
                    }
                    else
                    {
                        if ((playerMap.Controls.IndexOf((Panel)sender) / 10 != 9) && (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor != Color.Blue))
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.Red;
                        ((Panel)sender).BackColor = Color.Red;
                        allowAddShip = false;
                    }
                }
            }
            if (settleBattleShips.getShipNo() == 9)
            {
                if (!settleBattleShips.getReverse())
                {
                    if ((playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 7) &&
                        (playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 8) &&
                        (playerMap.Controls.IndexOf(((Panel)sender)) % 10 != 9))
                    {
                        if (checkIfShipFits(playerMap.Controls.IndexOf((Panel)sender)))
                        {
                            ((Panel)sender).BackColor = Color.Green;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.LawnGreen;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]).BackColor = Color.LawnGreen;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 3]).BackColor = Color.LawnGreen;
                            allowAddShip = true;
                        }
                        else
                        {
                            ((Panel)sender).BackColor = Color.Red;
                            if (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor != Color.Blue)
                                ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.Red;
                            if (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]).BackColor != Color.Blue)
                                ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]).BackColor = Color.Red;
         
                            allowAddShip = false;
                        }
                    }
                    else
                    {
                        if ((playerMap.Controls.IndexOf(((Panel)sender)) % 10 <= 8) && 
                             (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor != Color.Blue))
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 1]).BackColor = Color.Red;
                        if ((playerMap.Controls.IndexOf(((Panel)sender)) % 10 <= 7) &&
                            (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]).BackColor != Color.Blue))
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 2]).BackColor = Color.Red;

                        ((Panel)sender).BackColor = Color.Red;
                        allowAddShip = false;
                    }
                }
                else
                {
                    if ((playerMap.Controls.IndexOf(((Panel)sender)) / 10 != 7) &&
                        (playerMap.Controls.IndexOf(((Panel)sender)) / 10 != 8) &&
                        (playerMap.Controls.IndexOf(((Panel)sender)) / 10 != 9))
                    {
                        if (checkIfShipFits(playerMap.Controls.IndexOf((Panel)sender)))
                        {
                            ((Panel)sender).BackColor = Color.Green;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.LawnGreen;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]).BackColor = Color.LawnGreen;
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 30]).BackColor = Color.LawnGreen;
                            allowAddShip = true;
                        }
                        else
                        {
                            ((Panel)sender).BackColor = Color.Red;
                            if (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor != Color.Blue)
                                ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.Red;
                            if (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]).BackColor != Color.Blue)
                                ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]).BackColor = Color.Red;
                            allowAddShip = false;
                        }
                    }
                    else
                    {
                        if ((playerMap.Controls.IndexOf(((Panel)sender)) / 10 <= 8) && (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor != Color.Blue))
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 10]).BackColor = Color.Red;
                        if ((playerMap.Controls.IndexOf(((Panel)sender)) / 10 <= 7) && (((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]).BackColor != Color.Blue))
                            ((Panel)playerMap.Controls[playerMap.Controls.IndexOf((Panel)sender) + 20]).BackColor = Color.Red;

                        ((Panel)sender).BackColor = Color.Red;
                        allowAddShip = false;
                    }
                }
            }
        }
         
        private static void ButtonEnemyMap_Click(object sender, EventArgs e)
        {
            PictureBox pictBoxShot = new PictureBox();

            pictBoxShot.Image = Image.FromFile("X_FINALl.png");
            pictBoxShot.BackColor = Color.Transparent;

            ((Panel)sender).BackgroundImage = pictBoxShot.Image;
            mainClient.sendMessage(100);
        }

        public static void addBattleShips(Panel p_panelMain)
        {
            for (int i = 0; i < 10; i++)
            {
                if ((i == 0) || (i == 1) || (i == 2) || (i == 3))
                    addShipToPanel(p_panelMain, 40, i * 50, 0, i);
                if ((i == 4) || (i == 5) || (i == 6))
                    addShipToPanel(p_panelMain, 70, (i - 4) * 70, 50, i);
                if ((i == 7) || (i == 8))
                    addShipToPanel(p_panelMain, 120, (i - 7) * 120, 100, i);
                if (i == 9)
                    addShipToPanel(p_panelMain, 150, 0, 150, i);

            }
        }

        private static void Ship_Click(object sender, EventArgs e)
        {
            cancelShip();

            settleBattleShips.pickShip(mainPanel.Controls.IndexOf(((Panel)sender)));
            
            ((Panel)sender).Visible = false;
        }

        private static void addShipToPanel(Panel p_panelMain,
            int p_intWidth,
            int p_intLeft,
            int p_intTop,
            int p_intShipNo)
        {
            Panel panelShip = new Panel();

            if ((p_intShipNo == 0) || (p_intShipNo == 1) || (p_intShipNo == 2) || (p_intShipNo == 3))
                panelShip.BackgroundImage = Image.FromFile("1stShipFinal2.png");
           
            if ((p_intShipNo == 4) || (p_intShipNo == 5) || (p_intShipNo == 6))
                panelShip.BackgroundImage = Image.FromFile("2ndShipFinal3.png");

            if ((p_intShipNo == 7) || (p_intShipNo == 8))
                panelShip.BackgroundImage = Image.FromFile("3rdShipFinal2.png");

            if (p_intShipNo == 9)
                panelShip.BackgroundImage = Image.FromFile("4thShipFinal2.png");

            panelShip.BackColor = Color.Transparent;
            panelShip.Height = 50;
            panelShip.Width = p_intWidth;

            panelShip.Left = p_intLeft;
            panelShip.Top = p_intTop;
            panelShip.Click += new EventHandler(Ship_Click);
            p_panelMain.Controls.Add(panelShip);
        }

        public static void setMainPanel(Panel p_panelMain)
        {
            mainPanel = p_panelMain;
        }

        public static void setPlayerMapPanel(Panel p_panelPlayerMapPanel)
        {
            playerMap = p_panelPlayerMapPanel;
        }

        public static void reverseShip()
        {
            settleBattleShips.reverseShip();
        }

        public static void cancelShip()
        {
            Panel panelCurrentShip = new Panel();

            if (settleBattleShips.getShipNo() != -1)
            {
                panelCurrentShip = ((Panel)mainPanel.Controls[settleBattleShips.getShipNo()]);
                settleBattleShips.cancelCurrentBattleShip(panelCurrentShip);
            }

            if (settleBattleShips.getReverse())
                settleBattleShips.reverseShip();

        }

        public static void cancelShips()
        {
            cancelShip();
            while (playerMap.Controls.Count != 100)
                playerMap.Controls.RemoveAt(playerMap.Controls.Count - 1);

            for (int i = 0; i < 100; i++)
            {
                playerMap.Controls[i].Visible = true;
                playerMap.Controls[i].BackColor = Color.Transparent;
            }
            for (int i = 0; i < 10; i++)
                mainPanel.Controls[i].Visible = true;
            playerReady = false;

            mainClient.sendMessage(101);
            mainServer.sendMessage(101);
        }

        public static void placeShipOnMap(Panel p_panelMain)
        {
            p_panelMain.BackColor = Color.Blue;
            setInvisibleSelfAndAroundPanel(p_panelMain);
        }

        private static void setInvisibleSelfAndAroundPanel(Panel p_panelMain)
        {
            p_panelMain.Visible = false;
            
            if (playerMap.Controls.IndexOf(p_panelMain) % 10 != 9)
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) + 1].Visible = false;
            if (playerMap.Controls.IndexOf(p_panelMain) / 10 != 9)
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) + 10].Visible = false;
            if (playerMap.Controls.IndexOf(p_panelMain) % 10 != 0)
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) - 1].Visible = false;
            if (playerMap.Controls.IndexOf(p_panelMain) / 10 != 0)
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) - 10].Visible = false;

            if ((playerMap.Controls.IndexOf(p_panelMain) / 10 != 9) &&
                (playerMap.Controls.IndexOf(p_panelMain) % 10 != 9))
            {
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) + 11].Visible = false;
            }

            if ((playerMap.Controls.IndexOf(p_panelMain) / 10 != 0) &&
                (playerMap.Controls.IndexOf(p_panelMain) % 10 != 0))
            {
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) - 11].Visible = false;
            }

            if ((playerMap.Controls.IndexOf(p_panelMain) % 10 != 9) &&
                (playerMap.Controls.IndexOf(p_panelMain) / 10 != 0))
            {
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) - 9].Visible = false;
            }
            if ((playerMap.Controls.IndexOf(p_panelMain) / 10 != 9) &&
                (playerMap.Controls.IndexOf(p_panelMain) % 10 != 0))
            {
                playerMap.Controls[playerMap.Controls.IndexOf(p_panelMain) + 9].Visible = false;
            }
        }

        private static bool checkIfShipFits(int p_intPanelNo)
        {
            bool boolResult;

            if (!settleBattleShips.getReverse())
            {
                boolResult = playerMap.Controls[p_intPanelNo + 1].Visible;
            }
            else
            {
               boolResult = playerMap.Controls[p_intPanelNo + 10].Visible;
            }

            if ((settleBattleShips.getShipNo() >= 7) && (settleBattleShips.getShipNo() <= 9))
            {
                if (!settleBattleShips.getReverse())
                {
                    boolResult = playerMap.Controls[p_intPanelNo + 2].Visible && boolResult;
                }
                else
                {
                    boolResult = playerMap.Controls[p_intPanelNo + 20].Visible && boolResult;
                }
            }

            if (settleBattleShips.getShipNo() == 9)
            {
                if (!settleBattleShips.getReverse())
                {
                    boolResult = playerMap.Controls[p_intPanelNo + 3].Visible && boolResult;
                }
                else
                {
                    boolResult = playerMap.Controls[p_intPanelNo + 30].Visible && boolResult;
                }
            }

            return boolResult;
        }

        public static bool letGameStart()
        {
            return playerIsReady() && otherPlayerReady;
        }

        public static void removeGreetingPage()
        {
            greetingPagePanel.Visible = false;            
        }

        public static void setGreetingPagePanel(Panel p_panelMain)
        {
            Server mainServer = new Server();

            greetingPagePanel = p_panelMain;  

        }
        
        public static void setMainClient(Client p_mainClient)
        {
            mainClient = p_mainClient;
        }

        public static void setMainServer(Server p_mainServer)
        {
            mainServer = p_mainServer;
        }

        public static void setOtherPlayerReady()
        {
            otherPlayerReady = true;
        }

        public static void setOtherPlayerNotRead()
        {
            otherPlayerReady = false;
        }

        public static bool playerIsReady()
        {
            bool booResult = true;

            for (int i = 0; i < 10; i++)
            {
                if (((Panel)mainPanel.Controls[i]).Visible)
                    booResult = false;
            }
            playerReady = booResult;
            return booResult;
        }

        public static void RedrawToBattleField(Panel p_mainPanel, Panel p_removePanel)
        {
            //mainMenuStrip.Visible = false;
          //  firstPanel.Visible = false;
          //  firstButton.Visible = false;
          //  secondButton.Visible = false;
            p_removePanel.Visible = false;
            p_mainPanel.Visible = true;
            PanelUtils.AddButtonsToPanel(p_mainPanel, 10, 10, 25, 23, true);
        }

        public static void prepareObjectsForBattleField(MenuStrip p_mainMenuStrip,
            Panel p_firstPanel,
            Button p_firstButton,
            Button p_secondButton,
            Panel p_mainPanel)
        {
            mainMenuStrip = p_mainMenuStrip;
            firstPanel = p_firstPanel;
            firstButton = p_firstButton;
            secondButton = p_secondButton ;
            mainEnemyPanel = p_mainPanel;
        }
    }

}
