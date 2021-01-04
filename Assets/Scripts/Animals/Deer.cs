using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Deer : MonoBehaviour, IDamageable
{
    


    private BehaviorTree behaviorTree;
    private NavMeshAgent agent;
    private Animator animator;
    bool IdleIndexHasChanged = false;
    private void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //Setting idle index for starting idle animation
        animator.SetInteger("IdleIndex", Random.Range(0, 3));
    }

    private void Update()
    {

        //If agent is stopped and we havent yet set the idle index then set new idle index
        if(this.agent.velocity.magnitude < 0.05 && IdleIndexHasChanged == false)
        {
            animator.SetInteger("IdleIndex", Random.Range(0, 3));
            IdleIndexHasChanged = true;
        }
        //Reset bool
        if(this.agent.velocity.magnitude > 1 && IdleIndexHasChanged == true)
        {
            IdleIndexHasChanged = false;
        }

    }

    private void LateUpdate()
    {
        float speed = this.agent.velocity.magnitude;
        this.animator.SetFloat("WalkSpeed", speed);
    }

    //Take damage implementation on the Deer
    public void TakeDamage(int weaponDamage,GameObject whoInflictedDamage)
    {
        Debug.Log("Deer is taking damage");

        //Play hit animation
        animator.SetTrigger("isHit");

        //Get and decrease health, this variable is on the behavior tree 
        var myvariable = (SharedInt)behaviorTree.GetVariable("health");
        myvariable.Value -= weaponDamage;
        behaviorTree.SetVariable("health", myvariable);

        //Let the behavior tree know who inflicted damage on the Deer
        behaviorTree.SetVariable("target", (SharedGameObject)whoInflictedDamage);

        //Letting behavior tree know we are being hit
        behaviorTree.SetVariableValue("isHit", true);
    }
}
