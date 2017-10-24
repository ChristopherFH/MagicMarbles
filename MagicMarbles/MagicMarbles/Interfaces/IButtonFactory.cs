using GalaSoft.MvvmLight.Command;
using MagicMarbles.Model;
using MagicMarbles.Views;

namespace MagicMarbles.Interfaces
{
    interface IButtonFactory
    {
        CustomButton CreateRandomButton(int random, RelayCommand<object> command, int i);
    }
}
