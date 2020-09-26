using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    interface IMoving
    {
        public Transform transform { get; set; }
        public void seeTo(Vector3 position);
        public void moveTo(Vector3 position);
    }
}
