using UnityEngine;

public class InputHold : WeaponComponent<InputHoldData, AttackInputHold>
{
    private Animator anim;

    private bool input;

    private bool minHoldPassed;

    protected override void HandleEnter()
    {
        base.HandleEnter();

        minHoldPassed = false;
    }

    private void HandleCurrentInputChange(bool newInput)
    {
        input = newInput;

        SetAnimatorParamater();
    }

    private void HandleMinHoldPassed()
    {
        minHoldPassed = true;

        SetAnimatorParamater();
    }

    private void SetAnimatorParamater()
    {
        anim.SetBool("hold", input);
    }

    public float GetMultiplier() => minHoldPassed ? currentAttackData.Multiplier : 1f;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        weapon.OnCurrentInputChange += HandleCurrentInputChange;
        eventHandler.OnMinHoldPassed += HandleMinHoldPassed;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        eventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
    }
}
