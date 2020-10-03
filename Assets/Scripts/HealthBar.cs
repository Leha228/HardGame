using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AliveObject Player = null;
    [SerializeField] private Text TextField = null;
    private Timer timer = new Timer(2000);
    void Start()
    {
        Player.OnHealthChangedEvent += Player_OnHealthChangedEvent;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        print("timerTick");
        Player.OnAcceptDamage(1);
    }

    private void Player_OnHealthChangedEvent(float newValue)
    {
        print("Health Changed");
        this.TextField.text = $"Health: {newValue}";
        this.TextField.LayoutComplete();
    }
}
