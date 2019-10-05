using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;

using Xamarin.Forms;


namespace BoomItApp.GameEngine
{
    public class MatrixLevels<T> where T : Unit, new()
    {
        public delegate void EndGameDelegate(object sender, EndGameEventArgs e);
        public event EndGameDelegate EndGameEvent;

        public Unit MovingUnit { get; set; }
        public bool IsMoveState { get; set; }
        public bool IsKillState { get; set; }
        public int PlayCounter { get; set; }
        public bool IsFirstPlayer { get; set; }
        public ObservableCollection<T> OrderedUnits { get; set; } = new ObservableCollection<T>();

        public static List<int> GamePattern = new List<int>()
        {
            1, 0, 0, 1, 0, 0, 1,
            0, 1, 0, 1, 0, 1, 0,
            0, 0, 1, 1, 1, 0, 0,
            1, 1, 1, 0, 1, 1, 1,
            0, 0, 1, 1, 1, 0, 0,
            0, 1, 0, 1, 0, 1, 0,
            1, 0, 0, 1, 0, 0, 1
        };
        public bool CheckForTriplet(int side, int index, ObservableCollection<T> matrixUnits)
        {
            var position = matrixUnits[index].Position;
            if (position.x == 1 || position.y == 1)
            {
                if (position.y == 1 && position.MatrixLevel == 0)
                {
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y + 1 &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y - 1 &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
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
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x + 1 && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 1 && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
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
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y + 1 &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y - 1 &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
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
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x + 1 && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 1 && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel + 1).Side == side)
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
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y + 1 &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x && unit.Position.y == position.y - 1 &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel - 1).Side == side)
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
                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x + 1 && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 1 && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                            return true;
                    }

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel - 1).Side == side)
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

                    if (matrixUnits.First(unit =>
                            unit.Position.x == position.x - 1 && unit.Position.y == position.y &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.x == position.x - 2 && unit.Position.y == position.y &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }
                // 0 0 X Vertical
                if (position.y == 0)
                {
                    if (matrixUnits.First(unit =>
                            unit.Position.y == position.y + 1 && unit.Position.x == position.x &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.y == position.y + 2 && unit.Position.x == position.x &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }

                if (position.y == 2)
                {
                    if (matrixUnits.First(unit =>
                            unit.Position.y == position.y - 1 && unit.Position.x == position.x &&
                            unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                    {
                        if (matrixUnits.First(unit =>
                                unit.Position.y == position.y - 2 && unit.Position.x == position.x &&
                                unit.Position.MatrixLevel == position.MatrixLevel).Side == side)
                        {
                            return true;
                        }
                    }
                }



            }

            return false;
        }
        public static List<int> GiveNextPattern(int x, int z, int y, int countProcess)
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


            return new List<int>() { x, z, y };
        }
        public void GenerateMatrix(T[,,] matrix)
        {
            var countProcess = 0;
            var x = 0;
            var z = 0;
            var y = 0;
            foreach (var state in GamePattern)
            {
                if (state == 1)
                {
                    var orderItem = new T
                        { Position = new Position { x = x, MatrixLevel = z, y = y }, Side = 0, Index = countProcess };
                    OrderedUnits.Add(orderItem);
                    var nextPattern = GiveNextPattern(x, z, y, countProcess);
                    x = nextPattern[0];
                    y = nextPattern[2];
                    z = nextPattern[1];
                    countProcess++;
                }
            }
        }
        public int GetIndexByPosition(object sender, ObservableCollection<T> matrixUnits)
        {
            var sqareData = (Model.Unit)(((BoxView)sender).BindingContext);
            var index = matrixUnits.First(x => x.Position.x == sqareData.Position.x && x.Position.y == sqareData.Position.y &&
                                               x.Position.MatrixLevel == sqareData.Position.MatrixLevel).Index;

            return index;
        }
        public void UserTurn(object sender, EventArgs e)
        {
            var index = GetIndexByPosition(sender, this.OrderedUnits);
            if (!IsMoveState)
            {
                if (!IsKillState)
                {
                    if (PlayCounter < 18)
                    {
                        if (this.OrderedUnits[index].Side == 0)
                        {
                            if (IsFirstPlayer)
                            {
                                this.OrderedUnits[index].Side = 1;
                                IsFirstPlayer = false;
                            }
                            else
                            {
                                this.OrderedUnits[index].Side = 2;
                                IsFirstPlayer = true;
                            }

                            if (this.CheckForTriplet(this.OrderedUnits[index].Side, index,
                                this.OrderedUnits))
                            {
                                IsKillState = true;
                            }

                            PlayCounter++;
                        }
                    }
                    else
                    {

                        if (IsFirstPlayer)
                        {
                            if (this.OrderedUnits[index].Side == 1)
                            {
                                var check = this.FireMoveOptions(1, index, this.OrderedUnits);
                                MovingUnit = this.OrderedUnits[index];
                                IsMoveState = true;
                            }
                        }
                        else
                        {
                            if (this.OrderedUnits[index].Side == 2)
                            {
                                this.FireMoveOptions(2, index, this.OrderedUnits);
                                MovingUnit = this.OrderedUnits[index];
                                IsMoveState = true;
                            }
                        }
                    }

                }
                else
                {
                    if (IsFirstPlayer)
                    {
                        if (this.OrderedUnits[index].Side == 1)
                        {
                            if (this.KillUnit(1, index, this.OrderedUnits))
                            {
                                this.OrderedUnits[index].Side = 0;
                                IsKillState = false;
                                if (PlayCounter >= 18)
                                {
                                    IsMoveState = true;
                                    if (CheckDeath() > 0)
                                        OnEndGameEvent(new EndGameEventArgs(){SideWin = CheckDeath()});
                                }


                            }
                        }
                    }
                    else
                    {
                        if (this.OrderedUnits[index].Side == 2)
                        {
                            if (this.KillUnit(2, index, this.OrderedUnits))
                            {
                                this.OrderedUnits[index].Side = 0;
                                IsKillState = false;
                                if (PlayCounter >= 18)
                                {
                                    IsMoveState = true;
                                    if (CheckDeath() > 0)
                                        OnEndGameEvent(new EndGameEventArgs() { SideWin = CheckDeath() });
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.OrderedUnits[index].Side == 3)
                {
                    var oldSide = this.OrderedUnits[MovingUnit.Index].Side;
                    this.OrderedUnits[MovingUnit.Index].Side = 0;
                    this.OrderedUnits[index].Side = oldSide;
                    IsMoveState = false;


                    if (IsFirstPlayer)
                    {
                        IsFirstPlayer = false;
                    }
                    else
                    {
                        IsFirstPlayer = true;
                    }



                    if (this.CheckForTriplet(this.OrderedUnits[index].Side, index,
                        this.OrderedUnits))
                    {
                        IsKillState = true;
                    }
                    this.OrderedUnits = this.UnFireMove(this.OrderedUnits);
                }
                else
                {
                    this.OrderedUnits = this.UnFireMove(this.OrderedUnits);
                    IsMoveState = false;
                }
            }
            if (PlayCounter >= 18 && this.OrderedUnits.Count(x => x.Side == 3) == 0)
            {
                if (!this.EndMoveGame(2, this.OrderedUnits))
                {
                    OnEndGameEvent(new EndGameEventArgs() { SideWin = 2 });
                }
                if (!this.EndMoveGame(1, this.OrderedUnits))
                {
                    OnEndGameEvent(new EndGameEventArgs() { SideWin = 1 });
                }
            }
        }
        public bool KillUnit(int side, int index, ObservableCollection<T> matrixUnits)
        {
            var killTriplets = true;
            foreach (var unit in matrixUnits.Where(x => x.Side == side).ToList())
            {
                if (!CheckForTriplet(side, unit.Index, matrixUnits))
                {
                    killTriplets = false;
                }
            }

            if (killTriplets)
            {
                if (matrixUnits[index].Side == side)
                {
                    return true;
                }
            }
            else
            {
                if (matrixUnits[index].Side == side && !CheckForTriplet(side, index, matrixUnits))
                {
                    return true;
                }
            }


            return false;

        }
        private int CheckDeath()
        {
            if (this.OrderedUnits.Count(x => x.Side == 1) <= 2)
            {
                return 1;
            }

            if (this.OrderedUnits.Count(x => x.Side == 2) <= 2)
            {
                return 2;
            }

            return 0;
        }
        public bool EndMoveGame(int side,ObservableCollection<T> matrixUnits)
        {

            foreach (var unit in matrixUnits.Where(x => x.Side == side))
            {
                if (FireMoveOptions(side, unit.Index,matrixUnits).Count > 0)
                {
                    UnFireMove(matrixUnits);
                    return true;
                }
            }
            return false;
        }
        public ObservableCollection<T> UnFireMove(ObservableCollection<T> matrixUnits)
        {
            var resultItems = new ObservableCollection<T>();
            foreach (var firedUnit in matrixUnits)
            {
                if (firedUnit.Side == 3)
                {
                    firedUnit.Side = 0;
                }
                resultItems.Add(firedUnit);
            }

            return resultItems;
        }
        public ObservableCollection<T> FireMoveOptions(int p0, int index, ObservableCollection<T> matrixUnits)
        {
            var changedOccurences = new ObservableCollection<T>();
            var currentPosition = matrixUnits[index].Position;
            if (matrixUnits.Count(x => x.Side == p0) == 3)
            {
                foreach (var unit in matrixUnits)
                {
                    if (unit.Side == 0)
                    {
                        unit.Side = 3;
                        changedOccurences.Add(unit);
                    }
                }

                return changedOccurences;
            }

            var checkUnit = matrixUnits.FirstOrDefault(unit =>
                unit.Position.x == currentPosition.x + 1 && unit.Position.y == currentPosition.y &&
                unit.Position.MatrixLevel == currentPosition.MatrixLevel);
            if (checkUnit != null && checkUnit.Side == 0)
            {
                checkUnit.Side = 3;
                changedOccurences.Add(checkUnit);
            }

            checkUnit = matrixUnits.FirstOrDefault(unit =>
                unit.Position.x == currentPosition.x - 1 && unit.Position.y == currentPosition.y &&
                unit.Position.MatrixLevel == currentPosition.MatrixLevel);
            if (checkUnit != null && checkUnit.Side == 0)
            {
                checkUnit.Side = 3;
                changedOccurences.Add(checkUnit);
            }

            checkUnit = matrixUnits.FirstOrDefault(unit =>
                unit.Position.x == currentPosition.x && unit.Position.y == currentPosition.y + 1 &&
                unit.Position.MatrixLevel == currentPosition.MatrixLevel);
            if (checkUnit != null && checkUnit.Side == 0)
            {
                checkUnit.Side = 3;
                changedOccurences.Add(checkUnit);
            }

            checkUnit =
                 matrixUnits.FirstOrDefault(unit =>
                    unit.Position.x == currentPosition.x && unit.Position.y == currentPosition.y - 1 &&
                    unit.Position.MatrixLevel == currentPosition.MatrixLevel);
            if (checkUnit != null && checkUnit.Side == 0)
            {
                checkUnit.Side = 3;
                changedOccurences.Add(checkUnit);
            }

            if (currentPosition.x == 1 || currentPosition.y == 1)
            {
                checkUnit =
                     matrixUnits.FirstOrDefault(unit =>
                        unit.Position.x == currentPosition.x && unit.Position.y == currentPosition.y  &&
                        unit.Position.MatrixLevel == currentPosition.MatrixLevel + 1 );
                if (checkUnit != null && checkUnit.Side == 0)
                {
                    checkUnit.Side = 3;
                    changedOccurences.Add(checkUnit);
                }
                checkUnit =
                      matrixUnits.FirstOrDefault(unit =>
                        unit.Position.x == currentPosition.x && unit.Position.y == currentPosition.y &&
                        unit.Position.MatrixLevel == currentPosition.MatrixLevel - 1);
                if (checkUnit != null && checkUnit.Side == 0)
                {
                    checkUnit.Side = 3;
                    changedOccurences.Add(checkUnit);
                }
            }
            return changedOccurences;
        }

        protected virtual void OnEndGameEvent(EndGameEventArgs e)
        {
            EndGameEvent?.Invoke(this, e);
        }
    }

    public class EndGameEventArgs
    {
        public int SideWin { get; set; }
    }
}
