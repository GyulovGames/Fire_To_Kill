using UnityEngine;
using UnityEngine.Events;

public class Bacteria : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private MeshRenderer meshRendere;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AudioSource bangAudioSource;
    [SerializeField] private AudioSource splashAudioSource;
    [SerializeField] private ParticleSystem splashParticles;


    public static UnityEvent EnemyKill = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            rigidBody.isKinematic = true;
            boxCollider.enabled = false;
            meshRendere.enabled = false;
            splashParticles.Play();
            bangAudioSource.Play();
            splashAudioSource.Play();

            EnemyKill.Invoke();
            Invoke(nameof(DisbleBacteria), splashParticles.main.duration);
        }
    }

    private void DisbleBacteria()
    {
        gameObject.SetActive(false);
    }
}