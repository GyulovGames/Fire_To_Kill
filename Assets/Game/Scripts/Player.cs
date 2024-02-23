using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AssaultRifle assaultRifle;

    private bool gameStarted = true;
    private bool playerOnGround = true;

    private void FixedUpdate()
    {
        if (gameStarted)
        {
            if(Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                StopRotation();
                assaultRifle.Shoot();
            }
        }
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
        playerAudioSource.volume = collision.impulse.magnitude * 0.01f;
        playerAudioSource.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        playerOnGround = false;
    }
}