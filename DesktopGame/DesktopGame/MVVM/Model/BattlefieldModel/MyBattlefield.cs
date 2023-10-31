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

        public MyBattlefield() : base()
        {
            _fieldDictionary = new FieldDictionary();
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o =>
                {
                    var state = GetLastSetState();
                   

                    if (state.CurrentType == TypeShip.BowShip && !_fieldDictionary.BowShipIsFull)
                    {
                        SetShip(cm, state);
                    }
                    else if (state.CurrentType == TypeShip.DoubleDeckShip && !_fieldDictionary.DoubleShipIsFull)
                    {
                        SetShip(cm, state);
                    }
                    else if (state.CurrentType == TypeShip.ThreeDeckShip && !_fieldDictionary.ThreeShipIsFull)
                    {
                        SetShip(cm, state);
                    }
                    else if (state.CurrentType == TypeShip.FourDeckShip && !_fieldDictionary.FourShipIsFull)
                    {
                        SetShip(cm, state);
                    }
                    else if (state.CurrentType == TypeShip.Delete)
                    {
                        DeleteShip(cm);
                    }
                });
            }
        }

        private void SetShip(BattleCommand cm, StateShip state)
        {
            var x = cm.X;
            var y = cm.Y;

            var shiftModulusX = state.AngleRotation == AngleOfRotation.Angle_90 ? 1 : 0;
            var shiftModulusY = state.AngleRotation == AngleOfRotation.Angle_0 ? 1 : 0;

            var lengthShip = (int)state.CurrentType / 10;

            var shiftX = GetShift(lengthShip, shiftModulusX);
            var shiftY = GetShift(lengthShip, shiftModulusY);

            var finX = x + shiftX;
            var finY = y + shiftY;

            var segementManager = new ShipSegments();
            

            if (finX < 10 && finY < 10 && CheckSpace(shiftX, shiftY, x, y))
            {
                _fieldDictionary.AddShip(x, y, state);
                for (int i = 0; i < lengthShip; i++)
                {
                    var currX = x + i * shiftModulusX;
                    var currY = y + i * shiftModulusY;

                    var newState = segementManager[state.CurrentType, state.AngleRotation, i];
                    this[currX, currY].SetFullState(newState);
                }
            }
        }

        private int GetShift(int length, int modulus)
        {
            return (length - 1) * modulus;
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

        public void CreateField()
        {
            _baseStateCell = StateCell.Wave;

            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseStateCell);
            }
        }

        public void SetParentVM(BattlefieldViewModel vm)
        {
            _parentVM = vm;
        }

        private StateShip GetLastSetState()
        {
            return _parentVM.GetLastSetState();
        }


        private bool CheckSpace(int shiftX, int shiftY, int x, int y)
        {
            shiftX += x;
            shiftY += y;
            for (var iX = x; iX <= shiftX; iX++)
            {
                for (var iY = y; iY <= shiftY; iY++)
                {
                    var res = CheckArea(iX, iY);
                    if (!res)
                        return res;
                }
            }
            return true;
        }

        private bool CheckArea(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var X = x + i;
                    var Y = y + j;
                    if (X < 10 && Y < 10 && X > -1 && Y > -1)
                    {
                        if (this[X, Y].CurrentState != _baseStateCell)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
