using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.ViewModel
{
    class BattlefieldViewModel: ObservableObject
    {
        private IPlayingField _curVM;

        public IPlayingField CurrentVM
        {
            get { return _curVM; }
            set { _curVM = value; }
        }

        public BattlefieldViewModel(TypeField typeField) 
        {
            if (typeField == TypeField.EnemyField)
            {
                CurrentVM = new EnemyBattlefield();
            }
            else if (typeField == TypeField.MyField)
            {
                CurrentVM = new MyBattlefield();
            }
        }
	}
}
