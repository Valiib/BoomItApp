using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BoomItApp.Model;

namespace BoomItApp.ViewModel
{
    public class BoomGameViewModel : GameEngine.MatrixLevels<Model.Unit> , INotifyPropertyChanged
    {
        public List<int> BaseMatrix;
   

        public ObservableCollection<Model.Unit> OrderedUnits { get; set; }

        public new Model.Unit[,,] Matrix { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public BoomGameViewModel()
        {
            Matrix = GenerateMatrix(new Model.Unit[3, 3, 3]);
            BaseMatrix = GamePattern;
            OrderedUnits = OrderedUnitsEngine;
            
        }

    

        public void AddActiveUnit(Unit selectedBox)
        {
            OrderedUnits[selectedBox.Index].Side = 1;
        }

        
    }
}
