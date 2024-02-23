using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRendere;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AudioSource bangAudioSource;
    [SerializeField] private AudioSource creakAudioSource;
    [SerializeField] private ParticleSystem piecesParticles;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            boxCollider.enabled = false;
            meshRendere.enabled = false;
            piecesParticles.Play();
            bangAudioSource.Play();
            creakAudioSource.Play();

            Invoke("DisableBox", piecesParticles.main.duration);
        }
    }

    private void DisableBox()
    {
        gameObject.SetActive(false);
    }
}
