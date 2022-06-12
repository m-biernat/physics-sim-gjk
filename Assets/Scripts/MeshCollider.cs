using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshCollider : Collider
{
    private Mesh _mesh;

    private void Awake()
    {
        var meshFilter = GetComponent<MeshFilter>();
        _mesh = meshFilter?.mesh;
    }

    public override Vector3 FindFurthestPoint(Vector3 direction)
    {
        var dir = transform.InverseTransformDirection(direction);

        var furthestPoint = Vector3.zero;
        var maxDistance = float.MinValue;

        foreach (var vertex in _mesh.vertices)
        {
            var distance = Vector3.Dot(vertex, dir);
            
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestPoint = vertex;
            }
        }

        return transform.TransformPoint(furthestPoint);
    }

    /* For visual debugging
    private void FixedUpdate()
    {
        Debug.DrawRay(Vector3.zero, Vector3.forward, Color.green);
        Debug.DrawRay(Vector3.zero, FindFurthestPoint(Vector3.forward), Color.red);
    }
    */
}
