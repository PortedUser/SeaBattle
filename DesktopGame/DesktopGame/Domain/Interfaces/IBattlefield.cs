using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGame.MVVM.Model.BattlefieldModel;

namespace DesktopGame.Domain.Interfaces
{
    interface IBattlefield : IEnumerable 
    { 
        BattlefieldCell this[int index] { get; set; }
        BattlefieldCell this[int x,int y] { get; set; }
    }
}
