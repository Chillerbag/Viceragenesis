using UnityEngine;

namespace AbstractEnemy {
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float shootingInterval = 2f;
        public float bulletSpeed = 5f;
        protected float shootingTimer;

        protected virtual void Update()
        {
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0f)
            {
                Attack();
                shootingTimer = shootingInterval;
            }
        }

        protected abstract void Attack();

        protected void ShootBullet(Vector3 direction, Vector3 startPosition)
        {
            GameObject bullet = Instantiate(bulletPrefab, startPosition, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction.normalized * bulletSpeed;
            }
        }
    }
}