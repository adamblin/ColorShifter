using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.LightAnchor;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float secondsToShootAgain;
    private float actualTimer;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootingPoint;

    private void Start()
    {
        actualTimer = secondsToShootAgain;
    }

    private void Update()
    {
        ShootBullet();
    }

    private void ShootBullet() {
        if (actualTimer >= secondsToShootAgain)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirectionToMove(GetShootDirection());
            actualTimer = 0;
        }
        else 
        {
            actualTimer += Time.deltaTime;
        }
    }

    public Vector3 GetShootDirection() {
        return transform.TransformDirection(Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            FindAnyObjectByType<GameManager>().MoveToCheckPoint();
        }
    }
}
