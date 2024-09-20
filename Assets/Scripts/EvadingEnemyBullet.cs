using UnityEngine;

public class EvadingEnemyBullet : MonoBehaviour
{
    public float evadeDistance = 4f; // 총알을 피할 거리
    public float evadeStrength = 1f; // 피하는 힘

    private float speed = 2f; // 오브젝트의 속도
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 evadeDirection = CalculateEvadeDirection();

        Evade(evadeDirection);
        KeepWithinScreenBounds();
    }

    Vector2 CalculateEvadeDirection()
    {
        Vector3 evadeDirection = Vector3.zero;

        // 모든 총알을 찾음
        EnemyBullet[] bullets = FindObjectsOfType<EnemyBullet>();
        foreach (EnemyBullet bullet in bullets)
        {
            Vector3 bulletToObject = transform.position - bullet.transform.position;
            float distance = bulletToObject.magnitude;

            // 가까운 총알일수록 큰 값을 가지도록 함
            // evadeDirection에 더하여 회피 방향을 누적
            if (distance < evadeDistance)
            {
                evadeDirection += bulletToObject.normalized / distance;
            }
        }

        if (evadeDirection != Vector3.zero)
            evadeDirection.Normalize();

        return evadeDirection;
    }

    void Evade(Vector3 evadeDirection)
    {
        if (evadeDirection != Vector3.zero)
            rb2d.velocity = (Vector2)evadeDirection * speed * evadeStrength;  // 피할 방향으로 이동
        else
            rb2d.velocity = (Vector3.zero - transform.position).normalized * speed;  // 기본 이동 (화면 중앙 방향으로 이동)
    }

    void KeepWithinScreenBounds()
    {
        // 화면 경계를 벗어나지 않도록 위치 제한
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(newPosition);

        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // 약간의 여유를 두어 경계에서 튀지 않도록
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f);

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }
}