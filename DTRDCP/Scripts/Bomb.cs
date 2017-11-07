using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General;
using General.CostumGameComponents;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DestructibleTerain.Scripts
{
    class Bomb : Object2D
    {
        public Bomb(Texture2D bomb, Vector2 position) : base(bomb)
        {
            SetDrawOrder(200);
            Position = position;
        }

        public void Explode(Object2D explodable, Texture2D explosion)
        {
            var exp = new Explosion(explosion, explodable)
            {
                Scale = new Vector2(0.25f, 0.25f)
            };
            exp.Position = Position;
            exp.CenterOrigin();

            ComponentManegment.Instance.Dispose(this);
        }



    }
}
