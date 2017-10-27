using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows.Controls;

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

        private static readonly Random Rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
