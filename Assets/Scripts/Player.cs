using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Sprite spriteLeft;

    [SerializeField]
    private Sprite spriteRight;

    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput < 0)
        {
            spriteRenderer.sprite = spriteLeft;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.sprite = spriteRight;
        }

        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);

        transform.position += moveTo * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Enemy") {
        Destroy(gameObject);
        GameManager.instance.GameOver();
      }
    }
}
