using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Bear : MonoBehaviour, IDamageable
{   
    private BehaviorTree behaviorTree;
    private NavMeshAgent agent;
    private Animator animator;
    private bool IdleIndexHasChanged = false;                               
                                                                  
    public int damage = 75;

    private void Start()
    {
        //Initializing component references
        behaviorTree = GetComponent<BehaviorTree>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //Setting idle index for starting idle animation
        animator.SetInteger("IdleIndex", Random.Range(0, 4));
    }

    private void Update()
    {
       
        //If agent is stopped and we havent yet set the idle index then set new idle index
        if (this.agent.velocity.magnitude < 0.05 && IdleIndexHasChanged == false)
        {
            animator.SetInteger("IdleIndex", Random.Range(0, 4));
            IdleIndexHasChanged = true;
        }

        //Reset bool
        if (this.agent.velocity.magnitude > 1 && IdleIndexHasChanged == true)
        {
            IdleIndexHasChanged = false;
        }
    }

    private void LateUpdate()
    {
        //Syncronizing nav mesh speed with the animator (for locomotion blend tree)
        float speed = this.agent.velocity.magnitude;
        this.animator.SetFloat("WalkSpeed", speed);
    }

    /// <summary>
    /// Take damage implementation on the bear
    /// </summary>
    /// <param name="weaponDamage">The damage</param>
    /// <param name="whoInflictedDamage">The object who is inflicting damage</param>
    public void TakeDamage(int weaponDamage,GameObject whoInflictedDamage)
    {
        Debug.Log("Bear is taking damage");

        //Play hit animation
        animator.SetTrigger("isHit");

        //Get and decrease health, this variable is on the behavior tree 
        var myvariable = (SharedInt)behaviorTree.GetVariable("health");
        myvariable.Value -= weaponDamage;
        behaviorTree.SetVariable("health", myvariable);
        behaviorTree.SetVariable("target",(SharedGameObject)whoInflictedDamage);

        //Letting behavior tree know we are being hit
        behaviorTree.SetVariableValue("isHit", true);
    }

    /// <summary>
    /// Basically makes use of the IDamageable interface implemented on the object the bear is attacking
    /// </summary>
    /// <param name="target">The target to attack</param>
    public void Attack(GameObject target)
    {
        var damageable = target.GetComponent<IDamageable>();
        if (damageable == null) return;
        damageable.TakeDamage(damage,this.gameObject);
    }
}
