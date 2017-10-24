using System.Windows.Media;
using GalaSoft.MvvmLight.Command;


namespace MagicMarbles.Model
{
    class CustomRedButton : CustomButton
    {
        public CustomRedButton(RelayCommand<object> command, int i) : base(command, i)
        {
            Background = new SolidColorBrush(Colors.Red);
        }
    }
}
