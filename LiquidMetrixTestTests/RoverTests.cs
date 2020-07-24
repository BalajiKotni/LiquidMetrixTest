using LiquidMetrixTest.Helpers;
using LiquidMetrixTest.Services;
using Moq;
using NUnit.Framework;

namespace LiquidMetrixTestTests
{
    [TestFixture]
    public class RoverTests
    {
        private Mock<ILogger> _loggerMock;
        private Rover _roverUnderTest;
        
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger>();

            _roverUnderTest = new Rover(_loggerMock.Object);
        }

        [Test]
        public void WhenCommandNotValidLength_ShouldRejectMove()
        {
            _roverUnderTest.SetPosition(0, 10, Direction.N);

            _roverUnderTest.Move("XXXXX");

            _loggerMock.Verify(logger => logger.Log($"Invalid command XXXXX"));
        }


        [Test]
        public void WhenCommandDirectionNotValid_ShouldRejectMove()
        {
            _roverUnderTest.SetPosition(0, 10, Direction.N);

            _roverUnderTest.Move("J1");

            _loggerMock.Verify(logger => logger.Log($"Invalid command J1"));
        }

        [Test]
        public void WhenCommandDidNotContainValidNumericLength_ShouldRejectMove()
        {
            _roverUnderTest.SetPosition(0, 10, Direction.N);

            _roverUnderTest.Move("RO");

            _loggerMock.Verify(logger => logger.Log($"Invalid command RO"));
        }

        [Test]
        public void WhenCommandPointsToEast_ExceedingMaxLength_ShouldCauseFallOffPlateau()
        {
            _roverUnderTest.SetPosition(10, 10, Direction.N);

            _roverUnderTest.Move("R40");

            _loggerMock.Verify(logger => logger.Log($"Invalid command R40 causing fall off plateau"));
        }

        [Test]
        public void WhenValidCommandPointsToEast_MoveShouldSucceed()
        {
            _roverUnderTest.SetPosition(10, 10, Direction.N);

            _roverUnderTest.Move("R15");

            Assert.AreEqual(_roverUnderTest.CurrentX, 25);
            Assert.AreEqual(_roverUnderTest.CurrentY, 10);
            Assert.AreEqual(_roverUnderTest.CurrentDirection, Direction.E);
        }

        [Test]
        public void WhenCommandPointsToNorth_ExceedingMaxLength_ShouldCauseFallOffPlateau()
        {
            _roverUnderTest.SetPosition(5, 5, Direction.E);

            _roverUnderTest.Move("L50");

            _loggerMock.Verify(logger => logger.Log($"Invalid command L50 causing fall off plateau"));
        }

        [Test]
        public void WhenValidCommandPointsToNorth_MoveShouldSucceed()
        {
            _roverUnderTest.SetPosition(10, 10, Direction.E);

            _roverUnderTest.Move("L05");

            Assert.AreEqual(_roverUnderTest.CurrentX, 10);
            Assert.AreEqual(_roverUnderTest.CurrentY, 15);
            Assert.AreEqual(_roverUnderTest.CurrentDirection, Direction.N);
        }

        [Test]
        public void WhenCommandPointsToSouth_YGoingNegative_ShouldCauseFallOffPlateau()
        {
            _roverUnderTest.SetPosition(5, 5, Direction.E);

            _roverUnderTest.Move("R15");

            _loggerMock.Verify(logger => logger.Log($"Invalid command R15 causing fall off plateau"));
        }

        [Test]
        public void WhenValidCommandPointsToSouth_MoveShouldSucceed()
        {
            _roverUnderTest.SetPosition(12, 22, Direction.E);

            _roverUnderTest.Move("R05");

            Assert.AreEqual(_roverUnderTest.CurrentX, 12);
            Assert.AreEqual(_roverUnderTest.CurrentY, 17);
            Assert.AreEqual(_roverUnderTest.CurrentDirection, Direction.S);
        }

        [Test]
        public void WhenCommandPointsToWest_XGoingNegative_ShouldCauseFallOffPlateau()
        {
            _roverUnderTest.SetPosition(10, 5, Direction.N);

            _roverUnderTest.Move("L15");

            _loggerMock.Verify(logger => logger.Log($"Invalid command L15 causing fall off plateau"));
        }

        [Test]
        public void WhenValidCommandPointsToWest_MoveShouldSucceed()
        {
            _roverUnderTest.SetPosition(10, 6, Direction.N);

            _roverUnderTest.Move("L8");

            Assert.AreEqual(_roverUnderTest.CurrentX, 2);
            Assert.AreEqual(_roverUnderTest.CurrentY, 6);
            Assert.AreEqual(_roverUnderTest.CurrentDirection, Direction.W);
        }
    }
}