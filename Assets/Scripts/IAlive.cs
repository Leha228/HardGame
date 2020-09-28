using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    interface IAlive
    {
         float Health { get; set; }
         bool IsAlive { get; }
         float MaxHealth { get; set; }
         void OnAcceptDamage(float damage);
         void OnHealing(float health);
    }
}
