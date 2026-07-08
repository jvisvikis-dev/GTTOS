using System;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] private GameObject mesh;
    public GameObject Mesh => mesh;
    public Action<Vector3> hitWater;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            hitWater?.Invoke(transform.position);
        }
    }
}
