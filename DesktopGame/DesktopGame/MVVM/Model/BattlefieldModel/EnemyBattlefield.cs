using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.Domain.Interfaces;
using DesktopGame.MVVM.Model.BattlefieldModel;
using DesktopGame.MVVM.ViewModel;
using System;
using BattlefieldComponents.Core;
using BattlefieldComponents.SeaBattleBot;
using BattlefieldComponents.Models;
using System.Threading;

namespace DesktopGame.MVVM.Model
{
    class EnemyBattlefield : BaseBattlefield, IPlayingField
    {
        private BattlefieldViewModel _parentVM;
        private StateCell _baseState;
        private IPlayingVM _enemyVM;
        private Bot MeBot;

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public EnemyBattlefield(BattlefieldViewModel parentVM) : base()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            _parentVM = parentVM;
            _baseState = StateCell.Cloud;
            MeBot = new Bot();

            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }
            GenerateField(_baseState);
            foreach (BattlefieldCell item in this)
            {
                item.HideState(_baseState);
            }
        }

        public bool FieldFilled => FieldDictionary.IsFull;

        public ShotResult Shot(int x, int y)
        {
            return Shot(x, y, _baseState);
        }

        public void StartGame()
        {
            _enemyVM = _parentVM.GetEnemyVM();
            foreach (var item in Commands)
            {
                item.Command = new RelayCommand(o =>
                {
                    var x = item.X;
                    var y = item.Y;
                    var res = Shot(x, y);
                    if (!res.HitSuccessfully)
                    {
                        EnemyShot(MeBot.Shot());
                    }
                });
            }
        }

        private void EnemyShot(Point point)
        {
            StopGame();
            var res = _enemyVM.Shot(point.X, point.Y);
            if (res.HitSuccessfully)
            {
                MeBot.SetResultShot(res);
                EnemyShot(MeBot.Shot());
            }
            else
            {
                StartGame();
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

        public void SetRandomField()
        {
            GenerateField(_baseState);
        }

        public void DeleteAll()
        {
            DeleteAll(_baseState);
        }
    }
}
