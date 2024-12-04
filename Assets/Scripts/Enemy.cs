using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5f;

    private float minX = -30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (GameManager.instance.gameOver) {
        return;
      }
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
    }
}
