using DesktopGame.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.Domain.Interfaces
{
    interface IPlayingField
    {
        public void CreateField();
        public BattlefieldCell this[int index] { get; set; }
    }
}
