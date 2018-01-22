using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManagement : MonoBehaviour
{
    public GameManangment gameOver;

    public void DeadHandler(GameObject go)
    {
        if (go == gameObject)
        {
            gameOver.GameOver();
        }
        else
        {
            go.SetActive(false);
            StartCoroutine(ExecuteAfterTime(5, go));
        }
    }


    IEnumerator ExecuteAfterTime(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay

        go.SetActive(true); ;

    }
}

