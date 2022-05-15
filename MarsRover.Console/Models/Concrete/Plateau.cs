namespace MarsRover.Console.Models
{
    public class Plateau
    {
        public int XSize { get; }
        public int YSize { get; }
        public List<Rover> Rovers { get; }

        public Plateau(int xSize, int ySize)
        {
            XSize = xSize;
            YSize = ySize;
            Rovers = new List<Rover>();
        }

        public void AddRover(Rover rover)
        {
            Rovers.Add(rover);
        }
    }
}
