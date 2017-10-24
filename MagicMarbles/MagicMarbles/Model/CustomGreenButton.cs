using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

namespace MagicMarbles.Model
{
    class CustomGreenButton : CustomButton
    {
        public CustomGreenButton(RelayCommand<object> command, int i) : base(command, i)
        {
           Background = new SolidColorBrush(Colors.Green);
           
        }
    }
}
