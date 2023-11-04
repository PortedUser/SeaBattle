using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model;
using DesktopGame.MVVM.Model.BattlefieldModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.ViewModel
{
    class BattlefieldViewModel: ObservableObject
    {
        private IPlayingField _curField;
        private BattleViewModel _parentVM;
        private TypeField _type;

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

        public bool Shoot(int x, int y)
        {
            CurrentField.Shoot(x, y);
            return true;
        }

        public void SetActiveMode()
        {
            CurrentField.StartGame();
        }

        public void SetStandbyMode()
        {

        }

        private void SetField()
        {
            if (_type == TypeField.EnemyField)
            {
                CurrentField = new EnemyBattlefield();
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
	}
}
