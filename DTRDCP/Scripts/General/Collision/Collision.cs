using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using Microsoft.Xna.Framework;

namespace General.Collision
{

    class CollisionChecker
    {
        public static bool CheckCollision(Rectangle one, Rectangle two)
        {
            return (one.Left <= two.Right &&
                    two.Left <= one.Right &&
                    one.Top <= two.Bottom &&
                    two.Top <= one.Bottom);
        }

        public static bool CheckCollision(Rectangle rect, Vector2 point)
        {
            return ((point.X >= rect.Left && point.X <= rect.Right) &&
                    (point.Y >= rect.Top && point.Y <= rect.Bottom));
        }

        public static bool CheckCollision(Rectangle rect, Vector2 circlePosition, float radius)
        {
            return ((((float)rect.Width / 2f) + radius) >= Math.Abs(rect.Center.X - circlePosition.X) &&
                    (float)rect.Height / 2f + radius >= Math.Abs(rect.Center.Y - circlePosition.Y));
        }

        public static bool CheckCollision(Vector2 onePos, float oneRad, Vector2 twoPos, float twoRad)
        {
            float distance = Math.Abs((onePos - twoPos).Length());
            return (distance <= oneRad + twoRad);
        }

        public static bool CheckCollision(Vector2 circlePos, float circleRadius, Vector2 point)
        {
            float distance = Math.Abs((circlePos - point).Length());
            return distance <= circleRadius;
        }

    }

    class RectCollider : CollisionChecker
    { 
        public Rectangle Bounds { get; private set; }

        public RectCollider(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public bool CheckCollision(Vector2 point)
        {
            return PointCollision(point);
        }

        public bool CheckCollision(RectCollider collider)
        {
            return RectCollision(collider.Bounds);
        }
        public bool CheckCollision(Rectangle bounds)
        {
            return RectCollision(bounds);
        }

        private bool PointCollision(Vector2 point)
        {
            return ((point.X >= Bounds.Left && point.X <= Bounds.Right) &&
                    (point.Y >= Bounds.Top && point.Y <= Bounds.Bottom));
        }
        private bool RectCollision(Rectangle other)
        {
            return (((other.Right >= Bounds.Left && other.Right <= Bounds.Right) ||
                    (Bounds.Right >= other.Left && Bounds.Right <= other.Right)) &&
                    ((other.Bottom >= Bounds.Top && other.Bottom <= Bounds.Bottom) ||
                    (Bounds.Bottom >= other.Top && Bounds.Bottom <= other.Bottom)));
        }

    }

    class CircleCollider
    {
        public Vector2 Position { get; private set; }
        public float Radius { get; private set; }

        public CircleCollider(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }


        public bool RectCollision(Rectangle rect)
        {
            return ((float)rect.Width / 2f + Radius < Math.Abs(rect.Center.X - Position.X) &&
                    (float)rect.Height / 2f + Radius < Math.Abs(rect.Center.Y - Position.Y));
        }


    }

}
