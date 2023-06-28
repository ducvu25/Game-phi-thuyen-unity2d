using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    public GameObject[] Color;
    public GameObject[] Bullet;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Color[i].SetActive(false);
            Bullet[i].SetActive(false);
        }
        Color[PlayerPrefs.GetInt("type")].SetActive(true);
        Bullet[PlayerPrefs.GetInt("bullet")].SetActive(true);
    }
    public void SetColor(int value)
    {
        Color[PlayerPrefs.GetInt("type")].SetActive(false);
        PlayerPrefs.SetInt("type", value);
        Color[PlayerPrefs.GetInt("type")].SetActive(true);
    }
    public void SetBullet(int value)
    {
        Bullet[PlayerPrefs.GetInt("bullet")].SetActive(false);
        PlayerPrefs.SetInt("bullet", value);
        Bullet[PlayerPrefs.GetInt("bullet")].SetActive(true);
    }
}
