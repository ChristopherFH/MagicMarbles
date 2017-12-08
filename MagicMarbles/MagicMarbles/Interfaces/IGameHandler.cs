using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;

namespace MagicMarbles.Interfaces
{
    public interface IGameHandler
    {
        int Highscore { get; set; }
        ObservableCollection<Button> BoardSetup(int row, int column, RelayCommand<object> selectButtonCommand);
        ObservableCollection<Button> MakeMove(ObservableCollection<Button> buttons, object commandparam);
        int CalculateHighscore();
        string CheckWinLose();

    }
}
