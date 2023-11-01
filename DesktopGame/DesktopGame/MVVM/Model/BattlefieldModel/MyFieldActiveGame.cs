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
    class MyFieldActiveGame : BaseBattlefield, IPlayingField
    {

        public MyFieldActiveGame(IPlayingField field) : base()
        {
            for (int i = 0; i < 100; i++)
            {
                this[i].SetFullState(field[i].CurrentState);
            }
        }

        public bool FieldFilled => true; 

    }
}
