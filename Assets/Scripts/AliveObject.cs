using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;

class AliveObject : MonoBehaviour, IHandlingAliveObject
{
    [Header("Main settings")]
    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private LayerMask groundLayerMask = 2;
    [SerializeField] private float distanceToGround = 0.5f;

    [Header("Player parameters")]
    [SerializeField] private float _mouseSense = 2;
    [SerializeField] private float _health = 20;
    [SerializeField] private float _maxHealth = 20;
    [SerializeField] private float _jumpForce = 1.5f;
    [SerializeField] private float _lowHealthJumpForce = 1.5f;
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _runSpeed = 2.5f;
    [SerializeField] private float _lowHealthSpeed = 0.5f;

    // Внутренние переменные
    private Vector3 jumpMoveDirection = Vector3.zero;

    public float MouseSense { get => this._mouseSense; set => this._mouseSense = value; }
    public float Health
    {
        get => _health;
        set
        {
            if (this._health < value)
            {
                this.OnHealing(Math.Abs(this._health - value));
            }
            else
            {
                this.OnAcceptDamage(Math.Abs(this._health - value));
            }
        }
    }

    public float MaxHealth { get => this._maxHealth; set => this._maxHealth = value; }
    public float JumpForce { get => this._jumpForce; set => this._jumpForce = Mathf.Abs(value); }
    public float LowHealthJumpForce { get => _lowHealthJumpForce; set => this._lowHealthJumpForce = value; }
    public float CurrentMoveSpeed => GetMoveSpeedFromState(MovingState);
    public float WalkSpeed { get => this._walkSpeed; set => this._walkSpeed = value; }
    public float RunSpeed { get => this._runSpeed; set => this._runSpeed = value; }
    public float LowHealthSpeed { get => this._lowHealthSpeed; set => this._lowHealthSpeed = value; }
    public bool IsAlive => this._health > 0;
    private bool IsOnGround => Physics.CheckCapsule(this.transform.position, new Vector3(0,this.transform.position.y - this.transform.localScale.y - distanceToGround), Mathf.Max(this.transform.localScale.x,this.transform.localScale.y),this.groundLayerMask);
    public MovingState MovingState
    {
        get
        {
            if (this.Health < this.MaxHealth * 0.15)
                return MovingState.slowWalk;
            else
                return MovingState.walk;
        }
    }

    // События
    public event EventDelegates.onHealthChanged OnHealthChangedEvent;
    public event EventDelegates.onMoveSpeedChanged OnMoveSpeedChangedEvent;
    public event EventDelegates.onDeath OnDeathEvent;
    public event EventDelegates.onHealing OnHealingEvent;
    public event EventDelegates.onAcceptDamage OnAcceptDamageEvent;

    private void Start()
    {
        if (this.characterController == null)
            this.characterController = GetComponent<CharacterController>();
        if (this.playerCamera == null)
            this.playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        this.Move();
    }
            
    public void MoveTo(Vector3 position)
    {
        this.transform.Translate(position);
    }

    public void OnAcceptDamage(float damage)
    {
        float newHealthValue = Health - Math.Abs(damage); ;
        newHealthValue = Math.Max(0, newHealthValue);

        if (newHealthValue != Health)
        {
            this._health = newHealthValue;
            OnHealthChangedEvent?.Invoke(Health);
            OnAcceptDamageEvent?.Invoke(Math.Abs(damage));
            if (IsAlive == false)
                OnDeathEvent?.Invoke();
        }
    }

    public void OnHealing(float health)
    {
        float newHealthValue = Health + Math.Abs(health); 
        newHealthValue = Math.Min(newHealthValue,MaxHealth);

        if (newHealthValue != Health)
        {
            OnHealingEvent?.Invoke(newHealthValue);
            this._health = newHealthValue;

            OnHealthChangedEvent?.Invoke(Health);
        }
    }

    public float GetMoveSpeedFromState(MovingState currentState)
    {
        switch (currentState)
        {
            case MovingState.jump:
                return this.JumpForce;
            case MovingState.run:
                return this.RunSpeed;
            case MovingState.walk:
                return this.WalkSpeed;
            case MovingState.slowWalk:
                return this.LowHealthSpeed;
            case MovingState.slowRun:
                return this.LowHealthSpeed * 1.2f;
            default:
                return this.WalkSpeed * 0.8f;
        }
    }

    public void Jump()
    {
        var jump = Input.GetAxis("Jump");

        if (this.IsOnGround && jumpMoveDirection.y <= -1)
            if (jump == 1)
                jumpMoveDirection.y = JumpForce * jump;
            else 
                jumpMoveDirection.y = -1;
        else
        {

            // игрок летит вниз с увеличением скорости
            jumpMoveDirection.y += gravity * 0.01f;
            // до достижения скорости в половину от гравитации он ускоряется 
            if (jumpMoveDirection.y < gravity * 0.5f)
                jumpMoveDirection.y = gravity * 0.4f;
        }
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
            if (this.IsOnGround){
                var speed = GetMoveSpeedFromState(this.MovingState);
                print($"speed - {speed}");
                Vector3 moveDirection = new Vector3(hor * speed, 0.0f, ver * speed);
                this.characterController.Move(moveDirection);
            }
            this.Jump();
    }

    public void MoveCamera(Vector3 position)
    {
        this.playerCamera.transform.position = position;
    }
}