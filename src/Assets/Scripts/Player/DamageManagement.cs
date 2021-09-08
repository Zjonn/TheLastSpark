using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManagement : MonoBehaviour
{
    public GameManangment gameOver;
    public GameObject commandPod;

    public void DeadHandler(GameObject go)
    {
        if (!go.Equals(commandPod))
        {
            go.SetActive(false);
            StartCoroutine(ExecuteAfterTime(5, go));
        }
        else
        {
            gameObject.SetActive(false);
            gameOver.GameOver();
        }     
    }

    IEnumerator ExecuteAfterTime(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(true);
    }
}

