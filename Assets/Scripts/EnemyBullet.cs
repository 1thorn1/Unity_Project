using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float bulletSpeed = 2f; // �߻�ü�� �ӵ�
    public Vector3 direction; // �߻�ü�� �̵� ����

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // ȭ�� ������ ������ �߻�ü �ı�
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
}