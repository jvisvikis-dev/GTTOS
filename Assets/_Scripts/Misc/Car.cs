using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private float crashTime;
    [SerializeField] private AnimationCurve carStoppingCurve;
    [SerializeField] private AudioClip scream;
    [SerializeField] private GameObject carModel;
    private Vector3 origPos;
    private float timer;

    private void Awake()
    {
        origPos = transform.position;
        carModel.SetActive(false);
    }

    private void OnEnable()
    {
        taskManager.killPlayer += CrashIntoPlayer;
        taskManager.stopForPlayer += StopBeforePlayer;
    }

    private void OnDisable()
    {
        taskManager.killPlayer -= CrashIntoPlayer;
        taskManager.stopForPlayer -= StopBeforePlayer;
    }

    private void CrashIntoPlayer()
    {
        carModel.SetActive(true);
        player.LookAt(transform.position);
        StartCoroutine(MoveToPlayer());
    }

    private void StopBeforePlayer()
    {
        carModel.SetActive(true);
        player.LookAt(transform.position);
        StartCoroutine(MoveToBeforePlayer());
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

    public IEnumerator MoveToBeforePlayer()
    {
        Debug.Log("Stop before the player");
        yield return new WaitForSeconds(0.5f);
        while (timer < crashTime && player)
        {
            timer += Time.deltaTime;
            float x = Mathf.Lerp(origPos.x, player.transform.position.x + 5f, carStoppingCurve.Evaluate(timer / crashTime));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        bool playCameraActive = true;
        player.SetCamera(playCameraActive);
        player.ToggleAllowedMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<PlayerController>()) 
        {
            AudioManager.Instance.Play2DSound(scream);
            AudioManager.Instance.StopBackgroundMusic();
            Destroy(other.gameObject);
            UIManager.Instance.OpenEndGamePanel();
        }
    }
}
