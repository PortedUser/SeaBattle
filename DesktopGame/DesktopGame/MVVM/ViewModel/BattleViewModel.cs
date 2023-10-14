using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.ViewModel
{
    class BattleViewModel : ObservableObject
    {
        private BattlefieldViewModel _myBattleField;
        private BattlefieldViewModel _enemyBattleField;
        private BattlefieldControlViewModel _controlVM;

        public BattlefieldViewModel MyBattleField
        {
            get { return _myBattleField; }
            set { _myBattleField = value; OnPropertyChanged(); }
        }

        public BattlefieldViewModel EnemyBattleField
        {
            get { return _enemyBattleField; }
            set { _enemyBattleField = value; OnPropertyChanged(); }
        }

        public BattlefieldControlViewModel ControlVM
        {
            get { return _controlVM; }
            set { _controlVM = value; OnPropertyChanged(); }
        }


        public BattleViewModel() 
        {
            MyBattleField = new BattlefieldViewModel();
            MyBattleField.CreateField(TypeField.MyField);

            EnemyBattleField = new BattlefieldViewModel();
            EnemyBattleField.CreateField(TypeField.EnemyField);

            ControlVM = new BattlefieldControlViewModel();
        }
    }
}
