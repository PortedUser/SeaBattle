using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.ViewModel;
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

        public bool FieldFilled => true;

        public void CreateField()
        {
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(StateCell.Cloud);
            }
        }

        public void SetParentVM(BattlefieldViewModel vm)
        {
            
        }
    }
}
