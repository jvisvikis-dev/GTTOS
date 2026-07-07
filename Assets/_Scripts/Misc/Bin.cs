using System;
using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField] private GameObject rubbishParent;
    private int maxRubbish;
    private int rubbishCount;
    public Action trashCleared;
    private void Awake()
    {
        maxRubbish = rubbishParent.transform.childCount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == "Rubbish")
        {
            rubbishCount++;
            Debug.Log("Trash collected");
            if(rubbishCount >= maxRubbish)
            {
                Debug.Log("All trash collected");
                trashCleared?.Invoke();
            }
        }
    }

}
