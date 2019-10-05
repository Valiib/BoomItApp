using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BoomItApp.Model;

namespace BoomItApp.ViewModel
{
    public class BoomGameViewModel : GameEngine.MatrixLevels<Model.Unit> 
    {
        public List<int> BaseMatrix;

        public BoomGameViewModel()
        {
            GenerateMatrix(new Model.Unit[3, 3, 3]);
            BaseMatrix = GamePattern;
        }
    }
}
