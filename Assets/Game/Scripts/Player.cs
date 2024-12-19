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
    private FloatingJoystick joystick;

    private bool sounds = true;
    private bool isGamePaused = false;
    private bool cursorIsVisible = false;
    private bool isDesctop = true;
    private float desiredAngle;

    private void Start()
    {
        FindAndGetCamera();
        SubscribeToEvents();
        SetSoundsSettings();
        FindAndGetJoystick();
        DetermineDeviceType();
        FindAndGetEventSystem();
    }

    private void FindAndGetCamera()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("GameCamera");
        gameCamera = cam.GetComponent<Camera>();
    }
    private void SubscribeToEvents()
    {
        GameCanvas.PauseEvent.AddListener(GamePause);
        GameCanvas.ResumeGame.AddListener(ResumeGame);
    }
    private void SetSoundsSettings()
    {
        sounds = YG2.saves.soundsSettings;
    }
    private void FindAndGetJoystick()
    {
        GameObject joystickObject = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickObject.GetComponent<FloatingJoystick>();
    }
    private void DetermineDeviceType()
    {
        string deviceType = YG2.envir.deviceType;

        if (deviceType == "desktop")
        {
            isDesctop = true;
            aimTransform.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
        }
        else if (deviceType == "mobile" || deviceType == "tablet")
        {
            isDesctop = false;
            aimTransform.gameObject.SetActive(false);
            joystick.gameObject.SetActive(true);
        }
        else
        {
            isDesctop = false;
            aimTransform.gameObject.SetActive(false);
            joystick.gameObject.SetActive(true);
        }
    }
    private void FindAndGetEventSystem()
    {
        GameObject even = GameObject.FindGameObjectWithTag("EventSystem");
        eventSystem = even.GetComponent<EventSystem>();
    }



    private void Update()
    {
        if (!isGamePaused)
        {
            if (isDesctop)
            {
                
                DesctopControll();
            }
            else if(!isDesctop)
            {
                

                MobileControll();
            }           
        }
    }

    private void DesctopControll()
    {
        CusorAndAimVisible();

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

        if (!eventSystem.IsPointerOverGameObject() && Input.GetMouseButton(0))
        {
            assaultRifle.Shoot();
        }
    }
    private void MobileControll()
    {
        if (Input.touchCount > 0)
        {
            Vector3 joystickVectrs = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
            Vector3 direction = joystickVectrs - Vector3.zero;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if(joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                desiredAngle = angle;
            }

            if (joystickVectrs.x != 0 || joystickVectrs.y != 0)
            {
                assaultRifle.Shoot();
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, desiredAngle);
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