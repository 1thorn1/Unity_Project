using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 4f; // �� ���� �ӵ�
    private float nextSpawnTime = 0f;

    // �߾� �簢�� ������ ũ��
    private float centralRectWidth = 0.7f;
    private float centralRectHeight = 0.7f;

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float x, y;
        do
        {
            x = Random.Range(0f, 1f);
            y = Random.Range(0f, 1f);
        }
        while (IsInsideCentralRect(x, y));

        Vector2 viewportPos = new Vector2(x, y);
        return Camera.main.ViewportToWorldPoint(viewportPos);
    }

    // (x, y) �� centralRectWidth, centralRectHeight ���ο� ������ treu return
    private bool IsInsideCentralRect(float x, float y)
    {
        float minX = (1 - centralRectWidth) / 2f;
        float maxX = minX + centralRectWidth;
        float minY = (1 - centralRectHeight) / 2f;
        float maxY = minY + centralRectHeight;

        return x > minX && x < maxX && y > minY && y < maxY;
    }
}