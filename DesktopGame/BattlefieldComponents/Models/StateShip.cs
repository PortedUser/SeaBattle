using DesktopGame.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.Model
{
    public class StateShip
    {
        public TypeShip CurrentType { get; set; }

        public AngleOfRotation AngleRotation { get; set; }

        public StateShip(TypeShip typeShip)
        { 
            CurrentType = typeShip;
        }

    }
}
