using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    public static class TimeManager
    {
        public static float currentTime;
        public static int timerSeconds;
        public static int timerMinutes;

        public static void UpdateClock(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime > 1)
            {
                timerSeconds++;
                currentTime = 0;

                if (timerSeconds > 59)
                {
                    timerMinutes++;

                    timerSeconds = 0;
                }
            }

        }
    }
}
