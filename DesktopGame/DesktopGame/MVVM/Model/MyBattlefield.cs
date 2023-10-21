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
        public MyBattlefield() : base()
        {
            

            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o =>
                {
                    var state = _parentVM.GetLastSetState();

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
            if (y + 1 < 10 && angle == 0)
            {
                this[x, y].SetFullState(StateCell.Deck2_1);
                this[x , y + 1 ].SetFullState(StateCell.Deck2_2);
            }
        }

        private void SetThreeShip(BattleCommand cm, int angle)
        {
            var x = cm.X;
            var y = cm.Y;
            if (y + 2 < 10 && angle == 0)
            {
                this[x, y].SetFullState(StateCell.Deck3_1);
                this[x, y + 1].SetFullState(StateCell.Deck3_2);
                this[x, y + 2].SetFullState(StateCell.Deck3_3);
            }
        }

        private void SetFourShip(BattleCommand cm, int angle)
        {
            var x = cm.X;
            var y = cm.Y;
            if (y + 3 < 10 && angle == 0)
            {
                this[x, y].SetFullState(StateCell.Deck4_1);
                this[x, y + 1].SetFullState(StateCell.Deck4_2);
                this[x, y + 2].SetFullState(StateCell.Deck4_3);
                this[x, y + 3].SetFullState(StateCell.Deck4_4);
            }
        }

        private void SetBowShip(BattleCommand cm, int angle)
        {
            if (angle == 0)
            {
                this[cm.X, cm.Y].SetFullState(StateCell.BowShip);
            }else if (true)
            {

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
    }
}
