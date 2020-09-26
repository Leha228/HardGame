using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
class AliveObject : IMoving, IAlive
{
    private readonly IAlive aliveManager;
    private readonly IMoving movingManager;
    private Transform transform;
    private float health;
    private float maxHealth;

    public float walkSpeed { get; set; }
    public float runSpeed { get; set; }
    public float lowHealthSpeed { get; set; }

    public AliveObject()
    {
        aliveManager = this;
        movingManager = this;
    }

    Transform IMoving.transform
    {
        get => this.transform;
        set => this.transform = value;
    }

    float IAlive.Health {
        get => health;
        set {
            if (this.health < value) {
                aliveManager.onHealing(Math.Abs(this.health - value));
            }
            else
            {
                aliveManager.onAcceptDamage(Math.Abs(this.health - value));
            }
        }
    }
    float IAlive.MaxHealth { get => this.maxHealth; set => this.maxHealth = value; }

    bool IAlive.isAlive => this.health > 0;

    void IMoving.moveTo(Vector3 position)
    {
        
    }

    void IMoving.seeTo(Vector3 position)
    {
       
    }

    void IAlive.onAcceptDamage(float damage)
    {
        float damageValue = Math.Abs(damage);
        this.health -= damage;
        this.health = this.health < 0 ? 0 : this.health;
    }

    void IAlive.onHealing(float health)
    {
        float healingValue = Math.Abs(health);
        this.health += healingValue;
        this.health = this.health > this.maxHealth ? this.maxHealth : this.health;
    }
}
