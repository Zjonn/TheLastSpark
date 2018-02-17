using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManangment : MonoBehaviour
{
    public List<GameObject> GameUI;

    public GameObject gameOverUI;
    public GameObject sliderUI;
    public GameObject sliderText;

    public Image sliderFill;


    public static bool isEditorMode = true;

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        SetUI(false);
    }


    private void SetUI(bool state)
    {
        foreach (GameObject go in GameUI)
        {
            go.SetActive(state);
        }
    }

    private void Update()
    {
        ManageInput();
    }

    void ManageInput()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            isEditorMode = !isEditorMode;
        }

        if (Input.GetKeyUp(KeyCode.R) && !isEditorMode)
        {
            RestartGame();
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !isEditorMode)
        {
            Quit();
        }
    }

    void RestartGame()
    {
        SetUI(false);
        sliderUI.SetActive(true);
        sliderText.SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    void Quit()
    {
        Application.Quit();
    }

    IEnumerable RestartScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!async.isDone)
        {
            sliderFill.fillAmount = Mathf.Clamp01(async.progress / 0.9f);
            yield return null;
        }
    }
}
