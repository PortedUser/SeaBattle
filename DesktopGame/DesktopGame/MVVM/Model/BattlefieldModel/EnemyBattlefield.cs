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
        private FieldDictionary _fieldDictionary;
        private BattlefieldViewModel _parentVM;
        private StateCell _baseState;

        public EnemyBattlefield(BattlefieldViewModel parentVM) : base()
        {
            _parentVM = parentVM;
            _fieldDictionary = new FieldDictionary();
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

        public bool FieldFilled => true;

        public bool Shoot(int x, int y)
        {
            return Shoot(x, y, _baseState, _fieldDictionary);
        }

        public void StartGame()
        {
            foreach (var item in Commands)
            {
                item.Command = new RelayCommand(o =>
                {
                    var x = item.X;
                    var y = item.Y;
                    if (Shoot(x, y))
                    {
                        
                    }
                });
            }
        }

        public void StopGame()
        {
            foreach (var cm in Commands)
            {
                cm.Command = new RelayCommand(o => { });
            }
        }

        private void GenerateField()
        {
            var count = 0;
            while (!_fieldDictionary.IsFull && count < 5000)
            {
                count++;
                if (!_fieldDictionary.FourShipIsFull)
                {
                    SetShipRandomPos(TypeShip.FourDeckShip,0,8);
                }
                if (!_fieldDictionary.BowShipIsFull)
                {
                    SetShipRandomPos(TypeShip.BowShip,0 + count%10/10, 9);
                }
                 if (!_fieldDictionary.DoubleShipIsFull)
                {
                    SetShipRandomPos(TypeShip.DoubleDeckShip,2 ,8);
                }
                if (!_fieldDictionary.ThreeShipIsFull)
                {
                    SetShipRandomPos(TypeShip.ThreeDeckShip,1,8);
                }
                
            }
            if (count >= 999)
            {
                DeleteAll();
                GenerateField();
            }

        }

        private AngleOfRotation GenerateAngle()
        {
            var rnd = new Random();
            var value = rnd.Next(0, 100);
            if (value <=50 )
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
            SetShip(rndX, rndY, state, _fieldDictionary, _baseState);
        }

        private void DeleteAll()
        {
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }
            _fieldDictionary.DeleteAll();
        }

        public void SetStandbyMode()
        {
            StopGame();
        }
    }
}
