using UnityEngine;
public class Platform : MonoBehaviour
{
  public GameObject[] obstacles = { null, };
  bool stepped = false;

  void OnEnable()
  {
    stepped = false;

    for (int i = 0; i < obstacles.Length; i++)
    {
      if (Random.Range(0, 3) == 0)
      {
        obstacles[i].SetActive(true);
      }
      else
      {
        obstacles[i].SetActive(false);
      }
    }
  }

  void OnCollisisonEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Player" && !stepped)
    {
      stepped = true;
      GameManager.instance.AddScore(1);
    }
  }
}
