using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
using System.Runtime.CompilerServices;

class AliveObject : MonoBehaviour, IHandlingAliveObject
{
    private CharacterController characterController;
    private Rigidbody body;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float health = 20;
    [SerializeField] private float maxHealth = 20;
    [SerializeField] public float WalkSpeed { get; set; } = 1;
    [SerializeField] public float RunSpeed { get; set; } = 3;
    [SerializeField] public float LowHealthSpeed { get; set; } = 0.7f;
    [SerializeField] public float JumpForce { get; set; } = 5f;
    [SerializeField] public float MouseSense { get; set; } = 2;
    [SerializeField] public float MaxHealth { get => this.maxHealth; set => this.maxHealth = value; }
    [SerializeField] public bool IsAlive => this.health > 0;
    private Vector3 jumpMoveDirection = Vector3.zero;

    private void Start()
    {
        
        this.characterController = GetComponent<CharacterController>();
        this.body = GetComponent<Rigidbody>();
        if (this.playerCamera == null)
            this.playerCamera = Camera.main;

        this.MoveTo(Vector3.zero);
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    public float Health {
        get => health;
        set {
            if (this.health < value) {
                this.OnHealing(Math.Abs(this.health - value));
            }
            else
            {
                this.OnAcceptDamage(Math.Abs(this.health - value));
            }
        }
    }
    
    public MovingState MovingState {
        get
        {
            if (this.health < this.health * 0.15)
                return MovingState.slowWalk;
            else
                return MovingState.walk;
        }
    }
        
    public void MoveTo(Vector3 position)
    {
        this.transform.Translate(position);
    }

    public void OnAcceptDamage(float damage)
    {
        float damageValue = Math.Abs(damage);
        this.health -= damageValue;
        this.health = this.health < 0 ? 0 : this.health;
    }

    public void OnHealing(float health)
    {
        float healingValue = Math.Abs(health);
        this.health += healingValue;
        this.health = this.health > this.maxHealth ? this.maxHealth : this.health;
    }

    public float CurrentMoveSpeed(MovingState currentState)
    {
        switch (currentState)
        {
            case MovingState.run:
                return this.RunSpeed;
            case MovingState.slowWalk:
                return this.LowHealthSpeed;
            case MovingState.slowRun:
                return this.LowHealthSpeed * 1.2f;
            case MovingState.jump:
                return this.JumpForce;
            case MovingState.walk:
                return this.WalkSpeed;
            default:
                return this.WalkSpeed * 0.8f;
        }
    }

    public void Jump()
    {
        
        var jump = Input.GetAxis("Jump");
        print($"jump - {jump}");

        if (characterController.isGrounded)
        {
            if (jumpMoveDirection.y < 0)
                jumpMoveDirection.y = -1f;

            jumpMoveDirection.y = Mathf.Sqrt(JumpForce * -2f * gravity) * jump;
        }else
            jumpMoveDirection.y += gravity;
        print($"jumpDir - {jumpMoveDirection}");
        characterController.Move(jumpMoveDirection);

    }

    public void Seat()
    {
        throw new NotImplementedException();
    }

    public void SeeToMousePosition()
    {
        throw new NotImplementedException();
    }

    public void SeeTo(Vector3 position)
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");

            var speed = CurrentMoveSpeed(this.MovingState);
            print($"speed - {speed}");
            Vector3 moveDirection = new Vector3(hor * speed, 0.0f, ver * speed);
            
            this.characterController.Move(moveDirection);
            this.Jump();
        
        this.MoveCamera(new Vector3(this.transform.position.x - 15, this.transform.position.y + 2, this.transform.position.z + 15));
    }

    public void MoveCamera(Vector3 position)
    {
        this.playerCamera.transform.position = position;
    }
}
