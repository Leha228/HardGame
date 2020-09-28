using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;
using UnityEngine.EventSystems;

interface IHandlingAliveObject: IAlive
{
     float JumpForce { get; set; }
     float MouseSense { get; set; }
     float WalkSpeed { get; set; }
     float RunSpeed { get; set; }
     float LowHealthSpeed { get; set; }
     void Jump();
     void Seat();
     void Move();
     void SeeToMousePosition();
     void MoveCamera(Vector3 position);
     float CurrentMoveSpeed(MovingState currentState);
     MovingState MovingState { get; }
}