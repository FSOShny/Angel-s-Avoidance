using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemy; // エネミープレハブ

    private GamePlayingDirector director;

    private void Start()
    {
        director = GameObject.Find("Game Playing Director").GetComponent<GamePlayingDirector>();
        StartCoroutine(EnemyGenerate(3.0f, 5.0f));
    }

    private IEnumerator EnemyGenerate(float fWT, float sWT)
    {
        yield return new WaitForSeconds(fWT);

        Instantiate(enemy, new Vector3(10f, 20f, 10f), Quaternion.identity);

        while (true)
        {
            yield return new WaitForSeconds(sWT);

            float x = Random.Range(-11.9f, 11.9f);
            float y = Random.Range(20f, 25f);
            float z = Random.Range(-11.9f, 11.9f);
            Vector3 initPos = new Vector3(x, y, z);

            Instantiate(enemy, initPos, Quaternion.identity);
        }
    }
}
