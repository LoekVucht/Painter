using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    public class InputHelper
    {
        private MouseState _currentMouseState, _previousMouseState;
        private KeyboardState _currentKeyboardState, _previousKeyboardState;

        public void Update()
        {
            _previousMouseState = _currentMouseState;
            _previousKeyboardState = _currentKeyboardState;
            _currentMouseState = Mouse.GetState();
            _currentKeyboardState = Keyboard.GetState();
        }

        public bool KeyPressed(Keys k)
        {
            return _currentKeyboardState.IsKeyDown(k) && _previousKeyboardState.IsKeyUp(k);
        }

        public bool MouseLeftButtonPressed()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed
                && _previousMouseState.LeftButton == ButtonState.Released;
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(_currentMouseState.X, _currentMouseState.Y); }
        }
    }
}
