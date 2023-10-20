﻿using DesktopGame.Core;
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
                CurrentField = new EnemyBattlefield();
                CurrentField.CreateField();
                CurrentField.SetParentVM(this);
                OnPropertyChanged();
            }
            else if (type == TypeField.MyField)
            {
                CurrentField = new MyBattlefield();
                CurrentField.CreateField();
                CurrentField.SetParentVM(this);
                OnPropertyChanged();
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
