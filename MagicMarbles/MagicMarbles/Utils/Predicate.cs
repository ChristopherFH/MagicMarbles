using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MagicMarbles.Utils
{
    public static class Predicate
    {
        public static Predicate<Button> ByVisibility(Visibility visivility)
        {
            return btn => btn.Visibility == visivility;
        }
    }
}
