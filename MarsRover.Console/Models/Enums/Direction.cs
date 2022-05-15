using System.ComponentModel;

namespace MarsRover.Console.Models
{
    public enum Direction
    {
        [Description("North")]
        N,
        [Description("South")]
        S,
        [Description("West")]
        W,
        [Description("East")]
        E
    }
}
