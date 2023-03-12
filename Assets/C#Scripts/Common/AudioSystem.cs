using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    // ���ʉ�
    [SerializeField] private List<AudioClip> sound;

    // �I�[�f�B�I�\�[�X�R���|�[�l���g
    private AudioSource audioSource;

    // ���y�ԍ�
    private int music = -1;
    public int Music
    {
        get { return music; }
        set { music = value; }
    }

    private void Start()
    {
        // �I�[�f�B�I�\�[�X�R���|�[�l���g���擾����
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        /* ����U��ꂽ�ԍ��̌��ʉ����Đ����� */

        if (music != -1)
        {
            audioSource.PlayOneShot(sound[music]);

            // �i�ԍ�������������j
            music = -1;
        }
    }
}
