using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventDelegates
{
    // IHandlingAliveObject
    public delegate void onHealthChanged(float newValue);
    public delegate void onMoveSpeedChanged(float newValue);
    public delegate void onDeath();
    public delegate void onHealing(float newHealthValue);
    public delegate void onAcceptDamage(float damage);
}
