using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MagicMarbles.Model;
using MagicMarbles.Utils;

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

        private string _winLose;
        public string WinLose
        {
            get => _winLose;
            set
            {
                _winLose = value;
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
            RegisterReceiver();
            WinLose = "";
        }

        private void RegisterReceiver()
        {
            Messenger.Default.Register<int>
            (
                this,
                SetHighscore
            );
            Messenger.Default.Register<string>
            (
                this,
                SetWinLose
            );
        }

        private void SetWinLose(string winlose)
        {
            WinLose = winlose;
        }

        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(
                    (args) =>
                    {
                        ViewModelLocator.Cleanup();
                    });
            }
        }

        private void SetHighscore(int highscore)
        {
            if (WinLose == string.Empty)
            {
                Highscore = highscore;
            }
        }

        public void StartGame()
        {
            WinLose = string.Empty;
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

        //        public void ChangeToHighscore()
        //        {
        //            //TODO: must be implemented later
        //        }

        #endregion
    }
}