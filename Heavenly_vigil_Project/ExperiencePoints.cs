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

        /// <summary>
        /// Checks to see if the player has enough experience points to level up.
        /// If so, the player gains a level and the eperience resets.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void CheckForLevelUp(GameTime gameTime)
        {
            //Calculates the amount of experience the player has in percentage of the value required for the next level.
            expPercentage = playerExp * 100 / maxEXP;

            //If the player has the same amount or more experience than required to level up, the player gains a level, and the experience resets.
            //The amount of experience required for the next level is also increased by 10% of the current value.
            if (playerExp >= maxEXP)
            {
                playerLevel++;
                maxEXP += maxEXP / 10;
                playerExp = 0;

                //Calls the ChooseUpgrade method in the UpgradeInterface class.
                UpgradeInterface.ChooseUpgrade(gameTime);
            }
        }


    }
}
