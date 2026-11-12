using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;      // hareket kuvveti
    [SerializeField] private float jumpForce = 6f;   // ziplama kuvveti
    [SerializeField] private TextMeshProUGUI scoreText;
 
    public int score = 0;            // puan (FinishZone da okuyacak)
    public int targetScore = 50;     // hedef puan
    public bool isGrounded = false;  // oyuncu yerde mi?
 
    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private bool jumpRequested = false;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
 
    private void Start()
    {
        UpdateScoreUI();
    }
 
    private void Update()
    {
        // tuslari oku
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
 
        // sadece yerdeyken ziplama istegi olustur
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
 
        // haritadan dusersek oyunu bastan baslat
        if (transform.position.y < -10f)
        {
            RestartGame();
        }
    }
 
    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }
 
    private void MovePlayer()
    {
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        rb.AddForce(movement * speed);
    }
 
    private void Jump()
    {
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;      // havada tekrar ziplayamasin
            jumpRequested = false;
        }
    }
 
    // zemine veya engele ustten temas ediyorsak yerdeyiz
    private void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }
 
    private void OnCollisionStay(Collision collision)
    {
        CheckGround(collision);
    }
 
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
 
    private void CheckGround(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // temas yuzeyi yukari bakiyorsa (ustten degiyorsak) yerdeyiz
            if (collision.GetContact(0).normal.y > 0.5f)
            {
                isGrounded = true;
            }
        }
    }
 
    // nesne toplama
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PositiveCoin"))
        {
            score += 10;
            Destroy(other.gameObject);
            UpdateScoreUI();
        }
        else if (other.CompareTag("NegativeCoin"))
        {
            score -= 5;
            Destroy(other.gameObject);
            UpdateScoreUI();
        }
    }
 
    private void UpdateScoreUI()
    {
        scoreText.text = "Skor: " + score.ToString() + " / " + targetScore.ToString();
    }
 
    // sahneyi bastan yukle (oyuncu basa doner, puan sifirlanir, toplar geri gelir)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}