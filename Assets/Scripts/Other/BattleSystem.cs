using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{

    public event EventHandler OnBattleStarted;
    public event EventHandler OnBattleOver;


    private enum State
    {
        IDLE,
        ACTIVE,
        BATTLE_OVER
    }

    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Wave[] waves;

    private State state;

    private void Awake() {
        state = State.IDLE;
    }
    private void Start() 
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        
    }
    private void Update() {
        switch (state)
        {
            case State.ACTIVE:
                foreach (var wave in waves)
                {
                    wave.Update();
                }
                TestBattleOver();
            break;

            
        }
        
    }

    private void TestBattleOver()
    {
        if (state==State.ACTIVE)
        {
            if (AreWavesOver())
            {
                Debug.Log("Battle Over");
                state=State.BATTLE_OVER;
                OnBattleOver?.Invoke(this,EventArgs.Empty);

            }
        }
    }


    private bool AreWavesOver()
    {
        foreach (var wave in waves)
            {
                if (wave.IsWaveOver())
                {
                    //wave is over
                } 
                else    
                    return false;
                
            }
        return true;
    }


    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        if (state==State.IDLE)
        {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
    }



    public void StartBattle()
    {
        Debug.Log("Start Battle");
        state=State.ACTIVE;
        OnBattleStarted?.Invoke(this,EventArgs.Empty);
    }


//MARKER: REPRESENTS SINGLE ENEMY WAVE
[System.Serializable]
private class Wave
{
    [SerializeField] private Transform[] enemies;
    [SerializeField] private float timer;


    public void Update() {
        if (timer>=0)
        {
            timer-=Time.deltaTime;
            if (timer<=0)
            {
                SpawnEnemies();
            }
        }
    }

    public void SpawnEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Spawn();          
        }
    }

    public bool IsWaveOver()
    {
        if (timer<0)
        {
            //wave spawned
            foreach (var enemy in enemies)
            {
                if (enemy!=null)
                {
                    return false;
                }
            }
            return true; 
        }
        else
            return false;


    }



}


    
}


