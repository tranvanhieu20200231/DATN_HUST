public class PoiseDamageData : ComponentData<AttackPoiseDamage>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(PoiseDamage);
    }
}
