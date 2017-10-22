using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MagicMarbles.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        public RelayCommand NewGameCommand { get; set; }

        public MainViewModel()
        {
            NewGameCommand = new RelayCommand(ChangeView);
        }

        void ChangeView()
        {
            Console.WriteLine("Change View now.");
        }
    }
}