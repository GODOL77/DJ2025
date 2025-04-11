using UnityEngine;
public class ScrollingObject : MonoBehaviour
{
  public float speed = 0f;

  void Update()
  {
    if (!GameManager.instance.isGameOver)
    {
      transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
  }
}
