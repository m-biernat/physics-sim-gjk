using System;
using UnityEngine;

public abstract class Collider : MonoBehaviour
{
    private CollisionSolver _collisionSolver;

    public bool Colliding { get; private set; }

    private bool _currentState;

    public event Action CollisionEnter;

    public event Action CollisionExit;

    protected virtual void Awake()
    {
        var go = GameObject.FindGameObjectWithTag("CollisionSolver");
        _collisionSolver = go.GetComponent<CollisionSolver>();

        Colliding = _currentState = false;
    }

    protected virtual void OnEnable()
        => _collisionSolver?.Colliders?.Add(this);

    protected virtual void OnDisable()
        => _collisionSolver?.Colliders?.Remove(this);

    public abstract Vector3 FindFurthestPoint(Vector3 direction);

    public void RegisterCollisionResult(bool result)
    {
        if (_currentState == false)
            _currentState = result;
    }

    public void UpdateState()
    {
        if (Colliding != _currentState)
        {
            if (_currentState == true)
                CollisionEnter?.Invoke();
            else
                CollisionExit?.Invoke();
            
            Colliding = _currentState;
        }

        _currentState = false;
    }
}
