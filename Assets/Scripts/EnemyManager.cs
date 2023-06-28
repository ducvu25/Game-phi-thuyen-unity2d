using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] Boss; // danh sách mẫu các loại enemy
    List<GameObject> listLeft; // danh sách enemy bên trái
    List<GameObject> listRight; // danh sách enemy bên phải
    float time_spawn = 10; // thời gian tạo mới enemy
    float m_time_spawn = 0; // thời gian thực
    public GameObject player; // đối tượng người chơi
    public GameObject explosion; // hiệu ứng nổ
    int lv = 0; // lv quái
    public int round = 1; // màn

    // Start is called before the first frame update
    void Start()
    {
        listLeft = new List<GameObject>(); // khởi tạo
        listRight = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        m_time_spawn -= Time.deltaTime;
        if (m_time_spawn <= 0)
        {
            m_time_spawn = time_spawn - lv * 0.1f;
            listLeft.Clear();
            listRight.Clear();
            Spawn_enemy();
            lv++; ;
        }
    }
    void Spawn_enemy()
    {
        int x = (int)Random.Range(0, 3);
        switch (x)
        {
            case 1:
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < lv / 4 + 1; j++)
                        {
                            Vector3 index = new Vector3(-12 - i, round * 3f - j, 0);
                            GameObject xx = Instantiate(Boss[3], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                            EnemyS scripEnemy = xx.gameObject.GetComponent<EnemyS>();
                            scripEnemy.LevelUp(lv / 2);
                            listLeft.Add(xx);

                            index = new Vector3(12 + i, round * 2.5f - j, 0);
                            xx = Instantiate(Boss[4], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                            scripEnemy = xx.gameObject.GetComponent<EnemyS>();
                            scripEnemy.LevelUp(lv / 2);
                            listRight.Add(xx);
                        }
                    }
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < lv / 4 + 1; j++)
                        {
                            Vector3 index = new Vector3(-1 - 2 * j, round * 2 + 5 + 2 * i, 0);
                            GameObject xx = Instantiate(Boss[2], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                            EnemyS scripEnemy = xx.gameObject.GetComponent<EnemyS>();
                            scripEnemy.LevelUp(lv / 2);
                            listLeft.Add(xx);

                            index = new Vector3(1 + 2 * j, round * 2 + 5 + 2 * i, 0);
                            xx = Instantiate(Boss[2], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                            scripEnemy = xx.gameObject.GetComponent<EnemyS>();
                            scripEnemy.LevelUp(lv / 2);
                            listRight.Add(xx);
                        }
                    }
                    break;
                }
            case 0:
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < lv / 4 + 1; j++)
                        {
                            Vector3 index = new Vector3(-12f - i, round * 2 + 5 + i + j, 0);
                            GameObject xx = Instantiate(Boss[0], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                            EnemyS scripEnemy = xx.gameObject.GetComponent<EnemyS>();
                            scripEnemy.LevelUp(lv / 2);
                            listLeft.Add(xx);

                            index = new Vector3(12f + i, round * 2 + 5 + i + j, 0);
                            xx = Instantiate(Boss[1], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                            scripEnemy = xx.gameObject.GetComponent<EnemyS>();
                            scripEnemy.LevelUp(lv / 2);
                            listRight.Add(xx);
                        }
                    }
                    break;
                }
        }
    }
    public void Explosion(Vector3 index)
    {
        Instantiate(explosion, index, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
