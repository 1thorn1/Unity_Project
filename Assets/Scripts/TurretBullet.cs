using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float bulletSpeed = 2f; // �߻�ü�� �ӵ�
    public Vector3 direction; // �߻�ü�� �̵� ����

    void Update()
    {
        MoveBullet(); // �߻�ü �̵�
        CheckIfOutOfBounds(); // ȭ�� ������ ������ �ı�
    }

    // �߻�ü�� �̵���Ű�� �Լ�
    void MoveBullet()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject); // �浹�� �߻�ü �ı�
            Destroy(gameObject); // �� �ı�
        }
    }
    // �߻�ü�� ȭ�� ������ ������ �ı��ϴ� �Լ�
    void CheckIfOutOfBounds()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
