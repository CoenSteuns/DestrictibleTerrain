using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using General.CostumGameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace General
{
    public class Object2D : DrawableCostumGameComponent
    {
        #region Fields
        public Vector2 Position = new Vector2(0, 0);
        public float Rotation = 0;
        public Vector2 Scale = new Vector2(1, 1);

        public float Width
        { get
            {
                if(Sprite != null)
                    return Sprite.Width * Scale.X;

                return 0;
            }
        }
        public float Height
        {
            get
            {
                if (Sprite != null)
                    return Sprite.Height * Scale.Y;
                return 0;
            }
        }

        public Texture2D Sprite { get; protected set; }

        public Vector2 Origin { get; protected set; }
        #endregion

        public Object2D(Texture2D sprite) : base()
        {
            this.Sprite = sprite;
            Origin = new Vector2(0,0);
        }

        public override void Draw(GameTime gametime, SpriteBatch spritebatch)
        {
            if (Sprite == null)
                return;

            Vector2 size = new Vector2(Width, Height);


            spritebatch.Begin();
            spritebatch.Draw(Sprite, new Rectangle(Position.ToPoint(), size.ToPoint()), new Rectangle(0, 0, Sprite.Width, Sprite.Height), Color.White, Rotation, Origin, SpriteEffects.None, 0);
            spritebatch.End();
        }

        public void SetOrigin(Vector2 origin)
        {
            Origin = origin;
        }

        public void SetSprite(Texture2D newSprite, bool ScaleOrigin = true)
        {

            if (ScaleOrigin)
                Origin = new Vector2(newSprite.Width / (Sprite.Width / Origin.X), newSprite.Height / (Sprite.Height / Origin.Y));

            Sprite = newSprite;
        }
         public void CenterOrigin()
        {
            Origin = new Vector2(Sprite.Width/2, Sprite.Height/2);
        }

    }
}
