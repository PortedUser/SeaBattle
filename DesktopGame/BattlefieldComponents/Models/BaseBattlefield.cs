using DesktopGame.Domain.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using DesktopGame.MVVM.Model.BattlefieldModel;
using System.Linq;
using System.Windows;
using DesktopGame.Domain.Enum;
using System.Collections.Generic;
using BattlefieldComponents.Core;
using BattlefieldComponents.Models;

namespace DesktopGame.MVVM.Model
{
    public abstract class BaseBattlefield : ObservableObject, IBattlefield
    {
        private BindingList<BindingList<BattlefieldCell>> _battle;
        private BindingList<BattleCommand> _commands;
        private FieldDictionary _fieldDictionary;
        private int _countSunkShip;
        public bool IsLose { get; private set; }

        protected FieldDictionary FieldDictionary
        {
            get { return _fieldDictionary; }
            set { _fieldDictionary = value; }
        }


        public BindingList<BattleCommand> Commands
        {
            get { return _commands; }
            set { _commands = value;  OnPropertyChanged(); }
        }


        public BaseBattlefield() 
        {
            _battle = new BindingList<BindingList<BattlefieldCell>>();
            _commands = new BindingList<BattleCommand>();
            _fieldDictionary = new FieldDictionary();

            for (int x = 0; x < 10; x++)
            {
                _battle.Add(new BindingList<BattlefieldCell>());
                for (int y = 0; y < 10; y++)
                {
                    _battle[x].Add(new BattlefieldCell(x, y));
                    _commands.Add(new BattleCommand(x,y));
                }
            }
        }

        public BattlefieldCell this[int index]
        {
            get
            {
                try
                {
                    int x = index / 10;
                    int y = index % 10;
                    return _battle[x][y];
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                } 
            }
            set 
            {
                try
                {
                    int x = index / 10;
                    int y = index % 10;
                    _battle[x][y] = value;
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                }
            }
        }

        public BattlefieldCell this[int x, int y] 
        {
            get 
            {
                try
                {
                    return _battle[x][y];
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                }
            }
            set {
                try
                {
                    _battle[x][y] = value;
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                }
            }
        }

        private Exception GetIndexException(Exception e)
        {
            MessageBox.Show($"Не верный индекс при обращении к Battlefield: {e}");
            throw new Exception($"Не верный индекс при обращении к Battlefield: {e}");
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var line in _battle)
            {
                foreach(var cell in line)
                {
                    yield return cell;
                }
            }
        }

        protected ShotResult Shot(int x, int y, StateCell baseState)
        {
            if (this[x, y].CurrentState == baseState)
            {
                this[x, y].SetFullState(StateCell.Miss);
            }
            else if (this[x, y].CurrentState != StateCell.Miss)
            {
                this[x, y].SetFullState(StateCell.Explosion);
                var ship = FieldDictionary.GetShip(x, y);
                if (ship != null)
                {
                    if (IsShipSunk(ship))
                    {
                        _countSunkShip++;
                        if (_countSunkShip == 10) { IsLose = true; OnPropertyChanged(nameof(IsLose)); }
                        EncloseShip(ship, baseState);
                        return new ShotResult(true,true);
                    }
                    return new ShotResult(true);
                }
            }
            return new ShotResult();
        }

        private void EncloseShip(List<Point> ship, StateCell baseState)
        {
            for (int i = 0; i < ship.Count; i++)
            {
                for(int xi = -1;  xi <= 1; xi++)
                {
                    for (int yi = -1; yi <= 1; yi++)
                    {
                        var currX = ship[i].X + xi;
                        var currY = ship[i].Y + yi;
                        SetMiss(currX, currY, baseState);                    
                    }
                }
            }
        }

        private void SetMiss(int x, int y, StateCell baseState)
        {
            if (x < 10 && y < 10 && x > -1 && y > -1)
            {
                if (this[x,y].CurrentState == baseState)
                {
                    this[x, y].SetFullState(StateCell.Miss);
                }
            }
        }

        public bool IsShipSunk(List<Point> ship)
        {
            foreach (var point in ship)
            {
                if (this[point.X,point.Y].CurrentState != StateCell.Explosion)
                {
                    return false;
                }
            }
            return true;
        }

        protected void SetShip(BattleCommand cm, StateShip state,StateCell _baseStateCell)
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

            var segmentManager = new ShipSegments();


            if (finX < 10 && finY < 10 && CheckSpace(shiftX, shiftY, x, y, _baseStateCell))
            {
                FieldDictionary.AddShip(x, y, state);
                for (int i = 0; i < lengthShip; i++)
                {
                    var currX = x + i * shiftModulusX;
                    var currY = y + i * shiftModulusY;

                    var newState = segmentManager[state.CurrentType, state.AngleRotation, i];
                    this[currX, currY].SetFullState(newState);
                }
            }
        }

        protected void SetShip(int x, int y, StateShip state,StateCell _baseStateCell)
        {
            SetShip(new BattleCommand(x,y), state, _baseStateCell);
        }

        private int GetShift(int length, int modulus)
        {
            return (length - 1) * modulus;
        }

        private bool CheckSpace(int shiftX, int shiftY, int x, int y,StateCell _baseStateCell)
        {
            shiftX += x;
            shiftY += y;
            for (var iX = x; iX <= shiftX; iX++)
            {
                for (var iY = y; iY <= shiftY; iY++)
                {
                    var res = CheckArea(iX, iY, _baseStateCell);
                    if (!res)
                        return res;
                }
            }
            return true;
        }

        private bool CheckArea(int x, int y, StateCell _baseStateCell)
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

        protected void GenerateField(StateCell _baseState)
        {
            var count = 0;
            while (!FieldDictionary.IsFull && count < 5000)
            {
                count++;
                if (!FieldDictionary.FourShipIsFull)
                {
                    SetShipRandomPos(_baseState, TypeShip.FourDeckShip, 0, 9);
                }
                if (!FieldDictionary.BowShipIsFull)
                {
                    SetShipRandomPos(_baseState, TypeShip.BowShip, 0 + count % 10 / 10, 9);
                }
                if (!FieldDictionary.DoubleShipIsFull)
                {
                    SetShipRandomPos(_baseState, TypeShip.DoubleDeckShip, 0, 8);
                }
                if (!FieldDictionary.ThreeShipIsFull)
                {
                    SetShipRandomPos(_baseState , TypeShip.ThreeDeckShip, 1, 8);
                }

            }
            if (count >= 4999)
            {
                DeleteAll(_baseState);
                GenerateField(_baseState);
            }
        }

        private AngleOfRotation GenerateAngle()
        {
            var rnd = new Random();
            var value = rnd.Next(0, 100);
            if (value <= 65)
            {
                return AngleOfRotation.Angle_0;
            }
            else
            {
                return AngleOfRotation.Angle_90;
            }
        }

        private void SetShipRandomPos(StateCell _baseState, TypeShip type, int a, int b)
        {
            var rnd = new Random();
            var rndX = rnd.Next(a, b);
            var rndY = rnd.Next(a, b);
            var state = new StateShip(type);
            state.AngleRotation = GenerateAngle();
            SetShip(rndX, rndY, state, _baseState);
        }

        public void DeleteAll(StateCell _baseState)
        {
            foreach (BattlefieldCell item in this)
            {
                item.SetFullState(_baseState);
            }
            FieldDictionary.DeleteAll();
        }
    }
}
