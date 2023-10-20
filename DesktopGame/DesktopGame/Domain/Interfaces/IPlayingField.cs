﻿using DesktopGame.Core;
using DesktopGame.MVVM.Model;
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
        public void SetParentVM(BattlefieldViewModel vm);
        public void CreateField();
        public BattlefieldCell this[int index] { get; set; }
        public BindingList<BattleCommand> Commands { get; set; }
    }
}
