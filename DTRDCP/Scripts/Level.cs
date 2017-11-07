using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General;
using General.CostumGameComponents;

using Microsoft.Xna.Framework.Graphics;

namespace DestructibleTerain.Scripts
{
    class Level : Object2D
    {
        public Level(Texture2D level) : base(level)
        {
            SetDrawOrder(0);
        }

    }
}
