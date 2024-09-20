using UnityEngine;

public class Turret : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f; // �߻� �ӵ�
    private float nextFireTime = 0f;
    private bool isActive = true; // �ͷ��� Ȱ��ȭ ����

    void Update() {
        FindAndLookAtClosestEnemy();
        if (Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
/*        // �ͷ��� ������ �Ѿ��� �ε�����
        if()
        {
            Time.timeScale = 0;
        }*/
    }
    // �߻�ü�� �߻��ϴ� �Լ�
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
        // ��� ���� ���� ���� ����� �� ã��
        foreach (GameObject enemy in enemies) {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // ���� ����� ���� ���� ��ž�� ȸ��
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
            Time.timeScale = 0; // ���� �Ͻ� ����
            isActive = false; // �ͷ� ��Ȱ��ȭ
        }
    }
}
