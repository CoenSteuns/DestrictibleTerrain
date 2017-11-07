using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using General.CostumGameComponents;
using General.Collision;
using General.Input;
using General;

namespace General.UI
{
    class Button : Object2D
    {
        public Action OnClick;

        private Rectangle _collider;

        public Button(Texture2D button) : base(button)
        {

        }
        public override void Update(GameTime gametime)
        {
            var pos = Position.ToPoint();
            _collider = new Rectangle(pos.X, pos.Y, (int)Width, (int)Height);


            if (OnClick == null)
                return;

            if (InputHelper.MouseButtonUp(MouseButtons.left) && CollisionChecker.CheckCollision(_collider, InputHelper.MouseState.Position.ToVector2()) )
            {
                OnClick();
            }
        }

    }
}
