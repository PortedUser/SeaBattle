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
        private List<List<Point>>[] _ships;

        public bool IsFull { get { return BowShipIsFull && DoubleShipIsFull && ThreeShipIsFull && FourShipIsFull; } }
        public bool BowShipIsFull { get { return _bowShip.Count == 4; } }
        public bool DoubleShipIsFull { get { return _doubleShip.Count == 3; } }
        public bool ThreeShipIsFull { get { return _threeShip.Count == 2; } }
        public bool FourShipIsFull { get { return _fourShip.Count == 1; } }

        public FieldDictionary() 
        { 
            _bowShip = new List<List<Point>>();
            _doubleShip = new List<List<Point>>();
            _threeShip = new List<List<Point>>();
            _fourShip = new List<List<Point>>();

            _ships = new List<List<Point>>[4];
            _ships[0] = _bowShip;
            _ships[1] = _doubleShip;
            _ships[2] = _threeShip;
            _ships[3] = _fourShip;
        }

        public void DeleteAll()
        {
            _bowShip.Clear();
            _doubleShip.Clear();
            _fourShip.Clear();
            _threeShip.Clear();
        }

        public List<Point> GetAndDelPosShip(int x, int y) 
        {
            foreach (var item in _ships)
            {
                var res = SearchShip(item, x, y);
                if (res != null)
                {
                    item.Remove(res);
                    return res;
                }
            }
            return null;
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

        public void AddShip(int x, int y, StateShip state)
        {

            var shiftModulusX = state.AngleRotation == AngleOfRotation.Angle_90 ? 1 : 0;
            var shiftModulusY = state.AngleRotation == AngleOfRotation.Angle_0 ? 1 : 0;

            var lengthShip = (int)state.CurrentType / 10;
            var currList = _ships[lengthShip - 1];

            var shiftX = GetShift(lengthShip, shiftModulusX);
            var shiftY = GetShift(lengthShip, shiftModulusY);

            var finX = x + shiftX;
            var finY = y + shiftY;

            currList.Add(new List<Point>());
            var numbItem = currList.Count - 1;

            for (int i = 0; i < lengthShip; i++)
            {
                var currX = x + i * shiftModulusX;
                var currY = y + i * shiftModulusY;

                currList[numbItem].Add(new Point(currX, currY));
            }
        }

        private int GetShift(int length, int modulus)
        {
            return (length - 1) * modulus;
        }

        public void AddBowShip(int x, int y)
        {
            _bowShip.Add(new List<Point>());
            _bowShip[_bowShip.Count - 1].Add(new Point(x, y));
        }

        public void AddDoubleShip(int x, int y, int angle)
        {
            _doubleShip.Add(new List<Point>());
            var number = _doubleShip.Count - 1;

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
