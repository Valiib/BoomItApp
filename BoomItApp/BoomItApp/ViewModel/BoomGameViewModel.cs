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

        public Unit MovingUnit { get; set; }
        public bool IsMoveState { get; set; }
        public bool IsKillState { get; set; }
        public int PlayCounter { get; set; }
        public bool IsFirstPlayer { get; set; }

        public ObservableCollection<Model.Unit> OrderedUnits { get; set; }

        public new Model.Unit[,,] Matrix { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public BoomGameViewModel()
        {
            Matrix = GenerateMatrix(new Model.Unit[3, 3, 3]);
            BaseMatrix = GamePattern;
            OrderedUnits = OrderedUnitsEngine;
            
        }

        public void UserTurn(object sender, EventArgs e)
        {
            var index = GetIndexByPosition(sender,this.OrderedUnits);
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
                                    CheckDeath();
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
                                    CheckDeath();
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
                    //DisplayAlert("Player 2 won!", "", "ok");
                }
                if (!this.EndMoveGame(1, this.OrderedUnits))
                {
                    //DisplayAlert("Player 1 won!", "", "ok");
                }
            }
        }
        private void CheckDeath()
        {
            if (this.OrderedUnits.Count(x => x.Side == 1) <= 2)
            {
                //DisplayAlert("Player 2 won!", "", "ok");
            }

            if (this.OrderedUnits.Count(x => x.Side == 2) <= 2)
            {
                //DisplayAlert("Player 1", "", "Won");
            }
        }

        public void AddActiveUnit(Unit selectedBox)
        {
            OrderedUnits[selectedBox.Index].Side = 1;
        }


     
    }
}
