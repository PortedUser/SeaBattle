﻿using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.Model
{
    class EnemyBattlefield : BaseBattlefield, IPlayingField
    {
        public EnemyBattlefield() : base()
        { 

        }

        public void CreateField()
        {
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(StateCell.Cloud);
            }
        }
    }
}
