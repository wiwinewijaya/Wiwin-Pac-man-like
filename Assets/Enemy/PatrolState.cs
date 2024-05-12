using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Patrol");
        isMoving = false;
        enemy.animator.SetTrigger("PatrolState");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Patrolling");
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (!isMoving)
        {
            isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.waypoints.Count);
            destination = enemy.waypoints[index].position;
            enemy.navMeshAgent.destination = destination;
        }
        else
        {
            if (Vector3.Distance(destination, enemy.transform.position) <= 0.1)
            {
                isMoving = false;
            }
        }
    }
}
