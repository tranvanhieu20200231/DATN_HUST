using UnityEngine;

public class CollisionSenses : CoreComponent
{
    private Movement Movement => movement ? movement : core.GetCoreComponent<Movement>(ref movement);
    private Movement movement;

    public Transform GroundCheck { get => GenericNotlmplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name); private set => groundCheck = value; }
    public Transform WallCheck { get => GenericNotlmplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name); private set => wallCheck = value; }
    public Transform LedgeCheckHorizontal { get => GenericNotlmplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name); private set => ledgeCheckHorizontal = value; }
    public Transform LedgeCheckVertical { get => GenericNotlmplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name); private set => ledgeCheckVertical = value; }
    public Transform CeilingCheck { get => GenericNotlmplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name); private set => ceilingCheck = value; }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;

    public bool Ceiling { get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround); }

    public bool Ground { get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround); }

    public bool WallFront { get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround); }

    public bool LedgeHorizontal { get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround); }
    public bool LedgeVertical { get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround); }

    public bool WallBack { get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround); }
}
