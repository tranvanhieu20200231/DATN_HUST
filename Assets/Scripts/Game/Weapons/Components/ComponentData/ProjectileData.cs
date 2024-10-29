public class ProjectileData : ComponentData<AttackProjectile>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(Projectiles);
    }
}
