using UnityEngine;
public class PlatformSpawner : MonoBehaviour
{
  public GameObject platformPrefab = null;
  public int count = 0;

  public float timeBetSpawnMin = 1.25f;
  public float timeBetSpawnMax = 2.25f;
  public float timeBetSpawn = 0f;

  public float yMin = -3.5f;
  public float yMax = 1.5f;
  public float xPos = 20f;

  GameObject[] platforms = { null, };
  int currentIndex = 0;

  Vector2 poolPosition = new Vector2(0, -25);
  float lastSpawnTime = 0;

  void Start()
  {
    platforms = new GameObject[count];

    for (int i = 0; i < count; i++)
    {
      platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
    }

    lastSpawnTime = 0f;
    timeBetSpawn = 0f;
  }
  void Update()
  {
    if (GameManager.instance.isGameOver)
    {
      return;
    }

    if (Time.deltaTime >= lastSpawnTime + timeBetSpawn)
    {
      lastSpawnTime = Time.time;
      timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
      float yPos = Random.Range(yMin, yMax);

      platforms[currentIndex].SetActive(false);
      platforms[currentIndex].SetActive(true);

      platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
      currentIndex++;

      if (currentIndex >= count)
      {
        currentIndex = 0;
      }
    }
  }
}
