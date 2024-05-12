using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatingState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Retreat");
        enemy.animator.SetTrigger("RetreatState");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Retreat");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Retreating");
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;
        }
    }
}
