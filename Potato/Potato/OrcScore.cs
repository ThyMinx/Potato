using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potato;
internal class OrcScore : Score
{
	public OrcScore()
	{
		Name = "Orcs";
		StartValue = 0;
		EndValue = 10;
		CurrentValue = 0;
		Ending = "Orcs finally find your potato farm. Alas, orcs are not so interested in potatoes as they are in eating you," +
			"\r\n and you end up in a cookpot.";
    }
}
