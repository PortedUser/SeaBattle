using BattlefieldComponents.Core;
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

        public ButtonState SwitchStateGame { get; set; }
        public ButtonState SetRandomField { get; set; }

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
            SwitchStateGame = new ButtonState();
            SetRandomField = new ButtonState();
            MyFieldVM = new BattlefieldViewModel(TypeField.MyField, this);
            EnemyFieldVM = new BattlefieldViewModel(TypeField.EnemyField, this);
            ControlVM = new BattlefieldControlViewModel();

            SetRandomField.Text = "Случайная расстановка";

            MyFieldVM.SetEnemyVM(EnemyFieldVM);
            EnemyFieldVM.SetEnemyVM(MyFieldVM);

            StopGame();

            SetRandomField.Command = new RelayCommand(o =>
            {
                MyFieldVM.CurrentField.DeleteAll();
                MyFieldVM.CurrentField.SetRandomField();
            });

            SwitchStateGame.Command = new RelayCommand(o =>
            {
                if (!SwitchStateGame.State)
                {
                    StartGame();
                    SetRandomField.Visibility = Visibility.Hidden;
                }
                else
                {
                    StopGame();
                    SetRandomField.Visibility = Visibility.Visible;
                }
            });
        }

        private void StartGame()
        {
            if (MyFieldVM.Filled && EnemyFieldVM.Filled)
            {
                MyFieldVM.StartGame();
                EnemyFieldVM.StartGame();
                SwitchStateGame.State = true;
                SwitchStateGame.Text = "Закончить игру";
                ControlVM = null;
                OnPropertyChanged(nameof(SwitchStateGame));
            }
            else
            {
                MessageBox.Show("Поле не готово");
            }
        }

        private void StopGame()
        {
            SwitchStateGame.State = false;
            MyFieldVM.StopGame();
            EnemyFieldVM.StopGame();
            SwitchStateGame.Text = "Начать игру";
            ControlVM = new BattlefieldControlViewModel();
            OnPropertyChanged(nameof(SwitchStateGame));
        }

        public StateShip GetLastSetState()
        {
            return ControlVM.GetLastState();
        }
    }
}
