namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
    [TaskCategory("Unity/SharedVariable")]
    [TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
    public class CompareSharedInt : Conditional
    {
        [Tooltip("The first variable to compare")]
        public SharedInt variable;
        [Tooltip("Should the value be compared to less than specified?")]
        public SharedBool lessOrEqualTo;
        [Tooltip("The variable to compare to")]
        public SharedInt compareTo;

        public override TaskStatus OnUpdate()
        {
            if(lessOrEqualTo.Value)
                return (variable.Value <= compareTo.Value) ? TaskStatus.Success : TaskStatus.Failure;
            return variable.Value.Equals(compareTo.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            variable = 0;
            compareTo = 0;
        }
    }
}