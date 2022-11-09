using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// The Mushroom, A common enemy, but a little stronger than skeletons.
    /// </summary>
    internal class Mushroom : Enemy
    {
        //Fields
        //Properties
        //Constructors
        public Mushroom()
        {
            speed = 200;
            position = SpawnPosition();
            scale = 2f;
            health = 20 + ExperiencePoints.PlayerLevel * 4;
            damage = 10;
            color = Color.White;
            expPoints = 30;
        }
        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[8];
            for (int i = 0; i < objectSprites.Length; i++)
            {
                objectSprites[i] = content.Load<Texture2D>($"MushroomWalk_{i}");
            }
            defeatedSound = content.Load<SoundEffect>("enemy_defeat");
        }
        public override void Update(GameTime gameTime)
        {
            ChooseDirection();
            Animate(gameTime);
            Move(gameTime);
            Death();
            KatanaDamaged(gameTime);
            DamagedFeedBack(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            if (ReturnPlayerPosition().X < position.X)
                spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.FlipHorizontally, 1f);
            else
                spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.None, 1f);
        }
    }
}
