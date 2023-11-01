﻿using DesktopGame.Core;
using DesktopGame.Domain.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using DesktopGame.MVVM.Model.BattlefieldModel;
using System.Linq;
using System.Windows;
using DesktopGame.Domain.Enum;

namespace DesktopGame.MVVM.Model
{
    class BaseBattlefield : ObservableObject, IBattlefield
    {
        private BindingList<BindingList<BattlefieldCell>> _battle;
        private BindingList<BattleCommand> _commands;

        public BindingList<BattleCommand> Commands
        {
            get { return _commands; }
            set { _commands = value;  OnPropertyChanged(); }
        }


        public BaseBattlefield() 
        {
            _battle = new BindingList<BindingList<BattlefieldCell>>();
            _commands = new BindingList<BattleCommand>();

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

        public void SetShip(BattleCommand cm, StateShip state, FieldDictionary _fieldDictionary, StateCell _baseStateCell)
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


            if (finX < 10 && finY < 10 && CheckSpace(shiftX, shiftY, x, y, _baseStateCell))
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
    }
}
