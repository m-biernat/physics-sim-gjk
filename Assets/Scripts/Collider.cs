using UnityEngine;

public abstract class Collider : MonoBehaviour
{
    public abstract Vector3 FindFurthestPoint(Vector3 direction);
}
