using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 1f; // 적의 속도
    private Vector2 direction; // 적의 이동 방향

    public GameObject bulletPrefab;
    public Transform firePoint;
    private float fireRate = 4f; // 발사 속도
    private float nextFireTime = 0f;


    void Start()
    {
        // Turret 오브젝트를 찾아서 Transform 저장
        GameObject turret = GameObject.FindWithTag("Turret");
        if (turret != null)
        {
            direction = (turret.transform.position - transform.position).normalized;
        }
        else
        {
            direction = Random.insideUnitCircle.normalized;
        }
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();

        }
        // 화면 밖으로 나가면 적 파괴
        if (IsOutsideViewport())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutsideViewport()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // 충돌한 발사체 파괴
            Destroy(gameObject); // 적 파괴

        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        enemyBullet.direction = firePoint.right; // 발사 방향 설정
    }
}