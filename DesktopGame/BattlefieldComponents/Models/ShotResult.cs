using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattlefieldComponents.Models
{
    public class ShotResult
    {
        public bool HitSuccessfully { get; }
        public bool ShipSunk { get;}
        public ShotResult(bool hitSuccessfully = false, bool shipSunk = false)
        {
            HitSuccessfully = hitSuccessfully;
            ShipSunk = shipSunk;
        }
    }
}
