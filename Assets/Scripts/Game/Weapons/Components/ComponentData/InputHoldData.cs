public class InputHoldData : ComponentData<AttackInputHold>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(InputHold);
    }
}