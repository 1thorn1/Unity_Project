using UnityEngine;

public class EvadingEnemyBullet : MonoBehaviour
{
    public float evadeDistance = 4f; // �Ѿ��� ���� �Ÿ�
    public float evadeStrength = 1f; // ���ϴ� ��

    private float speed = 2f; // ������Ʈ�� �ӵ�
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

        // ��� �Ѿ��� ã��
        EnemyBullet[] bullets = FindObjectsOfType<EnemyBullet>();
        foreach (EnemyBullet bullet in bullets)
        {
            Vector3 bulletToObject = transform.position - bullet.transform.position;
            float distance = bulletToObject.magnitude;

            // ����� �Ѿ��ϼ��� ū ���� �������� ��
            // evadeDirection�� ���Ͽ� ȸ�� ������ ����
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
            rb2d.velocity = (Vector2)evadeDirection * speed * evadeStrength;  // ���� �������� �̵�
        else
            rb2d.velocity = (Vector3.zero - transform.position).normalized * speed;  // �⺻ �̵� (ȭ�� �߾� �������� �̵�)
    }

    void KeepWithinScreenBounds()
    {
        // ȭ�� ��踦 ����� �ʵ��� ��ġ ����
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(newPosition);

        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // �ణ�� ������ �ξ� ��迡�� Ƣ�� �ʵ���
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f);

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }
}