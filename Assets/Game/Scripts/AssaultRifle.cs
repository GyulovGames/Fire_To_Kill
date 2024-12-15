using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private float spread;
    [SerializeField] private float recoil;
    [SerializeField] private float fireRate;
    [Space(20)]
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private AudioSource weaponAudioSource;
    [SerializeField] private Rigidbody playerRigidBody;

    private float nextFire;


    public void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            GameObject bullet = bulletPool.GetPooledBullet();
            shootPoint.localEulerAngles = shootPoint.forward * Random.Range(spread, -spread);
            bullet.transform.position = shootPoint.transform.position;
            bullet.transform.rotation = shootPoint.transform.rotation;
            bullet.SetActive(true);

            weaponAnimator.SetTrigger("Shoot");
            weaponAudioSource.Play();
            GunRecoil();
        }
    }

    private void GunRecoil()
    {
        playerRigidBody.AddForce(-transform.right * recoil, ForceMode.Impulse);
    }
}