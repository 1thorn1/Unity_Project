using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float bulletSpeed = 2f; // 발사체의 속도
    public Vector3 direction; // 발사체의 이동 방향

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 화면 밖으로 나가면 발사체 파괴
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