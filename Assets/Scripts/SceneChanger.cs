using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public void Load(int index)
    {
        PlayerPrefs.SetInt("LoadMap", index);
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
