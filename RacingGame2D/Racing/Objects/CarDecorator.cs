namespace Racing.Objects
{
    public abstract class CarDecorator : Car
    {
        protected Car basicCar;

        public CarDecorator(Car basicCar)
        {
            this.basicCar = basicCar;
        }
    }
}
