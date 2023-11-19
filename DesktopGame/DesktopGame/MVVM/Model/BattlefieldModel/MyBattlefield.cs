using BattlefieldComponents.Core;
using BattlefieldComponents.Models;
using BattlefieldComponents.SeaBattleBot;
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
        private IPlayingVM _enemyVM;

        public bool FieldFilled { get { return FieldDictionary.IsFull; } }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public MyBattlefield(BattlefieldViewModel vm) : base()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
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
            if (!Commands[0].Equals(new RelayCommand(o => { })))
            {
                SetEmptyCommands();
            }
        }

        public void StopGame()
        {
            if (!Commands[0].Equals(new RelayCommand(o => { })))
            {
                SetEmptyCommands();
            }
        }

        private void SetEmptyCommands()
        {
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o => { });
            }
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

        public ShotResult Shot(int x, int y)
        {
            return Shot(x, y, _baseState);
        }
    }
}
