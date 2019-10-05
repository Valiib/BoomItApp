using System;
using System.ComponentModel;
using System.Linq;
using BoomItApp.GameEngine;
using BoomItApp.ViewModel;
using Xamarin.Forms;
using Unit = BoomItApp.Model.Unit;

namespace BoomItApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public BoomGameViewModel ViewModel { get; set; }
        public MainPage()
        {
            ViewModel = new BoomGameViewModel();
            InitializeComponent();
            BindingContext = ViewModel;
            ViewModel.EndGameEvent += ViewModel_EndGameEvent;   
            DrawBoard();
        }

        private async void ViewModel_EndGameEvent(object sender, EndGameEventArgs e)
        {
            if (e.SideWin == 1)
            {
                await DisplayAlert("Endgame","Player 1 Win","Ok");
            }
            else if (e.SideWin == 2)
            {
                await DisplayAlert("Endgame", "Player 2 Win", "Ok");
            }
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
                    gesture.Tapped += ViewModel.UserTurn;
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
    }
}
