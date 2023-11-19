using DesktopGame.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattlefieldComponents.SeaBattleBot
{
    public class MarkedPoint : Point
    {
        public bool IsWasAttacked { get; private set; }
        public MarkedPoint(int x, int y) : base(x, y)
        {
        }

        public MarkedPoint(Point point) : base(point)
        { 
        }

        public void WasAttacked()
        {
            IsWasAttacked = true;
        }
    }
}
