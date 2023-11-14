using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model.BattlefieldModel;
using DesktopGame.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.Model
{
    class MyBattlefield : BaseBattlefield, IPlayingField
    {
        private BattlefieldViewModel _parentVM;
        private StateCell _baseState;

        public bool FieldFilled { get { return FieldDictionary.IsFull; } }

        public MyBattlefield(BattlefieldViewModel vm) : base()
        {
            _parentVM = vm;
            _baseState = StateCell.Wave;

            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }

            SetPlacementCommands();
        }

        private void DeleteShip(BattleCommand cm)
        {
            var ship = FieldDictionary.GetAndDelPosShip(cm.X, cm.Y);
            if (ship != null)
            {
                foreach (var point in ship)
                {
                    this[point.X, point.Y].SetFullState(_baseState);
                }
            }
        }

        private StateShip GetLastSetState()
        {
            return _parentVM.GetLastSetState();
        }

        public void StartGame()
        {
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o => { });
            }
        }

        public void StopGame()
        {
            SetPlacementCommands();
        }

        private void SetPlacementCommands()
        {
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o =>
                {
                    var state = GetLastSetState();

                    if (state.CurrentType == TypeShip.Delete)
                    {
                        DeleteShip(cm);
                    }
                    else if (!FieldDictionary.IsShipsFull(state.CurrentType))
                    {
                        SetShip(cm, state,_baseState);
                    }  
                });
            }
        }

        public bool Shoot(int x, int y)
        {
            return Shoot(x, y, _baseState);
        }
    }
}
