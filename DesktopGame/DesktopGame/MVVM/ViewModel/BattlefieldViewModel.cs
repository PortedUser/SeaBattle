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

            if (type == TypeField.EnemyField)
            {
                _type = type;
                CurrentField = new EnemyBattlefield();
            }
            else if (type == TypeField.MyField)
            {
                _type = type;
                CurrentField = new MyBattlefield(this);
            }
        }

        public bool Filled { get { return CurrentField.FieldFilled; } }

        public void StartGame()
        {
            CurrentField.StartGame();
            OnPropertyChanged(nameof(CurrentField));
        }

        public void StopGame()
        {
            CurrentField.StopGame();
            OnPropertyChanged(nameof(CurrentField));
        }

        public StateShip GetLastSetState()
        {
            return _parentVM.GetLastSetState();
        }
	}
}
