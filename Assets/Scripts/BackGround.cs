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
      if (GameManager.instance.IsGameOver()) {
        return;
      }
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < -80f)
        {
            transform.position += new Vector3(180f, 0, 0);
        }
    }
}
