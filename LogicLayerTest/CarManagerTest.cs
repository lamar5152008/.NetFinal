using DataObject;
using DataAccessFake;
using LogicLayer;
using LogicLayerInterface;
using DataAccessInterface;
namespace LogicLayerTest
{
    public class CarManagerTest
    {
        private CarManagerInterface _carManager;
        private CarAccessorInterface _carAccessor;
        private Car _car;
        [SetUp]
        public void Setup()
        {
            _carAccessor = new FakeCarAccessor();
            _carManager = new CarManager(_carAccessor);
            _car = new Car();
        }

        [Test]
        public void TestInsertCarWithRealData()
        {
            //1- initial
            _car.Name = "foo";
            _car.Model = "bar";
            _car.Year = "1900";
            _car.Color = "red";
            _car.Active = true;
            int result = 1;
            int actual = _carManager.add(_car);
            Assert.That(actual, Is.EqualTo(result));
        }
        [Test]
        public void TestEditCarWithRealData()
        {
            //1- initial
            _car.CarID = 1;
            _car.Name = "car1";
            _car.Model = "model1";
            _car.Color = "color1";
            _car.Year = "1900";
            _car.Active = true;
            int result = 1;
            int actual = _carManager.edit(_car);
            Assert.That(actual, Is.EqualTo(result));
        }

        [Test]
        public void TestGetAllCars()
        {
            //1- initial
           List<Car> expected = new List<Car>();
            Car car = new Car();
            car.CarID = 1;
            car.Name = "car1";
            car.Model = "model1";
            car.Color = "color1";
            car.Year = "year1";
            car.Active = true;
            expected.Add(car);

            car = new Car();
            car.CarID = 2;
            car.Name = "car2";
            car.Model = "model2";
            car.Color = "color2";
            car.Year = "year2";
            car.Active = true;
            expected.Add(car);

            car = new Car();
            car.CarID = 3;
            car.Name = "car3";
            car.Model = "model3";
            car.Color = "color3";
            car.Year = "year3";
            car.Active = true;
            expected.Add(car);
            int result = expected.Count;
            int actual = _carManager.getAllCars().Count;
            Assert.That(actual, Is.EqualTo(result));
        }
    }
}