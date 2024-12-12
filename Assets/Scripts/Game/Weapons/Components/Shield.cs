public class Shield : WeaponComponent<ShieldData, AttackShield>
{
    private DamageReceiver damageReceiver;

    protected override void Awake()
    {
        base.Awake();

        damageReceiver = Core.GetCoreComponent<DamageReceiver>();
    }

    private void HanlderStartReduction()
    {
        if (damageReceiver != null)
        {
            damageReceiver.GetReduction(currentAttackData.ReductionRate);
        }
    }

    private void HanlderStopReduction()
    {
        if (damageReceiver != null)
        {
            damageReceiver.GetReduction(0);
        }
    }

    protected override void Start()
    {
        base.Start();

        eventHandler.OnStartReduction += HanlderStartReduction;
        eventHandler.OnStopReduction += HanlderStopReduction;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        eventHandler.OnStartReduction -= HanlderStartReduction;
        eventHandler.OnStopReduction -= HanlderStopReduction;
    }
}
