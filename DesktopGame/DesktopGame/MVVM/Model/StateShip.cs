using DesktopGame.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.Model
{
    class StateShip
    {
        public TypeShip CurrentType { get; private set; }
        public int AngleRotation { get; set; }

        public StateShip(TypeShip typeShip)
        { 
            CurrentType = typeShip;
        }

    }
}
