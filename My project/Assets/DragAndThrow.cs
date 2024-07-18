using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragAndThrow : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;
    private Rigidbody2D rb;

    public float forceMultiplier = 5.0f;
    public Sprite explosionSprite;
    public GameObject house;
    public Text gameClearText;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // 物理挙動を無効にする
        gameClearText.gameObject.SetActive(false); // ゲーム開始時は非表示
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("SpriteRenderer found: " + (spriteRenderer != null));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            Debug.Log("Start Point: " + startPoint);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("End Point: " + endPoint);
            Vector2 force = (startPoint - endPoint) * forceMultiplier;
            Debug.Log("Force: " + force);
            rb.isKinematic = false; // 物理挙動を有効にする
            rb.AddForce(force, ForceMode2D.Impulse);
            isDragging = false;
        }

        // Rキーが押されたときにシーンをリロードする
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == house)
        {
            // 家を非表示にする
            house.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("House sprite renderer disabled.");

            // 爆弾の画像を爆発の画像に変更する
            if (explosionSprite != null)
            {
                spriteRenderer.sprite = explosionSprite;
                Debug.Log("Explosion sprite set successfully. Sprite name: " + explosionSprite.name);
            }
            else
            {
                Debug.LogError("Explosion sprite is not set in the inspector.");
            }

            // ゲームクリアのテキストを表示する
            gameClearText.gameObject.SetActive(true);
            Debug.Log("Game Clear text displayed.");

            // 3秒後にシーンを移動する
            Invoke("LoadSampleScene", 3.0f);
        }
    }

    private void RestartGame()
    {
        // 現在のシーンをリロードする
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadSampleScene()
    {
        // SampleSceneをロードする
        SceneManager.LoadScene("SampleScene");
    }
}
