using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model.BattlefieldModel;
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
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(StateCell.Cloud);
            }
        }

        public bool FieldFilled => true;

        public void Shoot(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            //TODO
        }

        public void StopGame()
        {
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o => { });
            }
        }
    }
}
