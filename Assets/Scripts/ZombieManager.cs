using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public IdleState startingState; // the state this character begins with

    [Header("Current State")]
    [SerializeField] private State currentState; // the state this character is currently on 

    [Header("Current Target")]
    public PlayerManager currentTarget;
    public float distanceFromCurrentTarget;

    [Header("Animator")]
    public Animator animator;

    [Header("Navmesh Agent")]
    public NavMeshAgent zombieNavmeshAgent;

    [Header("Rigidbody")]
    private Rigidbody zombieRigidbody;

    [Header("Locomotion")]
    public float rotationSpeed = 5f;

    [Header("Attack")]
    public float minimumAttackDistance = 1;

    private void Awake()
    {
        currentState = startingState;
        animator = GetComponent<Animator>();
        zombieNavmeshAgent = GetComponentInChildren<NavMeshAgent>();
        zombieRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void Update()
    {
        // for making sure navmesh agent is never moving out, its staying at the same position as zombie
        zombieNavmeshAgent.transform.localPosition = Vector3.zero;

        if (currentTarget != null)
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }

    private void HandleStateMachine()
    {
        State nextState;

        if (currentState != null)
        {
            nextState = currentState.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }


    }
}
