using DesktopGame.Core;
using DesktopGame.MVVM.Model.BattlefieldModel;
using DesktopGame.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.Domain.Interfaces
{
    interface IPlayingField
    {
        public BattlefieldCell this[int index] { get; set; }
        public BindingList<BattleCommand> Commands { get; set; }
        public bool FieldFilled { get; }
        public void StartGame();
        public void StopGame();

        public void Shoot(int x, int y);

        public void SetStandbyMode();
    }
}
