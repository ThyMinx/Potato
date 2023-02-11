using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potato;
internal class RemoveOrcAction
{
    public string Description { get; set; }
    public int CostOfAction { get; set; }
    public RemoveOrcAction()
    {
        CostOfAction = 1;
        Description = $"You can remove one orc at the cost of: {CostOfAction}";
    }
    public RemoveOrcAction IncreaseCostBy(int amount)
    {
        CostOfAction = CostOfAction + amount;
        Description = $"You can now remove one orc at the cost of: {amount}";
        return this;
    }
}
