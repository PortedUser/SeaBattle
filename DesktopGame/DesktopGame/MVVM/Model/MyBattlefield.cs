using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.Model
{
    class MyBattlefield : BaseBattlefield, IPlayingField
    {
        public MyBattlefield() : base()
        {
            
        }

        public void CreateField()
        {
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(StateCell.Wave);
            }
        }
    }
}
