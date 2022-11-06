using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDirector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AnimationBind");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AnimationBind()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
