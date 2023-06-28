using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public Text ti_le;
    int phan_tram;
    float time = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        phan_tram = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            int x = (int)Random.Range(1, 10);
            phan_tram += x;
            if (phan_tram > 100)
                phan_tram = 100;
            ti_le.text = phan_tram.ToString() + "%";
            if (phan_tram == 100)
                Invoke("LoadMap", 0.5f);
            time = (int)Random.Range(1, 3) * 0.2f;
        }
    }
    void LoadMap()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LoadMap"));
    }
}
