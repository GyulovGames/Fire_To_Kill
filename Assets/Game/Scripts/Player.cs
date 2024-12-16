using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using YG;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AssaultRifle assaultRifle;
    [SerializeField] private Transform aimTransform;

    private Camera gameCamera;
    private EventSystem eventSystem;

    private bool sounds = true;
    private bool cursorIsVisible = false;
    private bool isGamePaused = false;


    private void Start()
    {
        Cursor.visible = false;
        sounds = YG2.saves.soundsSettings;

        GameObject cam = GameObject.FindGameObjectWithTag("GameCamera");
        gameCamera = cam.GetComponent<Camera>();

        GameObject even = GameObject.FindGameObjectWithTag("EventSystem");
        eventSystem = even.GetComponent<EventSystem>();

        GameCanvas.PauseEvent.AddListener(GamePause);
        GameCanvas.ResumeGame.AddListener(ResumeGame);
    }

    private void Update()
    {
        if (!isGamePaused)
        {
            CusorAndAimVisible();

            if (!eventSystem.IsPointerOverGameObject() && Input.GetMouseButton(0))
            {
                assaultRifle.Shoot();
            }


            Vector3 cursorScreenPoint = Input.mousePosition;
            Ray ray = gameCamera.ScreenPointToRay(cursorScreenPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 cursorWorldPosition = hit.point;
                cursorWorldPosition.z = -0.55f;
                aimTransform.position = cursorWorldPosition;

                Vector3 direction = aimTransform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    private void CusorAndAimVisible()
    {
        if(eventSystem.IsPointerOverGameObject() && !cursorIsVisible)
        {
            aimTransform.gameObject.SetActive(false);
            Cursor.visible = true;
            cursorIsVisible = true;
        }
        else if(!eventSystem.IsPointerOverGameObject() && cursorIsVisible)
        {
            aimTransform.gameObject.SetActive(true);
            Cursor.visible = false;
            cursorIsVisible = false;
        }
    }
    private void GamePause()
    {
        isGamePaused = true;
        aimTransform.gameObject.SetActive(false);
        Cursor.visible = true;
        cursorIsVisible = true;

    }
    private void ResumeGame()
    {
        isGamePaused = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sounds)
        {
            playerAudioSource.volume = collision.impulse.magnitude * 0.01f;
            playerAudioSource.Play();
        }
    }
}