using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework.Input;

using General.CostumGameComponents;
using Microsoft.Xna.Framework;

namespace General.Input
{

    public enum MouseButtons
    {
        left = 0,
        right = 1,
        middle = 2
    }

    public static class InputHelper
    {

        public static MouseState MouseState { get { return Mouse.GetState(); } }
        public static KeyboardState KeyboardState { get { return Keyboard.GetState(); } }

        private static KeyboardState _previousState;
        private static MouseState _previousMouseState;

        public static bool MouseButtonDown(MouseButtons button)
        {
            if (button == MouseButtons.left)
                return ButtonDown(Mouse.GetState().LeftButton == ButtonState.Pressed, _previousMouseState.LeftButton == ButtonState.Pressed);

            if (button == MouseButtons.right)
                return ButtonDown(Mouse.GetState().RightButton == ButtonState.Pressed, _previousMouseState.RightButton == ButtonState.Pressed);

            if (button == MouseButtons.middle)
                return ButtonDown(Mouse.GetState().MiddleButton == ButtonState.Pressed, _previousMouseState.MiddleButton == ButtonState.Pressed);

            return false;
        }
        public static bool MouseButtonUp(MouseButtons button)
        {
            if (button == MouseButtons.left)
                return ButtonUp(Mouse.GetState().LeftButton == ButtonState.Pressed, _previousMouseState.LeftButton == ButtonState.Pressed);

            if (button == MouseButtons.right)
                return ButtonUp(Mouse.GetState().RightButton == ButtonState.Pressed, _previousMouseState.RightButton == ButtonState.Pressed);

            if (button == MouseButtons.middle)
                return ButtonUp(Mouse.GetState().MiddleButton == ButtonState.Pressed, _previousMouseState.MiddleButton == ButtonState.Pressed);

            return false;
        }

        public static bool MouseButton(MouseButtons button)
        {
            if (button == MouseButtons.left)
                return Mouse.GetState().LeftButton == ButtonState.Pressed;

            if (button == MouseButtons.right)
                return Mouse.GetState().RightButton == ButtonState.Pressed;

            if (button == MouseButtons.middle)
                return Mouse.GetState().MiddleButton == ButtonState.Pressed;

            return false;
        }

        public static bool GetKeyDown(Keys key)
        {
            return ButtonDown(Keyboard.GetState().IsKeyDown(key), _previousState.IsKeyDown(key));
        }
        public static bool GetKeyUp(Keys key)
        {
            return ButtonUp(_previousState.IsKeyDown(key), Keyboard.GetState().IsKeyDown(key));
        }

        public static bool GetKey(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static void Update()
        {
            _previousState = Keyboard.GetState();
            _previousMouseState = Mouse.GetState();

        }


        private static bool ButtonDown(bool current, bool pref)
        {
            if (pref)
                return false;

            if (current)
                return true;

            return false;
        }

        private static bool ButtonUp(bool current, bool pref)
        {
            if (!pref)
                return false;

            if (!current)
                return true;

            return false;
        }

    }
}
