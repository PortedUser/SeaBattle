using DesktopGame.Domain.Enum;
using DesktopGame.MVVM.Model.BattlefieldModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DesktopGame.MVVM.Model
{
    class ShipSegments
    {
        private Dictionary<TypeShip, Dictionary<int, Dictionary<AngleOfRotation, StateCell>>> _segments; 
        public ShipSegments() 
        {
            _segments = new Dictionary<TypeShip, Dictionary<int, Dictionary<AngleOfRotation, StateCell>>>();

            GenerateTypeShipKeys();
            GenerateSegmentNumberKeys();  
        }
        
        public StateCell this[TypeShip type, AngleOfRotation angle, int numberSegment]
        {
            get 
            { 
                return _segments[type][numberSegment][angle];
            }
        }

        private void GenerateTypeShipKeys()
        {
            foreach (TypeShip type in Enum.GetValues(typeof(TypeShip)))
            {
                if (type.ToString().Contains("Ship"))
                {
                    _segments.Add(type, new Dictionary<int, Dictionary<AngleOfRotation, StateCell>>());
                }
            }
        }

        private void GenerateSegmentNumberKeys()
        {
            var counterSegment = 0;
            foreach (var typeKey in _segments)
            {
                counterSegment++;
                for (int i = 0; i < counterSegment; i++)
                {
                    typeKey.Value.Add(i, new Dictionary<AngleOfRotation, StateCell>());

                    GenerateRotationAngleKeys(typeKey.Key, i);
                }
            }
        }

        private void GenerateRotationAngleKeys(TypeShip type, int numberSegment)
        {
            var state = (int)type + numberSegment;
            StateCell cell = (StateCell)state;
            _segments[type][numberSegment].Add(AngleOfRotation.Angle_0, cell);

            cell = (StateCell)state + 90;
            _segments[type][numberSegment].Add(AngleOfRotation.Angle_90, cell);
        }
    }
}
