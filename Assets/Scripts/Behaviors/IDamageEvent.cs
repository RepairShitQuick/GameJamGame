using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviors
{
    public interface IDamageEvent
    {
        void RunEvent(); // empty for now, maybe pass game state?
    }
}
