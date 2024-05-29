using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̓�����AWSD�Ő���
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    // <�C���X�y�N�^�[�œo�^>
    public CharacterController controller; //�L�����N�^�[�R���g���[���[
    public Transform groundCheck; // �v���C���[�����̈ʒu
    public LayerMask groundMask; //�n�ʂ̃��C���[
    public AudioSource playerAudio; //�v���C���[�p�̃I�[�f�B�I�\�[�X
    public AudioClip[] pAudioClip = new AudioClip[5]; // ����
    // </�C���X�y�N�^�[�œo�^>

    public float speed = 12f; // �v���C���[�̈ړ����x
    public float gravity = -10f; // �d��
    public float groundDistance = 0.4f; //�����𒆐S�Ƃ������̂̔��a
    bool isGrounded = true; // �v���C���[�����n���Ă��邩�ǂ����̃t���O

    Vector3 velocity; // �v���C���[�̌��݂̑��x

    float stepCount; // ������炷���߂̃J�E���^�[
    float stepInterval = 0.7f; // ������炷�Ԋu

    bool canMove = true; // �����邩�ǂ����̃t���O
    public bool CanMove�@ // canMove�v���p�e�B
    {
        get { return canMove; }
        set { canMove = value; }
    }

    
    void Update()
    {
        
        if (!canMove) return;// �����Ȃ��ꍇ�͏����𒆒f

        float x = Input.GetAxis("Horizontal"); // ���������̓��͂��擾
        float z = Input.GetAxis("Vertical"); // ���������̓��͂��擾

        // ���n���Ă��邩�ǂ����𔻒�
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // �L�[���͂ł̈ړ������̃x�N�g�����v�Z
        Vector3 move = transform.right * x + transform.forward * z;

        // �v���C���[���ړ�
        controller.Move(move * speed * Time.deltaTime);

        //�@�d�͂ł̈ړ��ʂ��v�Z
        velocity.y += gravity * Time.deltaTime;

        //�@�d�͂̉e����K��
        controller.Move(velocity * Time.deltaTime);

        //�@�ړ����Ȃ���̊Ԋu�ő�����炷
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