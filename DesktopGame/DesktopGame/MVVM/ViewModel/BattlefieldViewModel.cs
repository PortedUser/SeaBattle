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
        private IPlayingField _curField;
        private BattleViewModel _parentVM;
        private TypeField _type;

        public IPlayingField CurrentField
        {
            get { return _curField; }
            set { _curField = value; }
        }

        public BattlefieldViewModel() 
        {
            
        }

        public void CreateField(TypeField type)
        {
            if (type == TypeField.EnemyField)
            {
                _type = type;
                CurrentField = new EnemyBattlefield();
                CurrentField.CreateField();
                CurrentField.SetParentVM(this);
                OnPropertyChanged();
            }
            else if (type == TypeField.MyField)
            {
                _type = type;
                CurrentField = new MyBattlefield();
                CurrentField.CreateField();
                CurrentField.SetParentVM(this);
                OnPropertyChanged();
            }
        }

        public void StartGame()
        {
            if (_type == TypeField.MyField && CurrentField.FieldFilled)
            {
                CurrentField = new MyFieldActiveGame(CurrentField);
                OnPropertyChanged(nameof(CurrentField));
            }
            else if (_type == TypeField.EnemyField)
            {

            }
        }

        public void SetParentVM(BattleViewModel vm)
        {
            _parentVM = vm;
        }

        public StateShip GetLastSetState()
        {
            return _parentVM.GetLastSetState();
        }
	}
}
