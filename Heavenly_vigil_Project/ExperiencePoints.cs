﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
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

                ChooseUpgrade(gameTime);
            }
        }

        public static void ChooseUpgrade(GameTime gameTime)
        {
            UpgradeInterface lvlup = new UpgradeInterface();
            GameWorld.InstantiateGameObject(lvlup);
        }
    }
}
