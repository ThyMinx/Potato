using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potato;
internal class DestinyScore : Score
{
	public DestinyScore()
	{
		Name = "Destiny";
		StartValue = 0;
		EndValue = 10;
		CurrentValue = 0;
        Ending = "An interfering bard or wizard turns up at your doorstep with a quest, and you are whisked away against your" +
			"\r\nwill on an adventure.";
    }
}
