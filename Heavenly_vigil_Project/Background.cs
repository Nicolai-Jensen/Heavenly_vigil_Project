using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// Loads and Draws the Background screen.
    /// </summary>
    internal class Background : GameObject
    {
        //Fields
        private Song backGroundMusic;
        //Properties

        //Constructors

        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>("NyBaggrund3");
            backGroundMusic = content.Load<Song>("BackGroundSong");
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(backGroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
        }
        public override void Update(GameTime gameTime)
        {
            if (Player.Health < 0)
                MediaPlayer.Stop();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[0], Vector2.Zero, Color.White);
        }
    }
}
