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
        private bool _gameIsActive;
        public string GameState { get; set; }
        public RelayCommand SwitchStateGame { get; set; }

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
            
            MyFieldVM = new BattlefieldViewModel(TypeField.MyField, this);
            EnemyFieldVM = new BattlefieldViewModel(TypeField.EnemyField, this);
            ControlVM = new BattlefieldControlViewModel();

            MyFieldVM.SetEnemyVM(EnemyFieldVM);
            EnemyFieldVM.SetEnemyVM(MyFieldVM);

            StopGame();

            SwitchStateGame = new RelayCommand(o =>
            {
                if (!_gameIsActive)
                {
                    StartGame();
                }
                else
                {
                    StopGame();
                }
            });
        }

        private void StartGame()
        {
            if (MyFieldVM.Filled && EnemyFieldVM.Filled)
            {
                MyFieldVM.StartGame();
                EnemyFieldVM.StartGame();
                _gameIsActive = true;
                GameState = "Закончить игру";
                ControlVM = null;
                OnPropertyChanged(nameof(GameState));
            }
            else
            {
                MessageBox.Show("Поле не готово");
            }
        }

        private void StopGame()
        {
            _gameIsActive = false;
            MyFieldVM.StopGame();
            EnemyFieldVM.StopGame();
            GameState = "Начать игру";
            ControlVM = new BattlefieldControlViewModel();
            OnPropertyChanged(nameof(GameState));
        }

        public StateShip GetLastSetState()
        {
            return ControlVM.GetLastState();
        }
    }
}
