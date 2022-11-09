using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{/// <summary>
/// Controls and updates the clock.
/// </summary>
    public static class TimeManager
    {
        public static float currentTime;
        public static int timerSeconds;
        public static int timerMinutes;

        /// <summary>
        /// Updates the clock by adding value to to timerSeconds every second and timerMinutes every 60 seconds.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void UpdateClock(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Player.Health > 0)
            {
                if (currentTime > 1)
                {
                    timerSeconds++;
                    currentTime = 0;
                }
                if (timerSeconds > 59)
                {
                    timerMinutes++;

                    timerSeconds = 0;
                }
            }

        }
    }
}