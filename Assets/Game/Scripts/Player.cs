using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AssaultRifle assaultRifle;
    public Transform target;

    private Camera gameCamera;
    private EventSystem eventSystem;

    private bool sounds = true;
    private bool playerOnGround = true;


    private void Start()
    {
        sounds = YG2.saves.soundsSettings;

        GameObject cam = GameObject.FindGameObjectWithTag("GameCamera");
        gameCamera = cam.GetComponent<Camera>();
        GameObject even = GameObject.FindGameObjectWithTag("EventSystem");
        eventSystem = even.GetComponent<EventSystem>();
    }

    private void Update()
    {
        if (!eventSystem.IsPointerOverGameObject() && Input.GetMouseButton(0))
        {
            //Vector3 mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 direction = mousePosition - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, angle);


            // ѕолучаем позицию курсора в экранных координатах
            Vector3 cursorScreenPosition = Input.mousePosition;

            // —оздаем луч из камеры через позицию курсора
            Ray ray = Camera.main.ScreenPointToRay(cursorScreenPosition);

            // Ќаходим точку пересечени€ луча с плоскостью (например, плоскостью земли)
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // ѕолучаем позицию пересечени€ в мировых координатах
                Vector3 cursorWorldPosition = hit.point;
                Debug.Log("ѕозици€ курсора в мировых координатах: " + cursorWorldPosition);

            }
        Vector3 mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = target.position- transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // angle -= 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

       // assaultRifle.Shoot();
    }

    private void StopRotation()
    {
        if (!playerOnGround)
        {
            rigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerOnGround = true;

        if (sounds)
        {
            playerAudioSource.volume = collision.impulse.magnitude * 0.01f;
            playerAudioSource.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        playerOnGround = false;
    }
}