using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private float crashTime;
    private Vector3 origPos;
    private float timer;

    private void Awake()
    {
        origPos = transform.position;
    }

    private void OnEnable()
    {
        taskManager.killPlayer += CrashIntoPlayer;
    }

    private void CrashIntoPlayer()
    {
        player.LookAt(transform.position);
        StartCoroutine(MoveToPlayer());
    }
    public IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        while (timer < crashTime && player)
        {
            timer += Time.deltaTime;
            float x = Mathf.Lerp(origPos.x, player.transform.position.x, timer / crashTime);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<PlayerController>()) 
        { 
            Destroy(other.gameObject);
            UIManager.Instance.OpenEndGamePanel();
        }
    }
}
