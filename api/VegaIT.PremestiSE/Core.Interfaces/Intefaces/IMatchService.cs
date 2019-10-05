using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Intefaces
{
    public interface IMatchService
    {
        int GetTotalCount();
        void TryMatch(int id);
        void Unmatch(int id);
    }
}
