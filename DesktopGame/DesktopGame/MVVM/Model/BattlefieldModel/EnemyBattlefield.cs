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
    class EnemyBattlefield : BaseBattlefield, IPlayingField
    {
        private BattlefieldViewModel _parentVM;
        private StateCell _baseState;
        private IPlayingVM _enemyVM;

        public EnemyBattlefield(BattlefieldViewModel parentVM) : base()
        {
            _parentVM = parentVM;
            _baseState = StateCell.Cloud;

            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }
            GenerateField();
            foreach (BattlefieldCell item in this)
            {
                item.HideState(_baseState);
            }
        }

        public bool FieldFilled => FieldDictionary.IsFull;

        public bool Shot(int x, int y)
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
                    if (!Shot(x, y))
                    {
                        RandomShot();
                    }
                });
            }
        }

        private void RandomShot()
        {
            StopGame();
            var rnd = new Random();
            var x = rnd.Next(0,9);
            var y = rnd.Next(0,9);
            if (_enemyVM.Shot(x,y))
            {
                _enemyVM.UpdateVM();
                RandomShot();
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

        private void GenerateField()
        {
            var count = 0;
            while (!FieldDictionary.IsFull && count < 5000)
            {
                count++;
                if (!FieldDictionary.FourShipIsFull)
                {
                    SetShipRandomPos(TypeShip.FourDeckShip, 0, 9);
                }
                if (!FieldDictionary.BowShipIsFull)
                {
                    SetShipRandomPos(TypeShip.BowShip, 0 + count % 10 / 10, 9);
                }
                if (!FieldDictionary.DoubleShipIsFull)
                {
                    SetShipRandomPos(TypeShip.DoubleDeckShip, 0, 8);
                }
                if (!FieldDictionary.ThreeShipIsFull)
                {
                    SetShipRandomPos(TypeShip.ThreeDeckShip, 1, 8);
                }
                
            }
            if (count >= 4999)
            {
                DeleteAll();
                GenerateField();
            }
        }

        private AngleOfRotation GenerateAngle()
        {
            var rnd = new Random();
            var value = rnd.Next(0, 100);
            if (value <=65 )
            {
                return AngleOfRotation.Angle_0;
            }
            else
            {
                return AngleOfRotation.Angle_90;
            }
        }

        private void SetShipRandomPos(TypeShip type, int a, int b)
        {
            var rnd = new Random();
            var rndX = rnd.Next(a, b);
            var rndY = rnd.Next(a, b);
            var state = new StateShip(type);
            state.AngleRotation = GenerateAngle();
            SetShip(rndX, rndY, state,_baseState);
        }

        private void DeleteAll()
        {
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }
            FieldDictionary.DeleteAll();
        }
    }
}
