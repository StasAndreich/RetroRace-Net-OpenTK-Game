using Microsoft.VisualStudio.TestTools.UnitTesting;
using Racing.Objects;

namespace UnitTesting
{
    [TestClass]
    public class UnitTestDecorator
    {
        [TestMethod]
        public void Decorate_CarProps_BoostProps()
        {
            CarProps testProps = new CarProps();
            testProps.MaxVelocity = 400f;
            testProps.MaxEngineForce = 250f;
            bool result = false;

            testProps = new BoostProps(testProps, null);

            if (testProps.MaxVelocity == 600f
                && testProps.MaxEngineForce == 500f)
                result = true;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Decorate_CarProps_SlowdownProps()
        {
            CarProps testProps = new CarProps();
            testProps.MaxVelocity = 375f;
            bool result = false;

            testProps = new SlowdownProps(testProps, null);

            if (testProps.MaxVelocity == 250f)
                result = true;

            Assert.IsTrue(result);
        }
    }
}
