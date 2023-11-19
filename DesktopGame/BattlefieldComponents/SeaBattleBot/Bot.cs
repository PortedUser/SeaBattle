using BattlefieldComponents.Models;
using DesktopGame.Domain.Enum;
using DesktopGame.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = DesktopGame.MVVM.Model.Point;

namespace BattlefieldComponents.SeaBattleBot
{
    public class Bot
    {
        private List<List<MarkedPoint>> _enemyField;
        private ShotResult _lastShotResult;
        private Point _lastHit;
        private List<Point> _enemyShip;
        private List<Point> _listSteps;
        public Bot() 
        {
            _listSteps = new List<Point>
            {
                new Point(0, 1) ,
                new Point(0, -1) ,
                new Point(-1, 0) ,
                new Point(1, 0)
            };
            _lastShotResult = new ShotResult();
            _enemyField = new List<List<MarkedPoint>>();
            _lastHit = new Point(-1,-1);
            _enemyShip = new List<Point>();

            for (int x = 0; x < 10; x++)
            {
                _enemyField.Add(new List<MarkedPoint>());   
                for (int y = 0; y < 10; y++)
                {
                    _enemyField[x].Add(new MarkedPoint(x, y));
                }
            }
        }

        public Point Shot()
        {
            if (_enemyShip.Count > 0)
            {
                var res = GetPointArea(_enemyShip[_enemyShip.Count - 1]);
                _lastHit = res;
                _enemyField[res.X][res.Y].WasAttacked();
                return res;
            }
            var rnd = new Random();
            return RandomShot(rnd);
        }

        private void EncloseShip(List<Point> ship)
        {
            for (int i = 0; i < ship.Count; i++)
            {
                for (int xi = -1; xi <= 1; xi++)
                {
                    for (int yi = -1; yi <= 1; yi++)
                    {
                        var currX = ship[i].X + xi;
                        var currY = ship[i].Y + yi;
                        if (WithinTheBorder(new Point(currX, currY)))
                        {
                            _enemyField[currX][currY].WasAttacked();
                        }

                    }
                }
            }
        }

        private Point GetPointArea(Point point, int currStep = 0)
        {
            

            for (int i = 0 + currStep; i < _listSteps.Count; i++)
            {
                var finX = point.X + _listSteps[i].X;
                var finY = point.Y + _listSteps[i].Y;

                if (!WithinTheBorder(new Point(finX, finY)))
                {
                    continue;
                }

                if (WithinTheBorder(new Point(finX,finY)) && !_enemyField[finX][finY].IsWasAttacked)
                {
                    return new Point(finX, finY);
                }
                else if (_enemyField[finX][finY].IsWasAttacked || !WithinTheBorder(new Point(finX, finY)))
                {
                    if (_listSteps.Count - 1 == i)
                    {
                        var alternate = GetAlternatePoint(point);
                        return GetPointArea(alternate);
                    }
                    return GetPointArea(point,++i);
                }
            }
            MessageBox.Show($"Бот стреляет в некорректный сегмент {this} --- {point} X = {point.X} Y = {point.Y}");
            throw new ArgumentException($"Бот стреляет в некорректный сегмент {this} --- {point} X = {point.X} Y = {point.Y}");
        }
        
        private Point GetAlternatePoint(Point currPoint)
        {
            var maxY = _enemyShip.MaxBy(o => o.Y);
            var minY = _enemyShip.MinBy(o => o.Y);

            var maxX = _enemyShip.MaxBy(o => o.X);
            var minX = _enemyShip.MinBy(o => o.X);

            if (!(maxY.Y == minY.Y))
            {
                if (currPoint.Y == maxY.Y)
                {
                    return minY;
                }
                else
                {
                    return maxY;
                }
            }

            if (!(maxX.X == minX.X))
            {
                if (currPoint.X == maxX.X)
                {
                    return minX;
                }
                else
                {
                    return maxX;
                }
            }
            MessageBox.Show($"Бот стреляет в некорректный сегмент {this} --- {currPoint} X = {currPoint.X} Y = {currPoint.Y}");
            throw new ArgumentException($"Бот стреляет в некорректный сегмент {this} --- {currPoint} X = {currPoint.X} Y = {currPoint.Y}");
        }

        private bool WithinTheBorder(Point point)
        {
            return WithinTheBorder(point, 0, 0);
        }
        private bool WithinTheBorder(Point point, int stepX, int stepY)
        {
            var finX = point.X + stepX;
            var finY = point.Y + stepY;
            return finX < 10 && finX > -1 && finY > -1 && finY < 10;
        }

        private Point RandomShot(Random rnd)
        {
            var shot = GetRandomPoint(rnd);
            if (_enemyField[shot.X][shot.Y].IsWasAttacked)
            {
                shot = RandomShot(rnd);
            }
            _enemyField[shot.X][shot.Y].WasAttacked();
            _lastHit = shot;
            return shot;
        }

        private Point GetRandomPoint(Random rnd)
        {
            return new Point(rnd.Next(0, 10), rnd.Next(0, 10));
        }

        public void SetResultShot(ShotResult result)
        {
            _lastShotResult = result;
            if (result.HitSuccessfully && !result.ShipSunk)
            {
                _enemyShip.Add(_lastHit);
            }
            else if (result.ShipSunk)
            {
                _enemyShip.Add(_lastHit);
                EncloseShip(_enemyShip);
                _enemyShip.Clear();
            }
        }
    }
}
