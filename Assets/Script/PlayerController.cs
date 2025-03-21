using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float jumpForce = 5f; // Z�plama g�c�
    public float gravity = -9.81f; // Yer�ekimi etkisi
    public Transform playerCamera;
    private float rotationX = 0f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Fareyi gizleyip, kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
        rb.freezeRotation = true; // D�nmeyi engelle
    }

    void Update()
    {
        // Yerde olup olmad���n� kontrol et
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);

        // Hareket Kontrol�
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0f, moveZ);

        // Kamera D�nd�rme
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        transform.Rotate(0f, mouseX, 0f);  // Y ekseninde d�nd�rme

        rotationX -= mouseY;  // X ekseninde d�nd�rme
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Yaln�zca yukar�-a�a�� s�n�r�
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Z�plama i�lemi
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Space tu�una bas�ld���nda
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);  // Yatay h�zlar� s�f�rlayarak z�plama ba�lat
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // Z�plama kuvveti ekle
        }

        // Yer�ekimi ekle
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);  // Yer�ekimi etkisi uygula
        }
    }
}
