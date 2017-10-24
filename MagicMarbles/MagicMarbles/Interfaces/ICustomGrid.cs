using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMarbles.Interfaces
{
    public interface ICustomGrid
    {
        int Rows { get; set; }
        int Columns { get; set; }
    }
}
