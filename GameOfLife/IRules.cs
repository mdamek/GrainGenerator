using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public interface IRules
    {
        ToState ChangeState(int neighborhoodsNumber, bool isAlive);
    }
}
