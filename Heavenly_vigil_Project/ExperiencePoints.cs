using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// Handles the experience to the player and checks if the player levelup.
    /// </summary>
    public static class ExperiencePoints
    {
        //-----FIELDS-----
        private static int playerExp;
        private static int maxEXP = 100;
        private static int expPercentage;
        private static int playerLevel = 1;



        //-----PROPERTIES-----

        public static int PlayerExp
        {
            get { return playerExp; }
            set { playerExp = value; }
        }

        public static int MaxEXP
        {
            get { return maxEXP; }
            set { maxEXP = value; }
        }

        public static int PlayerLevel
        {
            get { return playerLevel; }
            set { playerLevel = value; }
        }

        public static int ExpPercentage
        {
            get { return expPercentage; }
            set { expPercentage = value; }
        }

        //-----METHODS-----

        public static void CheckForLevelUp(GameTime gameTime)
        {
            expPercentage = playerExp * 100 / maxEXP;

            if (playerExp >= maxEXP)
            {
                playerLevel++;
                maxEXP += maxEXP / 10;
                playerExp = 0;

                UpgradeInterface.ChooseUpgrade(gameTime);
            }
        }


    }
}
