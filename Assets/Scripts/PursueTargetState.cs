using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    private AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        Debug.Log("running the pursue target state");
        MoveTowardsCurrentTarget(zombieManager);
        RotateTowardsCurrentTarget(zombieManager);

        if (zombieManager.distanceFromCurrentTarget <= zombieManager.minimumAttackDistance)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        zombieManager.animator.SetFloat("Vertical", 2f, 0.2f, Time.deltaTime);
    }

    private void RotateTowardsCurrentTarget(ZombieManager zombieManager)
    {
        zombieManager.zombieNavmeshAgent.enabled = true;
        zombieManager.zombieNavmeshAgent.SetDestination(zombieManager.currentTarget.transform.position);

        // overtime we are going to turn our zombie in the same direction our navmesh agent is turning
        zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation, zombieManager.zombieNavmeshAgent.transform.rotation, zombieManager.rotationSpeed / Time.deltaTime);
    }
}
