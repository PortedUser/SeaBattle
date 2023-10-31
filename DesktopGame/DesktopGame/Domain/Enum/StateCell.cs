using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.Domain.Enum
{
    public enum StateCell : int
    {
        None =0,
        Wave =1,
        Cloud=2,
        Explosion=3,
        Delete =4,

        BowShip = 10,
        Deck2_1 = 20,
        Deck2_2 = 21,
        Deck3_1 = 30,
        Deck3_2 = 31,
        Deck3_3 = 32,
        Deck4_1 = 40, 
        Deck4_2 = 41,
        Deck4_3 = 42,
        Deck4_4 = 43,

        BowShip_90 =10 + 90,
        Deck2_1_90 =20 + 90,
        Deck2_2_90 =21 + 90,
        Deck3_1_90 =30 + 90,
        Deck3_2_90 =31 + 90,
        Deck3_3_90 =32 + 90,
        Deck4_1_90 =40 + 90,
        Deck4_2_90 =41 + 90,
        Deck4_3_90 =42 + 90,
        Deck4_4_90 =43 + 90,

        Rotate,
        Rotate_90
    }
}
