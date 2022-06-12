using UnityEngine;

public abstract class Collider : MonoBehaviour
{
    public abstract Vector3 FindFurthestPoint(Vector3 direction);

    protected CollisionSolver _collisionSolver;

    protected virtual void Awake()
    {
        var go = GameObject.FindGameObjectWithTag("CollisionSolver");
        _collisionSolver = go.GetComponent<CollisionSolver>();
    }

    protected virtual void OnEnable()
        => _collisionSolver?.Colliders?.Add(this);

    protected virtual void OnDisable()
        => _collisionSolver?.Colliders?.Remove(this);
}
