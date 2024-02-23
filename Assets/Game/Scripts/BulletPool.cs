using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int ammountInBulletsPool;
    [SerializeField] private GameObject bulletPrefab;

    private List<GameObject> bulletsLIst;


    private void Start()
    {
        bulletsLIst = new List<GameObject>();
        GameObject bulletInstace;

        for (int i = 0; i < ammountInBulletsPool; i++)
        {
            bulletInstace = Instantiate(bulletPrefab);
            bulletInstace.SetActive(false);
            bulletInstace.transform.position = new Vector3(0, 0, 0);
            bulletsLIst.Add(bulletInstace);
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < ammountInBulletsPool; i++)
        {
            if (!bulletsLIst[i].activeInHierarchy)
            {
                return bulletsLIst[i];
            }
        }

        return null;
    }
}