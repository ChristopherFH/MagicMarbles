using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MagicMarbles.Model;

namespace MagicMarbles.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private const int Rows = 7;
        private const int Columns = 10;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged();
            }
        }

        private int _highscore;

        public int Highscore
        {
            get => _highscore;
            set
            {
                _highscore = value;
                RaisePropertyChanged(); 
            }
            
        }


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
            CustomGrid = new CustomGrid(Rows, Columns);
            Messenger.Default.Register<int>
            (
                this,
                SetHighscore
            );
        }

        private void SetHighscore(int highscore)
        {
            Highscore = highscore;
        }


        public void StartGame()
        {
            Highscore = 0; 
            CurrentViewModel = new GameViewModel();
            Messenger.Default.Send<CustomGrid>(CustomGrid);
        }

        #region ChangePageMethods

        public void ChangeToInfo()
        {
            CurrentViewModel = new InfoViewModel();
        }

        public void ChangeToGame()
        {
            CurrentViewModel = new GameViewModel();
        }

        public void ChangeToHighscore()
        {
            //TODO: must be implemented later
        }

        #endregion
    }
}