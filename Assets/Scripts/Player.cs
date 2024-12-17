using UnityEngine;

public class Player : MonoBehaviour
{

    public FixedJoystick joystick;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Sprite spriteLeft;

    [SerializeField]
    private Sprite spriteRight;

    private float reverseFlag = 1f;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsReverse()) {
          reverseFlag = -1;
        } else {
          reverseFlag = 1;
        }

        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (horizontalInput < 0)
        {
          if (reverseFlag == -1) {
            spriteRenderer.sprite = spriteRight;
          } else {
            spriteRenderer.sprite = spriteLeft;
          }
        }
        else if (horizontalInput > 0)
        {
          if (reverseFlag == -1) {
            spriteRenderer.sprite = spriteLeft;
          } else {
            spriteRenderer.sprite = spriteRight;
          }
        }

        Vector3 moveTo = new Vector3(horizontalInput * reverseFlag, verticalInput * reverseFlag, 0f);

        transform.position += moveTo * moveSpeed * Time.deltaTime;


        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = new Vector3 (Camera.main.ViewportToWorldPoint(pos).x, Camera.main.ViewportToWorldPoint(pos).y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Enemy") {
        GameManager.instance.LoseLife();
        if (GameManager.instance.GetLife() <= 0) {
          Destroy(gameObject);
        } 
      }
    }
}
