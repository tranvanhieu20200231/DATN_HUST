using UnityEngine;

public class InputHold : WeaponComponent
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
        if (input)
        {
            anim.SetBool("hold", input);
            return;
        }

        if (minHoldPassed)
        {
            anim.SetBool("hold", false);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponentInChildren<Animator>();

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
