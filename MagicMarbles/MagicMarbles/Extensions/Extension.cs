using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MagicMarbles.Extensions
{
    public static class Extension
    {
        public static T[,] To2DArray<T>(this IEnumerable<T> input, params object[] parameters)
        {
            var rowsCount = (int)parameters[0];
            var colsCount = (int)parameters[1];

            var ret = new T[rowsCount, colsCount];
            int i = 0, j = 0;
            foreach (var currentEntry in input)
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

        public static ObservableCollection<T> Array2DToCollection<T>(this T[,] array)
        {
            var list = new List<T>();
            var size1 = array.GetLength(1);
            var size0 = array.GetLength(0);
            for (int i = 0; i < size0; i++)
            {
                for (int j = 0; j < size1; j++)
                    list.Add(array[i, j]);
            }
            return new ObservableCollection<T>(list);
        }
    }
}
