using System;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public Action<Vector3> hitWater;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            hitWater?.Invoke(transform.position);
        }
    }
}
