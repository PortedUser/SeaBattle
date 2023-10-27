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
    class BattleViewModel : ObservableObject
    {
        private BattlefieldViewModel _myBattleField;
        private BattlefieldViewModel _enemyBattleField;
        private BattlefieldControlViewModel _controlVM;

        public RelayCommand StartGame { get; set; }

        public BattlefieldViewModel MyField
        {
            get { return _myBattleField; }
            set { _myBattleField = value; OnPropertyChanged(); }
        }

        public BattlefieldViewModel EnemyField
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
            MyField = new BattlefieldViewModel();
            MyField.CreateField(TypeField.MyField);
            MyField.SetParentVM(this);

            EnemyField = new BattlefieldViewModel();
            EnemyField.CreateField(TypeField.EnemyField);
            EnemyField.SetParentVM(this);

            ControlVM = new BattlefieldControlViewModel();

            StartGame = new RelayCommand(o =>
            {
                MyField.StartGame();
                EnemyField.StartGame();
            });
        }

        public StateShip GetLastSetState()
        {
            return ControlVM.GetLastState();
        }
    }
}
