using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動きをAWSDで制御
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    // <インスペクターで登録>
    public CharacterController controller; //キャラクターコントローラー
    public Transform groundCheck; // プレイヤー足元の位置
    public LayerMask groundMask; //地面のレイヤー
    public AudioSource playerAudio; //プレイヤー用のオーディオソース
    public AudioClip[] pAudioClip = new AudioClip[5]; // 足音
    // </インスペクターで登録>

    public float speed = 12f; // プレイヤーの移動速度
    public float gravity = -10f; // 重力
    public float groundDistance = 0.4f; //足元を中心とした球体の半径
    bool isGrounded = true; // プレイヤーが着地しているかどうかのフラグ

    Vector3 velocity; // プレイヤーの現在の速度

    float stepCount; // 足音を鳴らすためのカウンター
    float stepInterval = 0.7f; // 足音を鳴らす間隔

    bool canMove = true; // 動けるかどうかのフラグ
    public bool CanMove　 // canMoveプロパティ
    {
        get { return canMove; }
        set { canMove = value; }
    }

    
    void Update()
    {
        
        if (!canMove) return;// 動けない場合は処理を中断

        float x = Input.GetAxis("Horizontal"); // 水平方向の入力を取得
        float z = Input.GetAxis("Vertical"); // 垂直方向の入力を取得

        // 着地しているかどうかを判定
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // キー入力での移動方向のベクトルを計算
        Vector3 move = transform.right * x + transform.forward * z;

        // プレイヤーを移動
        controller.Move(move * speed * Time.deltaTime);

        //　重力での移動量を計算
        velocity.y += gravity * Time.deltaTime;

        //　重力の影響を適応
        controller.Move(velocity * Time.deltaTime);

        //　移動中なら一定の間隔で足音を鳴らす
        if (x != 0f || z != 0f)
        {
            stepCount += Time.deltaTime;
            if (stepCount >= stepInterval)
            {
                var random = Random.Range(0, 5);
                playerAudio.PlayOneShot(pAudioClip[random]);
                stepCount = 0;
            }
        }
    }
}