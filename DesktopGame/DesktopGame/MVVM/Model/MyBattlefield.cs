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
                        SetBowShip(cm, state);
                    }
                    else if (state.CurrentType == TypeShip.DoubleDeckShip)
                    {

                    }
                    else if (state.CurrentType == TypeShip.ThreeDeckShip)
                    {

                    }
                    else if (state.CurrentType == TypeShip.FourDeckShip)
                    {

                    }
                });
            }
        }

        private void SetDoubleShip()
        {

        }

        private void SetBowShip(BattleCommand cm,StateShip state)
        {
            this[cm.X, cm.Y].SetFullState(StateCell.BowShip);
            
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
