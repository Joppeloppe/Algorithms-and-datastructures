using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ProjektSkjukspel
{
    static class Input
    {
        public static KeyboardState keyboardState, oldKeyboardState = Keyboard.GetState();
        public static KeyboardState keyState, oldKeyState = Keyboard.GetState();

        public static MouseState mouseState, oldMouseState = Mouse.GetState();

        public static Point mousePosition, oldMousePosition;

        public static bool KeyClick(Keys key)
        {
            return keyboardState.IsKeyUp(key) && oldKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public static bool LeftClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftPress()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool RightClick()
        {
            return mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
        }

        public static bool RightPress()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool MiddleClick()
        {
            return mouseState.MiddleButton == ButtonState.Pressed && oldMouseState.MiddleButton == ButtonState.Released;
        }

        public static bool MiddlePress()
        {
            return mouseState.MiddleButton == ButtonState.Pressed;
        }

        public static void Update()
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            mousePosition = new Point(mouseState.X, mouseState.Y);
            oldMousePosition = new Point(oldMouseState.X, oldMouseState.Y);

            oldKeyState = keyState;
            keyState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
        }
    }
}
