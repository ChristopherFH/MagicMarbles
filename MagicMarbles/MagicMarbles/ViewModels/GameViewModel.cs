using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MagicMarbles.Extensions;
using MagicMarbles.Helpers;
using MagicMarbles.Model;

namespace MagicMarbles.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public MarbleGameHandler GameHandler; 
        public RelayCommand<object> SelectButtonCommand { get; set; }

        private ObservableCollection<Button> _btn;
        public ObservableCollection<Button> Buttons
        {
            get => _btn;
            set
            {
                _btn = value;
                RaisePropertyChanged();
            }
        }

        private int _row;
        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                RaisePropertyChanged();
            }
        }

        private int _column;
        public int Column
        {
            get => _column;
            set
            {
                _column = value;
                RaisePropertyChanged();
            }
        }

        public GameViewModel()
        {
            SelectButtonCommand = new RelayCommand<object>(SelectButton);
            GameHandler = new MarbleGameHandler();
            Messenger.Default.Register<CustomGrid>
            (
                this,
                ReceiveGridSize
            );
        }

        private void ReceiveGridSize(CustomGrid gridsize)
        {
            if (gridsize.Rows > 0 && gridsize.Columns > 0) //&& (Row != gridsize.Rows || Column != gridsize.Columns))
            {
                Row = gridsize.Rows;
                Column = gridsize.Columns;
                Buttons = GameHandler.BoardSetup(Row, Column, SelectButtonCommand);
            }
        }

        private void SelectButton(object commandparam)
        {
            Buttons = GameHandler.MakeMove(Buttons, commandparam);
            Messenger.Default.Send<string>(GameHandler.CheckWinLose());
            Messenger.Default.Send<int>(GameHandler.Highscore);
        }
}
}