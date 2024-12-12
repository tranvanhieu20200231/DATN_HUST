using UnityEngine;

public class InputHold : WeaponComponent<InputHoldData, AttackInputHold>
{
    private Animator anim;

    private Rigidbody2D rb;

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

        rb.velocity = Vector3.zero;

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

        rb = GetComponentInParent<Rigidbody2D>();
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
