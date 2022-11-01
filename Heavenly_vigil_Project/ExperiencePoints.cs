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
        private static int experiencePoints;
        private static int maxEXP;
        private static int playerLevel;

        //-----PROPERTIES-----

        public static int Experiencepoints
        {
            get { return experiencePoints; }
            set { experiencePoints = value; }
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

        //-----METHODS-----

        public static void CheckForLevelUp()
        {
            if (experiencePoints >= maxEXP)
            {
                playerLevel++;
                ChooseUpgrade();
            }
        }

        public static void ChooseUpgrade()
        {

        }
    }
}
