using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] UIManager _UIManager;
    public bool IsDead { get; private set; }
    [SerializeField] float CurHealth;
    [SerializeField] float MaxHealth = 100;
    public UnityEvent OnDeath = new UnityEvent();
    public UnityEvent OnRevive = new UnityEvent();
    public UnityEvent<float> OnDamage = new UnityEvent<float>();

    private void Awake()
    {
        CurHealth = MaxHealth;
        if(_UIManager != null)
        {
            OnDeath.AddListener(_UIManager.DeadPanelEnable);
            OnRevive.AddListener(_UIManager.DeadPanelDisable);
        }
    }
    public void GetDamage(float dmg)
    {
        OnDamage.Invoke(CurHealth/MaxHealth);
        CurHealth -= dmg;
        if(CurHealth <= 0) Death();
    }

    public void Revive()
    {
        CurHealth = MaxHealth;
        IsDead = false;
        OnRevive?.Invoke();
    }

    public void Death()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }
}
