using MarsRover.Console.Models;

Console.WriteLine("Enter plateau size: ");
var plateauSizes = Console.ReadLine().Trim().Split(' ');
int plateauXSize = 0;
int plateauYSize = 0;

if (!HasValidPlateauSize(ref plateauXSize, ref plateauYSize, plateauSizes))
{
    throw new Exception("Invalid plateau size inputs.");
}

var plateau = new Plateau(plateauXSize, plateauYSize);
var isContinue = true;

while (isContinue)
{
    Console.Write("Enter rover initial positions: ");
    var roverInitialPositions = Console.ReadLine().Trim().ToUpper().Split(' ');

    int roverXPosition = 0;
    int roverYPosition = 0;
    var direction = Direction.N;
    if (!HasRoverValidInitialPosition(roverInitialPositions, ref roverXPosition, ref roverYPosition, ref direction))
    {
        throw new Exception("Invalid rover initial position inputs.");
    }
    plateau.AddRover(new Rover(roverXPosition, roverYPosition, direction, plateau));

    Console.Write("Enter rover actions: ");
    var roverActions = Console.ReadLine().Trim().ToUpper();
    var rover = plateau.Rovers.Last();
    var message = rover.StartAction(roverActions);

    if (!string.IsNullOrEmpty(message))
    {
        throw new Exception(message);
    }

    Console.Write("To add more rover please enter..");
    var readKey = Console.ReadKey();
    if (readKey.Key != ConsoleKey.Enter)
    {
        isContinue = false;
    }

    Console.WriteLine();
}

if (plateau == null)
{
    throw new ArgumentNullException();
}

Console.WriteLine("Output is:");
foreach (var item in plateau.Rovers)
{
    Console.WriteLine(item.ToString());
}

Console.ReadLine();

#region private methods

bool HasValidPlateauSize(ref int plateauXSize, ref int plateauYSize, string[] plateauSizes)
{
    return plateauSizes.Length == 2 && Int32.TryParse(plateauSizes[0], out plateauXSize) && Int32.TryParse(plateauSizes[1], out plateauYSize);
}

bool HasRoverValidInitialPosition(string[] roverInitialPositions, ref int roverXPosition, ref int roverYPosition, ref Direction direction)
{
    return roverInitialPositions.Length == 3 && Int32.TryParse(roverInitialPositions[0], out roverXPosition) && Int32.TryParse(roverInitialPositions[1], out roverYPosition) && Enum.TryParse<Direction>(roverInitialPositions[2], true, out direction);
}

#endregion
