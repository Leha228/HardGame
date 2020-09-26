using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    interface IAlive
    {
        public float Health { get; set; }
        public bool isAlive { get; }
        public float MaxHealth { get; set; }
        public void onAcceptDamage(float damage);
        public void onHealing(float health);
    }
}
