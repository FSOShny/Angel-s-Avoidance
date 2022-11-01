using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public GameObject enemy;

    private Vector3 initPos;
    private float x;
    private float y;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        int judge = Random.Range(1, 4);
        Judgement(judge, x, y, z);
        Vector3 initPos = new Vector3(x, y, z);
        Instantiate(enemy, initPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Judgement(int rnd, float x, float y, float z)
    {
        y = Random.Range(2.5f, 27.5f);
    }
}
