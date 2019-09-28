using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoomItApp.GameEngine
{
    public class MatrixLevels<T> where T : Unit ,new()
    {

        public static  ObservableCollection<T> OrderedUnitsEngine { get; set; } = new ObservableCollection<T>();

        public static List<int> GamePattern = new List<int>() {
                                                        1, 0, 0, 1, 0, 0, 1,
                                                        0, 1, 0, 1, 0, 1, 0,
                                                        0, 0, 1, 1, 1, 0, 0,
                                                        1, 1, 1, 0, 1, 1, 1,
                                                        0, 0, 1, 1, 1, 0, 0,
                                                        0, 1, 0, 1, 0, 1, 0,
                                                        1, 0, 0, 1, 0, 0, 1 };
        public T[,,] Matrix { get; set; }

        public bool KillUnit(int side,int index,ObservableCollection<T> matrixUnits)
        {

            if (matrixUnits[index].Side == side && !CheckForTriplet(side,index, matrixUnits))
            {
                return true;
            }
            return false;
        }
        public bool CheckForTriplet(int side, int index, ObservableCollection<T> matrixUnits)
        {
            var position = matrixUnits[index].Position;
            if (position.x == 1 || position.y == 1)
            {
                if (position.y == 1 && position.MatrixLevel == 0)
                {
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y + 1 && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y - 1 &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel + 2).Side == side)
                        {
                            return true;
                        }
                    }
                }
                if (position.x == 1 && position.MatrixLevel == 0)
                {
                    if (matrixUnits.First(unit => unit.Position.x == position.x + 1 && unit.Position.y == position.y  && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 1 && unit.Position.y == position.y  &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel + 2).Side == side)
                        {
                            return true;
                        }
                    }
                }

                if (position.y == 1 && position.MatrixLevel == 1)
                {
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y + 1 && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y - 1 &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel - 1).Side == side)
                        {
                            return true;
                        }
                    }
                }
                if (position.x == 1 && position.MatrixLevel == 1)
                {
                    if (matrixUnits.First(unit => unit.Position.x == position.x + 1 && unit.Position.y == position.y  && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 1 && unit.Position.y == position.y  &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel - 1).Side == side)
                        {
                            return true;
                        }
                    }
                }

                if (position.y == 1 && position.MatrixLevel == 2)
                {
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y + 1 && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y - 1 &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel - 1).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel - 2).Side == side)
                        {
                            return true;
                        }
                    }
                }
                if (position.x == 1 && position.MatrixLevel == 2)
                {
                    if (matrixUnits.First(unit => unit.Position.x == position.x + 1 && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 1 && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }
                    if (matrixUnits.First(unit => unit.Position.x == position.x && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel - 1).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel - 2).Side == side)
                        {
                            return true;
                        }
                    }
                }


            }
            else
            {
                if (position.x == 0)
                {
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x + 1 && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x + 2 && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }

                if (position.x == 2)
                {
                  
                    if (matrixUnits.First(unit => unit.Position.x == position.x - 1 && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit => unit.Position.x == position.x - 2 && unit.Position.y == position.y && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }
                // 0 0 X Vertical
                if (position.y == 0)
                {
                    if (matrixUnits.First(unit => unit.Position.y == position.y + 1 && unit.Position.x == position.x && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit => unit.Position.y == position.y + 2 && unit.Position.x == position.x && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }

                if (position.y == 2)
                {
                    if (matrixUnits.First(unit => unit.Position.y == position.y - 1 && unit.Position.x == position.x && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit => unit.Position.y == position.y - 2 && unit.Position.x == position.x && unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }



            }

            return false;
        }

        public T[,,] GenerateMatrix(T[,,] matrix)
        {
            var gameBoard = new T[3,3,3];
            var countProcess = 0;


            var x = 0;
            var z = 0;
            var y = 0;

            foreach (var state in GamePattern) {

                if (state == 1)
                {
                    var orderItem = new T { Position = new Position { x = x, MatrixLevel = z, y = y }, Side = 0 , Index = countProcess};
                    OrderedUnitsEngine.Add(orderItem);
                    gameBoard[x, z, y] = orderItem;
                    var nextPattern = GiveNextPattern(x, z, y, countProcess);
                    x = nextPattern[0];
                    y = nextPattern[2];
                    z = nextPattern[1];
                    countProcess++;
                  
                }
                
            }
            
            return gameBoard;
        }

        public static List<int> GiveNextPattern(int x, int z, int y,int countProcess)
        {
   
            if (countProcess >= 0 && countProcess < 2)
            {
                x++;
            }
            else if (countProcess >= 3 && countProcess < 5)
            {
                x++;
            }
            else if (countProcess >= 6 && countProcess < 8)
            {
                x++;
            }
            else if (countProcess >= 9 && countProcess < 11)
            {
                z++;
            }
            else if (countProcess >= 12 && countProcess < 14)
            {
                z--;
            }
            else if (countProcess >= 15 && countProcess < 17)
            {
                x++;
            }
            else if (countProcess >= 18 && countProcess < 20)
            {
                x++;
            }
            else if (countProcess >= 21 && countProcess < 23)
            {
                x++;
            }
            if (countProcess == 2)
            {
                z = 1;
                x = 0;
            }
            else if (countProcess == 5)
            {
                z = 2;
                x = 0;
            }
            else if (countProcess == 8)
            {
                x = 0;
                z = 0;
                y = 1;
            }
            else if (countProcess == 11)
            {
                x = 2;
            }
            else if (countProcess == 14)
            {
                y = 2;
                z = 2;
                x = 0;
            }
            else if (countProcess == 17)
            {
                z = 1;
                x = 0;
            }
            else if (countProcess == 20)
            {
                z = 0;
                x = 0;
            }

          
            return  new List<int>(){x,z,y};
        }
    }
}
