using GalaSoft.MvvmLight.Command;
using MagicMarbles.Interfaces;
using MagicMarbles.Model;
using MagicMarbles.Views;

namespace MagicMarbles.Helpers
{
    class ButtonFactory : IButtonFactory
    {
       
        public CustomButton CreateRandomButton(int random, RelayCommand<object> command, int i)
        {
            switch (random)
            {
                case 0:
                    return new CustomRedButton(command, i);
                case 1:
                    return new CustomBlueButton(command, i);
                case 2:
                    return new CustomGreenButton(command, i);
            }
            return null;
        }
    }
}
