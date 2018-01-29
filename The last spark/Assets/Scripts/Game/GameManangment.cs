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
    public Image sliderFill;
    public GameObject sliderText;

    public static bool isEditor = false;

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
        if (Input.GetKeyUp(KeyCode.I))
            isEditor = !isEditor;

        if (isEditor)
        {

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Handle_R();
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Handle_Esc();
            }
        }
    }

    void Handle_R()
    {
        SetUI(false);
        sliderUI.SetActive(true);
        sliderText.SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    void Handle_Esc()
    {
        Application.Quit();
    }

    IEnumerable ReloadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!async.isDone)
        {
            sliderFill.fillAmount = Mathf.Clamp01(async.progress / 0.9f);
            yield return null;
        }
        //yield return new WaitForSeconds(0.2f);

    }

    public static bool isKeyPressed(KeyCode KeyCode, bool isEditorPart)
    {
        if ((isEditor && isEditorPart) || (!isEditor && !isEditorPart))
        {
            return Input.GetKey(KeyCode);
        }
        else return false;
    }

    public static bool isKeyUp(KeyCode KeyCode, bool isEditorPart)
    {
        if ((isEditor && isEditorPart) || (!isEditor && !isEditorPart))
        {
            return Input.GetKeyUp(KeyCode);
        }
        else return false;
    }
}
