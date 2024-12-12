public class ShieldData : ComponentData<AttackShield>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(Shield);
    }
}
