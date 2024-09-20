using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 1f; // ���� �ӵ�
    private Vector2 direction; // ���� �̵� ����

    public GameObject bulletPrefab;
    public Transform firePoint;
    private float fireRate = 4f; // �߻� �ӵ�
    private float nextFireTime = 0f;


    void Start()
    {
        // Turret ������Ʈ�� ã�Ƽ� Transform ����
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
        // ȭ�� ������ ������ �� �ı�
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
            Destroy(other.gameObject); // �浹�� �߻�ü �ı�
            Destroy(gameObject); // �� �ı�

        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        enemyBullet.direction = firePoint.right; // �߻� ���� ����
    }
}