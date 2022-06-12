using System.Collections.Generic;
using UnityEngine;

public class CollisionSolver : MonoBehaviour
{
    public List<Collider> Colliders 
    { get; private set; } = new List<Collider>();

    private bool GJK(Collider colliderA, Collider colliderB)
    {
        var support = Support(colliderA, colliderB, Vector3.forward);

        var points = new Simplex();
        points.Push(support);

        var direction = -support;

        while (true) 
        {
            support = Support(colliderA, colliderB, direction);

            if (Vector3.Dot(support, direction) <= 0)
                return false;

            points.Push(support);

            if (points.NextSimplex(ref direction))
                return true;
        }
    }

    private Vector3 Support(Collider colliderA,
                            Collider colliderB,
                            Vector3 direction)
    {
        return colliderA.FindFurthestPoint(direction)
             - colliderB.FindFurthestPoint(-direction);
    }
}
