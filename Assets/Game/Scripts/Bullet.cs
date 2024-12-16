using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [Space(20)]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem collisionParticles;

    private void OnEnable()
    {
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
        rigidBody.AddForce(transform.right * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 8)
        {
            rigidBody.velocity = Vector3.zero;
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
            collisionParticles.Play();
            Invoke("DeactivateTheBullet", collisionParticles.main.duration);
        }
    }

    private void DeactivateTheBullet()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles= Vector3.zero;
        gameObject.SetActive(false);
    }
}