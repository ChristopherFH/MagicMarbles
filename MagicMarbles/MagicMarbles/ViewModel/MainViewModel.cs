using System;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MagicMarbles.Views;

namespace MagicMarbles.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        public ViewModelBase CurrentViewModel { get; set; }

        #region Commands

        public RelayCommand ChangeToInfoPageCommand { get; set; }
        public RelayCommand ChangeToGamePageCommand { get; set; }
        public RelayCommand ChangeToHighscorePageCommand { get; set; }

        #endregion
    

        public MainViewModel()
        {
            ChangeToInfoPageCommand = new RelayCommand(ChangeToInfo);
            ChangeToGamePageCommand = new RelayCommand(ChangeToGame);
            CurrentViewModel = new GameViewModel();
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