using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public float WaitTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(WaitTime);
        while (true)
        {
            if (Input.anyKey)
                SceneManager.LoadScene("Main");
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
