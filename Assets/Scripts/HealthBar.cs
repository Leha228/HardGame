using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AliveObject Player = null;
    [SerializeField] private Text TextField = null;
    private Timer timer = new Timer(2000);
    private float step = 0;
    void Start()
    {
        Player.OnHealthChangedEvent += Player_OnHealthChangedEvent;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }
    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Player.OnAcceptDamage(1);
    }
    private void Update()
    {
        if (Player.Health <= 0)
        {
            this.TextField.color = Color.black;
            var a = Player.GetComponent<MeshRenderer>();
            a.material.color = Color.black;
        }
        else
            this.TextField.color = Color.Lerp(Color.green, Color.red, step);
        this.TextField.text = $"Health: {Player.Health}";
    }
    private void Player_OnHealthChangedEvent(float newValue)
    {
        print($"Health Changed new Value is {newValue}");
        this.step += 1/this.Player.MaxHealth;
    }

}
