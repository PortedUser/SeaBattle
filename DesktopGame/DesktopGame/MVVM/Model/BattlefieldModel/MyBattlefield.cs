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
        private StateCell _baseStateCell;
        private FieldDictionary _fieldDictionary;

        public bool FieldFilled { 
            get 
            {
                return _fieldDictionary.BowShipIsFull 
                    && _fieldDictionary.DoubleShipIsFull 
                    && _fieldDictionary.FourShipIsFull
                    && _fieldDictionary.ThreeShipIsFull;    
            } 
        }

        public MyBattlefield(BattlefieldViewModel vm) : base()
        {
            _parentVM = vm;
            _baseStateCell = StateCell.Wave;

            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseStateCell);
            }

            _fieldDictionary = new FieldDictionary();
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o =>
                {
                    var state = GetLastSetState();
                   

                    if (state.CurrentType == TypeShip.BowShip && !_fieldDictionary.BowShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseStateCell);
                    }
                    else if (state.CurrentType == TypeShip.DoubleDeckShip && !_fieldDictionary.DoubleShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseStateCell);
                    }
                    else if (state.CurrentType == TypeShip.ThreeDeckShip && !_fieldDictionary.ThreeShipIsFull)
                    {
                        SetShip(cm, state, _fieldDictionary, _baseStateCell);
                    }
                    else if (state.CurrentType == TypeShip.FourDeckShip && !_fieldDictionary.FourShipIsFull)
                    {
                        SetShip(cm, state,_fieldDictionary, _baseStateCell);
                    }
                    else if (state.CurrentType == TypeShip.Delete)
                    {
                        DeleteShip(cm);
                    }
                });
            }
        }

        

        private void DeleteShip(BattleCommand cm)
        {
            var ship = _fieldDictionary.GetAndDelPosShip(cm.X, cm.Y);
            if (ship != null)
            {
                foreach (var point in ship)
                {
                    this[point.X, point.Y].SetFullState(_baseStateCell);
                }
            }
        }

        private StateShip GetLastSetState()
        {
            return _parentVM.GetLastSetState();
        }


        
    }
}
