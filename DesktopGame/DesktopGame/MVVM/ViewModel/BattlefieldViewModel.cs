using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model;
using BattlefieldComponents.Core;
using BattlefieldComponents.Models;

namespace DesktopGame.MVVM.ViewModel
{
    class BattlefieldViewModel: ObservableObject, IPlayingVM
    {
        private IPlayingField _curField;
        private BattleViewModel _parentVM;
        private TypeField _type;
        private IPlayingVM _enemyVM;

        public IPlayingField CurrentField
        {
            get { return _curField; }
            set { _curField = value; }
        }

        public BattlefieldViewModel(TypeField type, BattleViewModel vm) 
        {
            _parentVM = vm;
            _type = type;

            SetField();
        }

        public bool Filled { get { return CurrentField.FieldFilled; } }

        public void StartGame()
        {
            CurrentField.StartGame();
            OnPropertyChanged(nameof(CurrentField));
        }

        public void StopGame()
        {
            SetField();
            OnPropertyChanged(nameof(CurrentField));
        }

        public void SetEnemyVM(IPlayingVM vm)
        {
            _enemyVM = vm;
        }

        public IPlayingVM GetEnemyVM()
        {
            return _enemyVM;
        }

        public ShotResult Shot(int x, int y)
        {

            return CurrentField.Shot(x, y);
        }

        private void SetField()
        {
            if (_type == TypeField.EnemyField)
            {
                CurrentField = new EnemyBattlefield(this);
            }
            else if (_type == TypeField.MyField)
            {
                CurrentField = new MyBattlefield(this);
            }
        }

        public StateShip GetLastSetState()
        {
            return _parentVM.GetLastSetState();
        }

        public void UpdateVM()
        {
            OnPropertyChanged(nameof(CurrentField));
        }
    }
}
