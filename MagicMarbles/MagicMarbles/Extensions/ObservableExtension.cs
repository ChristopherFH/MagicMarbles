using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MagicMarbles.Extensions
{
    public static class ObservableExtension
    {
        public static Button[,] To2DArray(this IEnumerable<Button> input, params object[] parameters)
        {
            int rowsCount = (int)parameters[0];
            int colsCount = (int)parameters[1];

            Button[,] ret = new Button[rowsCount, colsCount];
            int i = 0, j = 0;
            foreach (Button currentEntry in input)
            {
                ret[i, j] = currentEntry;
                j++;
                if (j == colsCount)
                {
                    i++;
                    j = 0;
                }
            }
            return ret;
        }
    }
}
