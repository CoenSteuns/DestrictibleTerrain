using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General;
using General.CostumGameComponents;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DestructibleTerain.Scripts
{
    class Explosion : Object2D
    {
        Object2D _explodable;

        float starttime = 0;

        public Explosion(Texture2D explosion, Object2D explodable) : base(explosion)
        {
            _explodable = explodable;
        }

        public override void Update(GameTime gametime)
        {
            if (starttime == 0)
                starttime = (float)gametime.TotalGameTime.TotalMilliseconds;

            if ((float)gametime.TotalGameTime.TotalMilliseconds - starttime >= 300)
                DestroyObject();
            base.Update(gametime);
        }

        private void DestroyObject()
        {
            ComponentManegment.Instance.Dispose(this);
            var rad = (Sprite.Width / 2) * Scale.X;
            var pos = Position.ToPoint();
            DestroyTerrain(pos.X, pos.Y, (int)rad, _explodable);

        }

        public void DestroyTerrain(int xPos, int yPos, int Radius, Object2D explodable,int color = 0, bool ignoreAlpha = true)
        {
            var explodableSprite = explodable.Sprite;

            uint[] pixelLevelData = new uint[explodableSprite.Width * explodableSprite.Height];

            explodableSprite.GetData(pixelLevelData, 0, explodableSprite.Width * explodableSprite.Height);
            Vector2 position = new Vector2(xPos, yPos);

            for (int x = xPos - Radius; x < xPos + Radius; x++)
            {
                for (int y = yPos - Radius; y < yPos + Radius; y++)
                {

                    //pixel buiten beeld
                    if (x >= explodableSprite.Width || y >= explodableSprite.Height ||
                        x < 0 || y < 0)
                        continue;

                    //level pixel of weapon pixel is alpha
                    if (!(pixelLevelData[x + y * explodableSprite.Width] != 0))
                        continue;

                    if (!(Math.Pow(x - xPos, 2) + Math.Pow(y - yPos, 2) < Radius * Radius))
                        continue;

                    pixelLevelData[x + y * explodableSprite.Width] = (uint)color;

                }

            }
            // Update the texture with the changes made above
            explodableSprite.SetData(pixelLevelData);
            explodable.SetSprite(explodableSprite);
        }

    }
}
