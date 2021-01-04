using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Flee from the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}FleeIcon.png")]
    public class Flee : NavMeshMovement
    {
        [Tooltip("The agent has fleed when the magnitude is greater than this value")]
        public SharedFloat fleedDistance = 20;
        [Tooltip("The distance to look ahead when fleeing")]
        public SharedFloat lookAheadDistance = 5;
        [Tooltip("The GameObject that the agent is fleeing from")]
        public SharedGameObject target;
        [Tooltip("The GameObject tag that the agent is fleeing from")]
        public SharedString targetTag;
        [Tooltip("Flee based on target ditance?")]
        public SharedBool fleeBasedOnTargetDistance;

        private bool hasMoved;

        public override void OnStart()
        {
            base.OnStart();

            //If we decide to use tag then find the game obj with tag and set our target
            if (targetTag.Value != null)
                target.Value = GameObject.FindGameObjectWithTag(targetTag.Value);


            //Calculate flee distance on top of the current distance
            if (fleeBasedOnTargetDistance.Value)
            {
                float ditanceBetweenTargetObject = Vector3.Distance(this.gameObject.transform.position, target.Value.transform.position);
                fleedDistance.Value += ditanceBetweenTargetObject;
            }

          
            SetDestination(Target());
        }

        // Flee from the target. Return success once the agent has fleed the target by moving far enough away from it
        // Return running if the agent is still fleeing
        public override TaskStatus OnUpdate()
        {
          
            if (Vector3.Magnitude(transform.position - target.Value.transform.position) > fleedDistance.Value) {
                return TaskStatus.Success;
            }

            if (HasArrived()) {
                if (!hasMoved) {
                    return TaskStatus.Failure;
                }
                if (!SetDestination(Target())) {
                    return TaskStatus.Failure;
                }
                hasMoved = false;
            } else {
                // If the agent is stuck the task shouldn't continue to return a status of running.
                var velocityMagnitude = Velocity().sqrMagnitude;
                if (hasMoved && velocityMagnitude <= 0f) {
                    return TaskStatus.Failure;
                }
                hasMoved = velocityMagnitude > 0f;
            }

            return TaskStatus.Running;
        }

        // Flee in the opposite direction
        private Vector3 Target()
        {
            return transform.position + (transform.position - target.Value.transform.position).normalized * lookAheadDistance.Value;
        }

        // Return false if the position isn't valid on the NavMesh.
        protected override bool SetDestination(Vector3 destination)
        {
            if (!SamplePosition(destination)) {
                return false;
            }
            return base.SetDestination(destination);
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            fleedDistance = 20;
            lookAheadDistance = 5;
            target = null;
        }
    }
}