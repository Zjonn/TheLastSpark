using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManangment : MonoBehaviour {

    public GameObject miniMapCamera;
    public GameObject gameOverUI;
    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Destroy(player);
        miniMapCamera.SetActive(false);

    }
}
