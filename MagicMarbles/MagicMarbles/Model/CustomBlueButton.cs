using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using MagicMarbles.Model;

namespace MagicMarbles.Views
{
    class CustomBlueButton : CustomButton
    {
        public CustomBlueButton(RelayCommand<object> command, int i) : base(command, i)
        {
            Background = new SolidColorBrush(Colors.Blue);
        }
    }
}
