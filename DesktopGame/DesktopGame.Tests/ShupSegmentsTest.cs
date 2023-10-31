using DesktopGame.Domain.Enum;
using DesktopGame.MVVM.Model;
namespace DesktopGame.Tests
{
    [TestClass]
    public class ShupSegmentsTest
    {
        [TestMethod]
        public void GetBowShipAngle90()
        {
            //arrange
            var type = TypeShip.BowShip;
            var numbSegment = 0;
            var angle = AngleOfRotation.Angle_90;

            var expected = StateCell.BowShip_90;

            //act
            var managerSegment = new ShipSegments();
            var actual = managerSegment[type, angle, numbSegment];

            //assert
            Assert.AreEqual(expected, actual);
        }

        public void GetShipAndAngle(TypeShip type, AngleOfRotation angle, int numbSegment, StateCell expected)
        {
            //arrange
            

            //act
            var managerSegment = new ShipSegments();
            var actual = managerSegment[type, angle, numbSegment];

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FullTest()
        {
            GetShipAndAngle(TypeShip.BowShip, AngleOfRotation.Angle_0, 0, StateCell.BowShip);
            GetShipAndAngle(TypeShip.BowShip, AngleOfRotation.Angle_90, 0, StateCell.BowShip_90);

            GetShipAndAngle(TypeShip.DoubleDeckShip, AngleOfRotation.Angle_0, 0, StateCell.Deck2_1);
            GetShipAndAngle(TypeShip.DoubleDeckShip, AngleOfRotation.Angle_90, 0, StateCell.Deck2_1_90);

            GetShipAndAngle(TypeShip.DoubleDeckShip, AngleOfRotation.Angle_0, 1, StateCell.Deck2_2);
            GetShipAndAngle(TypeShip.DoubleDeckShip, AngleOfRotation.Angle_90, 1, StateCell.Deck2_2_90);

            GetShipAndAngle(TypeShip.ThreeDeckShip, AngleOfRotation.Angle_0, 0, StateCell.Deck3_1);
            GetShipAndAngle(TypeShip.ThreeDeckShip, AngleOfRotation.Angle_90, 0, StateCell.Deck3_1_90);

            GetShipAndAngle(TypeShip.ThreeDeckShip, AngleOfRotation.Angle_0, 1, StateCell.Deck3_2);
            GetShipAndAngle(TypeShip.ThreeDeckShip, AngleOfRotation.Angle_90, 1, StateCell.Deck3_2_90);

            GetShipAndAngle(TypeShip.ThreeDeckShip, AngleOfRotation.Angle_0, 2, StateCell.Deck3_3);
            GetShipAndAngle(TypeShip.ThreeDeckShip, AngleOfRotation.Angle_90, 2, StateCell.Deck3_3_90);

            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_0, 0, StateCell.Deck4_1);
            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_90, 0, StateCell.Deck4_1_90);

            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_0, 1, StateCell.Deck4_2);
            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_90, 1, StateCell.Deck4_2_90);

            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_0, 2, StateCell.Deck4_3);
            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_90, 2, StateCell.Deck4_3_90);

            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_0, 3, StateCell.Deck4_4);
            GetShipAndAngle(TypeShip.FourDeckShip, AngleOfRotation.Angle_90, 3, StateCell.Deck4_4_90);
        }
        [TestMethod]
        public void InvalidRequest()
        {
            //arrange
            var type = TypeShip.BowShip;
            var angle = AngleOfRotation.Angle_0;
            var numbSegment = 10;

            var expected = StateCell.None;
            //act
            var managerSegment = new ShipSegments();
            var actual = managerSegment[type, angle, numbSegment];

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}