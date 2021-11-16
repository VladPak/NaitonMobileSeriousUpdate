using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Interfaces
{
    public interface IRoleRightManager
    {
        void GetAll();
        bool CheckRole(string roleRight);
    }
}
