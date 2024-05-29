using UnityEngine;
using System.Collections;

/// <summary>
/// 地下室のオーディオを管理する
/// </summary>

public class UnderGroundBGMControlle : MonoBehaviour
{
    public AudioClip roomAudioClip; // 地下室のBGM
    public AudioSource audioSource; // オーディオソース

    public float fadeTime = 2.0f; // フェードにかかる時間

    void Start()
    {
        // 再生するBGMを登録
        audioSource.clip = roomAudioClip; 
        audioSource.loop = true; 
        audioSource.volume = 0f;
    }

    //プレイヤーが地下室に入ったらBGMをフェードインさせる
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            audioSource.Play();
            StartCoroutine(FadeInAudio());
        }
    }

    //プレイヤーが地下室から出たらBGMをフェードアウトさせる
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            StartCoroutine(FadeOutAudio());
        }
    }

    // BGMをフェードインさせるコルーチン
    IEnumerator FadeInAudio()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f; // 完全にフェードイン
    }

    // BGMをフェードアウトさせるコルーチン
    IEnumerator FadeOutAudio()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f; // 完全にフェードアウト
        audioSource.Stop();
    }
}
