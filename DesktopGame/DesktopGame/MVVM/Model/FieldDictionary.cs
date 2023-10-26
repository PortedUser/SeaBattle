using DesktopGame.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.Model
{
    class FieldDictionary 
    {
        private List<List<Point>> _bowShip;
        private List<List<Point>> _doubleShip;
        private List<List<Point>> _threeShip;
        private List<List<Point>> _fourShip;

        public FieldDictionary() 
        { 
            _bowShip = new List<List<Point>>();
            _doubleShip = new List<List<Point>>();
            _threeShip = new List<List<Point>>();
            _fourShip = new List<List<Point>>();
        }

        public List<Point> GetAndDelPosShip(int x, int y) 
        {
            var res = SearchShip(_bowShip ,x, y);
            if (res != null) { _bowShip.Remove(res); return res; }

            res = SearchShip(_doubleShip ,x, y);
            if(res != null) { _doubleShip.Remove(res); return res; }

            res = SearchShip(_threeShip ,x, y);
            if ( res != null){ _threeShip.Remove(res); return res; }

            res = SearchShip(_fourShip ,x, y);
            if (res != null) { _fourShip.Remove(res); return res; }

            return res;
        }

        private List<Point> SearchShip(List<List<Point>> ships, int x, int y)
        {
            foreach (List<Point> ship in ships)
            {
                foreach(Point point in ship) 
                {
                    if (point.X == x && point.Y == y)
                    {
                        return ship;
                    }
                }
            }
            return null;
        }

        public void AddBowShip(int x, int y)
        {
            _bowShip.Add(new List<Point>());
            _bowShip[_bowShip.Count - 1].Add(new Point(x, y));
        }

        public void AddDoubleShip(int x, int y, int angle)
        {
            _doubleShip.Add(new List<Point>());
            var number = _bowShip.Count - 1;

            if (angle == 0)
            {
                _doubleShip[number].Add(new Point(x, y ));
                _doubleShip[number].Add(new Point(x, y + 1));
            }
            else if (angle == 90)
            {
                _doubleShip[number].Add(new Point(x, y));
                _doubleShip[number].Add(new Point(x + 1, y));
            }
        }

        public void AddThreeShip(int x, int y, int angle)
        {
            _threeShip.Add(new List<Point>());
            var number = _threeShip.Count - 1;

            if (angle == 0)
            {
                _threeShip[number].Add(new Point(x, y));
                _threeShip[number].Add(new Point(x, y + 1));
                _threeShip[number].Add(new Point(x, y + 2));
            }
            else if (angle == 90)
            {
                _threeShip[number].Add(new Point(x, y));
                _threeShip[number].Add(new Point(x + 1, y));
                _threeShip[number].Add(new Point(x + 2, y));
            }
        }

        public void AddFourShip(int x, int y, int angle)
        {
            _fourShip.Add(new List<Point>());
            var number = _fourShip.Count - 1;

            if (angle == 0)
            {
                _fourShip[number].Add(new Point(x, y));
                _fourShip[number].Add(new Point(x, y + 1));
                _fourShip[number].Add(new Point(x, y + 2));
                _fourShip[number].Add(new Point(x, y + 3));
            }
            else if (angle == 90)
            {
                _fourShip[number].Add(new Point(x, y));
                _fourShip[number].Add(new Point(x + 1, y));
                _fourShip[number].Add(new Point(x + 2, y));
                _fourShip[number].Add(new Point(x + 3, y ));
            }
        }
    }
}
