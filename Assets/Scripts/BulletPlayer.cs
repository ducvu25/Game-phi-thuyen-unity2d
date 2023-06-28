using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public GameObject[] Color;
    float speed = 15; // tốc độ đạn
    //Vector2 destination;
    float time = 4f; // thời gian tồn tại
    //float Angle;
    Rigidbody2D myBody; // tác động vật lý
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        for (int i = 0; i < 4; i++)
        {
            Color[i].SetActive(false);
        }
        Color[PlayerPrefs.GetInt("bullet")].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
        Vector2 directionPlus90 = Quaternion.Euler(0, 0, 90) * transform.right;
        myBody.AddForce(directionPlus90 * speed);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
