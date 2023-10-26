using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model;
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

        public MyBattlefield() : base()
        {
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o =>
                {
                    var state = GetLastSetState();

                    if (state.CurrentType == TypeShip.BowShip)
                    {
                        SetBowShip(cm, state.AngleRotation);
                    }
                    else if (state.CurrentType == TypeShip.DoubleDeckShip)
                    {
                        SetDoubleShip(cm, state.AngleRotation);
                    }
                    else if (state.CurrentType == TypeShip.ThreeDeckShip)
                    {
                        SetThreeShip(cm, state.AngleRotation);
                    }
                    else if (state.CurrentType == TypeShip.FourDeckShip)
                    {
                        SetFourShip(cm, state.AngleRotation);
                    }
                });
            }
        }

        private void SetDoubleShip(BattleCommand cm, int angle)
        {
            var x = cm.X;
            var y = cm.Y;
            if (y + 1 < 10 && angle == 0 && CheckSpace(0,1,x,y))
            {
                this[x, y].SetFullState(StateCell.Deck2_1);
                this[x , y + 1 ].SetFullState(StateCell.Deck2_2);
                _fieldDictionary.AddDoubleShip(x, y, angle);
            }
            else if (x + 1 < 10 && CheckSpace(1,0,x,y) && angle == 90)
            {
                this[x, y].SetFullState(StateCell.Deck2_1_90);
                this[x + 1, y].SetFullState(StateCell.Deck2_2_90);
                _fieldDictionary.AddDoubleShip(x, y, angle);
            }
        }

        private void SetThreeShip(BattleCommand cm, int angle)
        {
            var x = cm.X;
            var y = cm.Y;
            if (y + 2 < 10 && angle == 0 && CheckSpace(0,2,x,y))
            {
                this[x, y].SetFullState(StateCell.Deck3_1);
                this[x, y + 1].SetFullState(StateCell.Deck3_2);
                this[x, y + 2].SetFullState(StateCell.Deck3_3);
                _fieldDictionary.AddThreeShip(x, y, angle); 
            }else if (x + 2 < 10 && CheckSpace(2,0,x,y) && angle == 90)
            {
                this[x, y].SetFullState(StateCell.Deck3_1_90);
                this[x + 1, y].SetFullState(StateCell.Deck3_2_90);
                this[x + 2, y].SetFullState(StateCell.Deck3_3_90);
                _fieldDictionary.AddThreeShip(x, y, angle);
            }
        }

        private void SetFourShip(BattleCommand cm, int angle)
        {
            var x = cm.X;
            var y = cm.Y;
            if (y + 3 < 10 && angle == 0 && CheckSpace(0, 3, x, y))
            {
                this[x, y].SetFullState(StateCell.Deck4_1);
                this[x, y + 1].SetFullState(StateCell.Deck4_2);
                this[x, y + 2].SetFullState(StateCell.Deck4_3);
                this[x, y + 3].SetFullState(StateCell.Deck4_4);
                _fieldDictionary.AddFourShip(x, y, angle);
            }else if(x + 3 < 10 && CheckSpace(3, 0, x, y) && angle == 90)
            {
                this[x, y].SetFullState(StateCell.Deck4_1_90);
                this[x + 1, y].SetFullState(StateCell.Deck4_2_90);
                this[x + 2, y].SetFullState(StateCell.Deck4_3_90);
                this[x + 3, y].SetFullState(StateCell.Deck4_4_90);
                _fieldDictionary.AddFourShip(x, y, angle);
            }
        }

        private void SetBowShip(BattleCommand cm, int angle)
        {
            if (angle == 0 && CheckArea(cm.X, cm.Y))
            {
                this[cm.X, cm.Y].SetFullState(StateCell.BowShip);
                _fieldDictionary.AddBowShip(cm.X, cm.Y);
            }else if ( angle == 90 && CheckArea(cm.X, cm.Y))
            {
                this[cm.X, cm.Y].SetFullState(StateCell.BowShip_90);
                _fieldDictionary.AddBowShip(cm.X, cm.Y);
            }
            
        }

        public void CreateField()
        {
            _baseStateCell = StateCell.Wave;
            _fieldDictionary = new FieldDictionary();

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
