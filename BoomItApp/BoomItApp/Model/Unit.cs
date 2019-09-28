using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace BoomItApp.Model
{
    public class Unit : GameEngine.Unit, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static List<Color> Colors = new List<Color>() { Color.Gray, Color.DarkSalmon, Color.Aquamarine };
      
        private int _side { get; set; }
        public override int Side { get {return _side; } set {
                if (value == _side)
                {
                    return;
                }
                _side = value;
                ColorSelected = Colors[_side];

                OnPropertyChanged("Side");
            }
        }

        private Color _colorSelected { get; set; } = Colors[0];
        public new Color ColorSelected {
            get { return  _colorSelected; }
            set {
                if (value == _colorSelected)
                {
                    return;
                }
                _colorSelected = value  ;
                OnPropertyChanged("ColorSelected");

            }
        }
        private bool _canMove;
        public bool CanMove
        {
            get => _canMove;
            set
            {
                if (_canMove == value)
                { return;}
                _canMove = value;
                OnPropertyChanged("CanMove");
            }
        }

        protected void OnPropertyChanged( string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
