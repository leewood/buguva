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
    class SettleBattleShips
    {
        private int pickedShipNo = -1;
        private bool pickedShipReverse = false;
        private int[] shipNoInNewPanel = new int[10];
        private int shipPlacedAt = -1;

        public SettleBattleShips()
        {
            for (int i = 0; i < 10; i++)
            {
                shipNoInNewPanel[i] = -1;
            }

        }
        public void pickShip(int p_intShipNo)
        {
            pickedShipNo = p_intShipNo;
        }

        public void reverseShip()
        {
            pickedShipReverse = !pickedShipReverse;
        }

        public int cancelPickedShip()
        {
            int intResult = -1;

            intResult = pickedShipNo;
            pickedShipNo = -1;
            pickedShipReverse = false;

            return intResult;
        }

        public int getShipNo()
        {
            return pickedShipNo;
        }

        public bool getReverse()
        {
            return pickedShipReverse;
        }

        public void cancelCurrentBattleShip(Panel p_panelMain)
        {
            pickedShipNo = -1;
            p_panelMain.Visible = true;
        }

        public void placeShip()
        {
            shipPlacedAt++;
            shipNoInNewPanel[pickedShipNo] = shipPlacedAt;
            pickedShipNo = -1;
            pickedShipReverse = false;
        }
    }
}
