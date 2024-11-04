using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30.0f;

    public float damageHopSpeed = 3f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;
    public float wallCheckRadius = 0.3f;

    public float stunResistance = 3.0f;
    public float stunRecoveryTime = 2.0f;

    public float minAgroDistance = 3.0f;
    public float maxAgroDistance = 4.0f;

    public float closeRangeActionDistance = 1.0f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
