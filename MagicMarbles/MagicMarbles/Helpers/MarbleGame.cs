using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MagicMarbles.Model;
using MagicMarbles.Utils;

namespace MagicMarbles.Helpers
{
    public static class MarbleGame
    {

        private static readonly ButtonFactory Factory = new ButtonFactory();
        private static readonly int NumberOfEnums = Enum.GetNames(typeof(Enums.Colors)).Length;
        private static CustomGrid Grid { get; set; }

        public static ObservableCollection<Button> BoardSetup(int row, int column, RelayCommand<object> selectButtonCommand)
        {
            Grid = new CustomGrid(row, column);
            ObservableCollection<Button> buttons = new ObservableCollection<Button>();
            for (int i = 0; i < row * column; i++)
            {
                CustomButton btn = Factory.CreateRandomButton(RandomNumberGenerator.Dice(0, NumberOfEnums), selectButtonCommand, i);
                buttons.Add(btn);
            }
            return buttons;
        }

        public static void HideButton(ObservableCollection<Button> buttons, Object commandparam)
        {
            var command = Convert.ToString(commandparam);
            Console.WriteLine(@"Button: {0}", command);

            //             Change color of button 
            //            Buttons[Convert.ToInt32(command)].Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            buttons[Convert.ToInt32(command)].Visibility = Visibility.Hidden;
        }
    }
}
