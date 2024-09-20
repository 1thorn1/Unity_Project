using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float bulletSpeed = 2f; // 발사체의 속도
    public Vector3 direction; // 발사체의 이동 방향

    void Update()
    {
        MoveBullet(); // 발사체 이동
        CheckIfOutOfBounds(); // 화면 밖으로 나가면 파괴
    }

    // 발사체를 이동시키는 함수
    void MoveBullet()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject); // 충돌한 발사체 파괴
            Destroy(gameObject); // 적 파괴
        }
    }
    // 발사체가 화면 밖으로 나가면 파괴하는 함수
    void CheckIfOutOfBounds()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
