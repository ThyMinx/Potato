using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potato;
internal class Score
{
    public string Name { get; set; }
    public int StartValue { get; set; }
    public int EndValue { get; set; }
    public int CurrentValue { get; set; }
    public string Ending { get; set; }

    public Score AddBy(int amount)
    {
        CurrentValue = CurrentValue + amount;
        if (CurrentValue > 10)
        {
            CurrentValue = 10;
        }
        return this;
    }

    public Score RemoveBy(int amount)
    {
        CurrentValue = CurrentValue - amount;
        if (CurrentValue < 0)
        {
            CurrentValue = 0;
        }
        return this;
    }

}
