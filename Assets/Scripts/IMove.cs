using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMove
{
   enum Directions : UInt16
    {
        Up,
        Left,
        Down,
        Right
    }
    public void MoveTo(Transform position);
}
