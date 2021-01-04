using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Rabbit : MonoBehaviour, IDamageable
{
    private BehaviorTree behaviorTree;
    private NavMeshAgent mNavMeshAgent;
    private Rigidbody rb;
    private Animator animator;
    private void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        animator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsNotMoving())
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
    }
    
    //Take damage implementation on the Rabbit
    public void TakeDamage(int weaponDamage, GameObject whoInflictedDamage)
    {
        var myvariable = (SharedInt)behaviorTree.GetVariable("health");
        myvariable.Value -= weaponDamage;
        behaviorTree.SetVariable("health", myvariable);
        behaviorTree.SetVariable("target", (SharedGameObject)whoInflictedDamage);
    }

    private bool IsNotMoving()
    {
        if (!mNavMeshAgent.pathPending)
        {
            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                return (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f);                          
            }
        }
        return false;
    }
}
