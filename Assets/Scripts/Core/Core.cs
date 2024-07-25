using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get => GenericNotlmplementedError<Movement>.TryGet(movement, transform.parent.name); private set => movement = value; }
    public CollisionSenses CollisionSenses { get => GenericNotlmplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name); private set => collisionSenses = value; }
    public Combat Combat { get => GenericNotlmplementedError<Combat>.TryGet(combat, transform.parent.name); private set => combat = value; }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Combat combat;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
