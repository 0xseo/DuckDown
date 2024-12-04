using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float moveSpeed = 10f;
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

        if (transform.position.x < -50f)
        {
            transform.position += new Vector3(126f, 0, 0);
        }
    }
}
