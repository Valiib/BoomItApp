using System;
using System.Collections.Generic;
using System.Drawing;


namespace BoomItApp.GameEngine
{
    public abstract class Unit
    {
        public Unit()
        {
            ColorSelected = Color.Blue;
        }
        public int Index { get; set; }
        public abstract int Side { get; set; }
        public Position Position { get; set; }
        public Color ColorSelected { get; set; }
    }
}
