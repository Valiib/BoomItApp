using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BoomItApp.GameEngine;
using BoomItApp.Model;
using BoomItApp.ViewModel;
using Xamarin.Forms;

namespace BoomItApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public bool IsKillState { get; set; }
        public int PlayCounter { get; set; }
        public bool IsFirstPlayer { get; set; }
        public BoomGameViewModel ViewModel { get; set; }
        public MainPage()
        {
            
            ViewModel = new BoomGameViewModel();
            InitializeComponent();
            BindingContext = ViewModel;
            DrawBoard();
            
        }
        


        private void DrawBoard()
        {
            var grid = new Grid();
            grid.HorizontalOptions = LayoutOptions.CenterAndExpand;
            grid.RowSpacing = 20;
            grid.ColumnSpacing = 20;

            var rowSize = 7;
            for (int i = 0; i < rowSize; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            }

            int x=0, y=0, z=0;
            var matrixIndex = 0;
            var currentIndex = 0;
            var rowIndex = 0;
            foreach (var spaceOrStar in ViewModel.BaseMatrix)
            {
                if (currentIndex == rowSize)
                {
                    currentIndex = 0;
                    grid.RowDefinitions.Add(new RowDefinition(){Height = GridLength.Auto});
                    rowIndex++;
                }
               
                if (spaceOrStar == 1)
                {


                    var gesture = new TapGestureRecognizer();
                    gesture.Tapped += UserTurn;

                    var emptySqare = new BoxView() { BindingContext = ViewModel.OrderedUnits[matrixIndex], CornerRadius = 2, HeightRequest = 30 , WidthRequest = 30};
                    emptySqare.SetBinding(BackgroundColorProperty, "ColorSelected");
                    emptySqare.GestureRecognizers.Add(gesture);

                    matrixIndex++;

                    var nextValuesOnList = MatrixLevels<Model.Unit>.GiveNextPattern(x, z, y, matrixIndex);
                    x = nextValuesOnList[0];
                    z = nextValuesOnList[1];
                    y = nextValuesOnList[2];

                    grid.Children.Add(emptySqare, currentIndex, rowIndex);
                }
           

                currentIndex++;
            }
            this.Board.Children.Add(grid);
        }

      
        void UserTurn(object sender, EventArgs e)
        {
            var index = GetIndexByPosition(sender);
            if (!IsKillState)
            {
                if (PlayCounter < 18)
                {
                    if (ViewModel.OrderedUnits[index].Side == 0)
                    {
                 

                        if (IsFirstPlayer)
                        {
                            ViewModel.OrderedUnits[index].Side = 1;
                            IsFirstPlayer = false;
                        }
                        else
                        {
                            ViewModel.OrderedUnits[index].Side = 2;
                            IsFirstPlayer = true;
                        }

                        if (ViewModel.CheckForTriplet(ViewModel.OrderedUnits[index].Side, index,
                            ViewModel.OrderedUnits))
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
                        if (ViewModel.OrderedUnits[index].Side == 1)
                        {
                            //FireMoveOptions(1, index, ViewModel.OrderedUnits);
                        }
                    }
                    else
                    {
                        if (ViewModel.OrderedUnits[index].Side == 2)
                        {
                            //FireMoveOptions(1, index, ViewModel.OrderedUnits);
                        }
                    }
                }
               
            }
            else
            { 

                if (IsFirstPlayer)
                {
                    if (ViewModel.OrderedUnits[index].Side == 1)
                    {
                        if (ViewModel.KillUnit(1, index, ViewModel.OrderedUnits))
                        {
                            ViewModel.OrderedUnits[index].Side = 0;
                            IsKillState = false;
                            //CheckDeath();
                        }
                    }
                }
                else
                {
                    if (ViewModel.OrderedUnits[index].Side == 2)
                    {
                        if (ViewModel.KillUnit(2, index, ViewModel.OrderedUnits))
                        {
                            ViewModel.OrderedUnits[index].Side = 0;
                          
                           
                            IsKillState = false;
                        }
                    }
                }

               
            }
        }

        private int GetIndexByPosition(object sender)
        {
            var sqareData =(Model.Unit)(((BoxView) sender).BindingContext);
            var index = ViewModel.OrderedUnits.First(x => x.Position.x == sqareData.Position.x && x.Position.y == sqareData.Position.y &&
                                                          x.Position.MatrixLevel == sqareData.Position.MatrixLevel).Index;
         
            return index;
        }
    }
}
