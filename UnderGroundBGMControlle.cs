using UnityEngine;
using System.Collections;

/// <summary>
/// �n�����̃I�[�f�B�I���Ǘ�����
/// </summary>

public class UnderGroundBGMControlle : MonoBehaviour
{
    public AudioClip roomAudioClip; // �n������BGM
    public AudioSource audioSource; // �I�[�f�B�I�\�[�X

    public float fadeTime = 2.0f; // �t�F�[�h�ɂ����鎞��

    void Start()
    {
        // �Đ�����BGM��o�^
        audioSource.clip = roomAudioClip; 
        audioSource.loop = true; 
        audioSource.volume = 0f;
    }

    //�v���C���[���n�����ɓ�������BGM���t�F�[�h�C��������
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            audioSource.Play();
            StartCoroutine(FadeInAudio());
        }
    }

    //�v���C���[���n��������o����BGM���t�F�[�h�A�E�g������
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            StartCoroutine(FadeOutAudio());
        }
    }

    // BGM���t�F�[�h�C��������R���[�`��
    IEnumerator FadeInAudio()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f; // ���S�Ƀt�F�[�h�C��
    }

    // BGM���t�F�[�h�A�E�g������R���[�`��
    IEnumerator FadeOutAudio()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f; // ���S�Ƀt�F�[�h�A�E�g
        audioSource.Stop();
    }
}
