using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopGame.MVVM.ViewModel
{
    class BattleViewModel : ObservableObject
    {
        private BattlefieldViewModel _myBattleField;
        private BattlefieldViewModel _enemyBattleField;
        private BattlefieldControlViewModel _controlVM;

        public RelayCommand StartGame { get; set; }

        public BattlefieldViewModel MyFieldVM
        {
            get { return _myBattleField; }
            set { _myBattleField = value; OnPropertyChanged(); }
        }

        public BattlefieldViewModel EnemyFieldVM
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
            MyFieldVM = new BattlefieldViewModel();
            MyFieldVM.CreateField(TypeField.MyField);
            MyFieldVM.SetParentVM(this);

            EnemyFieldVM = new BattlefieldViewModel();
            EnemyFieldVM.CreateField(TypeField.EnemyField);
            EnemyFieldVM.SetParentVM(this);

            ControlVM = new BattlefieldControlViewModel();

            StartGame = new RelayCommand(o =>
            {
                if (MyFieldVM.Filled && EnemyFieldVM.Filled)
                {
                    MyFieldVM.StartGame();
                    EnemyFieldVM.StartGame();
                }
                else
                {
                    MessageBox.Show("Игровое поле не готово");
                }
                
            });
        }

        public StateShip GetLastSetState()
        {
            return ControlVM.GetLastState();
        }
    }
}
