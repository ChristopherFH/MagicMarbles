using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MagicMarbles.Model
{
    public class CustomButton : Button
    {
        private const String DictionaryUri = "Views/Dictionary.xaml";

        public CustomButton(ICommand command, object i)
        {
            Command = command;
            CommandParameter = i;
            SetStyle();
        }

        public void SetStyle()
        {
            var mystyles = new ResourceDictionary {Source = new Uri(DictionaryUri, UriKind.RelativeOrAbsolute)};
            var mybuttonstyle = mystyles["CircleButton"] as Style;
            Style = mybuttonstyle;
        }
    }
}
