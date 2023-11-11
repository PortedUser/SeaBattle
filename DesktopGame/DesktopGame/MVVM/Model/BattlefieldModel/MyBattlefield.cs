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
        private FieldDictionary _fieldDictionary;

        public bool FieldFilled { get { return _fieldDictionary.IsFull; } }

        public MyBattlefield(BattlefieldViewModel vm) : base()
        {
            _parentVM = vm;
            _baseState = StateCell.Wave;
            _fieldDictionary = new FieldDictionary();

            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }

            SetPlacementCommands();
        }

        private void DeleteShip(BattleCommand cm)
        {
            var ship = _fieldDictionary.GetAndDelPosShip(cm.X, cm.Y);
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

                    if (state.CurrentType == TypeShip.BowShip && !_fieldDictionary.BowShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseState);
                    }
                    else if (state.CurrentType == TypeShip.DoubleDeckShip && !_fieldDictionary.DoubleShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseState);
                    }
                    else if (state.CurrentType == TypeShip.ThreeDeckShip && !_fieldDictionary.ThreeShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseState);
                    }
                    else if (state.CurrentType == TypeShip.FourDeckShip && !_fieldDictionary.FourShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseState);
                    }
                    else if (state.CurrentType == TypeShip.Delete)
                    {
                        DeleteShip(cm);
                    }
                });
            }
        }

        public bool Shoot(int x, int y)
        {
            return Shoot(x, y, _baseState, _fieldDictionary);
        }

        public void SetStandbyMode()
        {
            StartGame();
        }
    }
}
