using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potato;
internal class PotatoScore : Score
{
    public PotatoScore()
    {
        Name = "Potatoes";
        StartValue = 0;
        EndValue = 10;
        CurrentValue = 0;
        Ending = "You finally have enough potatoes to go underground and not return to the surface until the danger is past. " +
            "\r\nYou nestle down into your burrow and enjoy your well earned rest.";
    }
}
