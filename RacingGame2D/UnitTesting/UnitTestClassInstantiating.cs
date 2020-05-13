using Microsoft.VisualStudio.TestTools.UnitTesting;
using Racing.Objects;

namespace UnitTesting
{
    [TestClass]
    public class UnitTestClassInstantiating
    {
        [TestMethod]
        public void TestInstantiatingCarProps()
        {
            var carProps = new CarProps(null);
            var result = false;

            if (carProps.MaxEngineForce == 430000f
                && carProps.MaxVelocity == 480f
                && carProps.MaxVelocityReverse == -250f
                && carProps.MaxSteeringAngle == 25f
                && carProps.MaxBreakingForce == 500000f
                && carProps.MaxFuelLevel == 35f
                && carProps.IdleFuelConsumption == 1f
                && carProps.DrivingFuelConsumption == 3f)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }
    }
}
