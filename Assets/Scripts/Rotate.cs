using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 _axis;

    [SerializeField]
    private float _speed;

    private void Update() 
        => transform.Rotate(_axis.normalized * _speed * Time.deltaTime);
}
