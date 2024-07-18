using UnityEngine;

public class Target : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // クリア処理
            Debug.Log("目標に当たった！");
            // ここにクリアの処理を追加します。例えば、次のレベルに移行するなど。
        }
    }
}
