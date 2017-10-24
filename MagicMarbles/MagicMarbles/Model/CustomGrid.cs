using MagicMarbles.Interfaces;

namespace MagicMarbles.Model
{
    public class CustomGrid : ICustomGrid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public CustomGrid()
        {
            Rows = 5;
            Columns = 5; 
        }

        public CustomGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }
    }
}
