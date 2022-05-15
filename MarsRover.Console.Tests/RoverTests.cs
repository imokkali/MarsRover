using MarsRover.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Console.Tests
{
    public class RoverTests : IDisposable
    {
        private readonly Plateau plateau;

        public RoverTests()
        {
            plateau = new Plateau(5, 5);

        }

        [Theory]
        [InlineData("L", Direction.N, Direction.W)]
        [InlineData("L", Direction.W, Direction.S)]
        [InlineData("L", Direction.S, Direction.E)]
        [InlineData("L", Direction.E, Direction.N)]
        [InlineData("R", Direction.N, Direction.E)]
        [InlineData("R", Direction.E, Direction.S)]
        [InlineData("R", Direction.S, Direction.W)]
        [InlineData("R", Direction.W, Direction.N)]
        public void StartAction_IfExistDirection_DirectionShouldExpected(string directionAction, Direction existDirection, Direction expectedDirection)
        {
            //Arrange
            var xPosition = 1;
            var yPosition = 2;
            var rover = new Rover(xPosition, yPosition, existDirection, plateau);

            //Act
            var message = rover.StartAction(directionAction);

            //Assert
            Assert.Equal(xPosition, rover.XPosition);
            Assert.Equal(yPosition, rover.YPosition);
            Assert.Equal(expectedDirection, rover.Direction);
            Assert.Empty(message);
        }

        [Theory]
        [InlineData(Direction.N, 2, 2, 2, 3)]
        [InlineData(Direction.E, 2, 2, 3, 2)]
        [InlineData(Direction.S, 2, 2, 2, 1)]
        [InlineData(Direction.W, 2, 2, 1, 2)]
        public void StartAction_IfExistDirection_PositionsShouldExpected(Direction existDirection, int existXPosition, int existYPosition, int expectedXPosition, int expectedYPosition)
        {
            //Arrange
            var rover = new Rover(existXPosition, existYPosition, existDirection, plateau);
            var moveAction = "M";

            //Act
            var message = rover.StartAction(moveAction);

            //Assert
            Assert.Equal(expectedXPosition, rover.XPosition);
            Assert.Equal(expectedYPosition, rover.YPosition);
            Assert.Equal(existDirection, rover.Direction);
            Assert.Empty(message);
        }

        [Fact]
        public void StartAction_IfActionNotValid_ReturnMessage()
        {
            //Arrange
            var rover = new Rover(1, 3, Direction.N, plateau);
            var moveAction = "A";

            //Act
            var message = rover.StartAction(moveAction);

            //Assert
            Assert.Equal("Invalid action input.", message);
        }

        [Theory]
        //NOTE: Plateau size 5,5 initally.
        [InlineData(Direction.N, 1, 1, "MMMMM")]
        [InlineData(Direction.E, 1, 1, "MMMMM")]
        [InlineData(Direction.S, 1, 1, "MM")]
        [InlineData(Direction.W, 1, 1, "MM")]
        public void StartAction_RoverMovesOutOfRange_ReturnMessage(Direction direction, int xPosition, int yPosition, string moveAction)
        {
            //Arrange
            var rover = new Rover(xPosition, yPosition, direction, plateau);

            //Act
            var message = rover.StartAction(moveAction);

            //Assert
            Assert.Equal($"Rover {rover.Id} is out of range: ({rover.XPosition}, {rover.YPosition}) !", message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
