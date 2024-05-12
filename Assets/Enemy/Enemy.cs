using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    private BaseState currState;

    [SerializeField]
    public List<Transform> waypoints = new List<Transform>();
    [SerializeField]
    public float chaseDistance;
    [SerializeField]
    public Player player; 

    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public RetreatingState retreatingState = new RetreatingState();

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currState = patrolState;
        currState.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            player.OnPowerUpStart += StartRetreating;
            player.OnPowerUpStop += StopRetreating;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currState != null)
        {
            currState.UpdateState(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        currState.ExitState(this);
        currState = state;
        currState.EnterState(this);
    }

    private void StartRetreating()
    {
        SwitchState(retreatingState);
    }

    private void StopRetreating()
    {
        SwitchState(patrolState);
    }

    public void Dead()
    {

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currState != retreatingState)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }
}
