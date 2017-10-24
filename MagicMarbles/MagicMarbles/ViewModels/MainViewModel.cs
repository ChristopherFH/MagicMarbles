using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MagicMarbles.Model;

namespace MagicMarbles.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private const int ROWS = 7;
        private const int COLUMNS = 10;

        public ViewModelBase CurrentViewModel { get; set; }
        public CustomGrid CustomGrid { get; set; } 

        #region Commands

        public RelayCommand ChangeToInfoPageCommand { get; set; }
        public RelayCommand ChangeToGamePageCommand { get; set; }
        public RelayCommand ChangeToHighscorePageCommand { get; set; }
        public RelayCommand StartGameCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            ChangeToInfoPageCommand = new RelayCommand(ChangeToInfo);
            ChangeToGamePageCommand = new RelayCommand(ChangeToGame);
            StartGameCommand = new RelayCommand(StartGame);
            CurrentViewModel = new GameViewModel();
            CustomGrid = new CustomGrid(ROWS,COLUMNS);
        }


        public void StartGame()
        {
            Messenger.Default.Send<CustomGrid>(CustomGrid);
        }

        #region ChangePageMethods

        public void ChangeToInfo()
        {
            CurrentViewModel = new InfoViewModel();
            RaisePropertyChanged("CurrentViewModel");
        }

        public void ChangeToGame()
        {
            CurrentViewModel = new GameViewModel();
            RaisePropertyChanged("CurrentViewModel");
        }

        public void ChangeToHighscore()
        {
            //TODO: must be implemented later
        }

        #endregion
    }
}