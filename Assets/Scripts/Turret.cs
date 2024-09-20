using UnityEngine;

public class Turret : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f; // 발사 속도
    private float nextFireTime = 0f;
    private bool isActive = true; // 터렛의 활성화 상태

    void Update() {
        FindAndLookAtClosestEnemy();
        if (Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
/*        // 터렛과 적군의 총알이 부딪히면
        if()
        {
            Time.timeScale = 0;
        }*/
    }
    // 발사체를 발사하는 함수
    void Fire() {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        TurretBullet turretBullet = bullet.GetComponent<TurretBullet>();
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        turretBullet.direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void FindAndLookAtClosestEnemy() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        // 모든 적에 대해 가장 가까운 적 찾기
        foreach (GameObject enemy in enemies) {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // 가장 가까운 적을 향해 포탑을 회전
        if (closestEnemy != null) {
            Vector2 direction = closestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Time.timeScale = 0; // 게임 일시 중지
            isActive = false; // 터렛 비활성화
        }
    }
}
