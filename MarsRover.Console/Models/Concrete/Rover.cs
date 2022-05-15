using MarsRover.Console.Models.Abstract;

namespace MarsRover.Console.Models
{
    public class Rover : IRoverAction
    {
        public int Id { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Direction { get; set; }
        public Plateau Plateau { get; }

        public Rover(int xPosition, int yPosition, Direction direction, Plateau plateau)
        {
            Id = plateau.Rovers.Count + 1;
            XPosition = xPosition;
            YPosition = yPosition;
            Direction = direction;
            Plateau = plateau;

            if (xPosition > plateau.XSize || xPosition < 0 || yPosition > plateau.YSize || yPosition < 0)
            {
                throw new Exception("Out of range initial positions!");
            }
        }

        private void Move()
        {
            switch (Direction)
            {
                case Direction.N:
                    YPosition += 1;
                    break;
                case Direction.E:
                    XPosition += 1;
                    break;
                case Direction.S:
                    YPosition -= 1;
                    break;
                case Direction.W:
                    XPosition -= 1;
                    break;
            }
        }

        private void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.E;
                    break;
                case Direction.E:
                    Direction = Direction.N;
                    break;
            }
        }

        private void TurnRight()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.E;
                    break;
                case Direction.E:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.N;
                    break;
            }
        }

        public string StartAction(string actions)
        {
            var roverActions = actions.ToCharArray();

            foreach (var action in roverActions)
            {
                switch (action)
                {
                    case 'L':
                        this.TurnLeft();
                        break;
                    case 'R':
                        this.TurnRight();
                        break;
                    case 'M':
                        this.Move();
                        break;
                    default:
                        return "Invalid action input.";
                }
            }

            if (XPosition < 0 || XPosition > this.Plateau.XSize || YPosition < 0 || YPosition > this.Plateau.YSize)
            {
                return $"Rover {Id} is out of range: ({XPosition}, {YPosition}) !";
            }

            return string.Empty;
        }

        public override string ToString()
        {
            return $"{XPosition} {YPosition} {Direction}";
        }
    }
}
