using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public GameObject enemy;
    public float term = 5f;

    private float delta = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, new Vector3(10f, 20f, 10f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;

        if (delta > term)
        {
            float x = Random.Range(-11.9f, 11.9f);
            float y = Random.Range(20f, 25f);
            float z = Random.Range(-11.9f, 11.9f);
            Vector3 initPos = new Vector3(x, y, z);
            Instantiate(enemy, initPos, Quaternion.identity);
            delta = 0f;
        }
    }
}
