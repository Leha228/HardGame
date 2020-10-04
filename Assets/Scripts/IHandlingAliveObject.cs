using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;
using UnityEngine.EventSystems;

interface IHandlingAliveObject : IAlive
{
    float JumpForce { get; set; }
    float LowHealthJumpForce { get; set; }
    float MouseSense { get; set; }
    float CurrentMoveSpeed { get; }
    float WalkSpeed { get; set; }
    float RunSpeed { get; set; }
    float LowHealthSpeed { get; set; }
    void Jump();
    void Seat();
    void Move();
    void SeeToMousePosition();
    void MoveCamera(Vector3 position);
    float GetMoveSpeedFromState(MovingState currentState);
    MovingState MovingState { get; }

    event EventDelegates.onHealthChanged OnHealthChangedEvent;
    event EventDelegates.onMoveSpeedChanged OnMoveSpeedChangedEvent;
    event EventDelegates.onDeath OnDeathEvent;
    event EventDelegates.onHealing OnHealingEvent;
    event EventDelegates.onAcceptDamage OnAcceptDamageEvent;
}