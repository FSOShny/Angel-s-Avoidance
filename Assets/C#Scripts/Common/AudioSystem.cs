using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    // 効果音
    [SerializeField] private List<AudioClip> sound;

    // オーディオソースコンポーネント
    private AudioSource audioSource;

    // 音楽番号
    private int music = -1;
    public int Music
    {
        get { return music; }
        set { music = value; }
    }

    private void Start()
    {
        // オーディオソースコンポーネントを取得する
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        /* 割り振られた番号の効果音を再生する */

        if (music != -1)
        {
            audioSource.PlayOneShot(sound[music]);

            // （番号を初期化する）
            music = -1;
        }
    }
}
