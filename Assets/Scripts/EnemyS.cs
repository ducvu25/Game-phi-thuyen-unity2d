using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public struct Information
    {
        public float max;
        public float min;
        public float value;
        public Information(float max, float min, float value)
        {
            this.max = max;
            this.min = min;
            this.value = value;
        }
    }
    public GameObject bullet; // đối tượng đạn
    Information hp; // chỉ số hp
    Player player; // đối tượng player
    ManagerController manager; // code đối tượng điều khiển game
    EnemyManager managerEnemy; // code điều khiển danh sách enemy
    public float speed; // tốc độ
    public int type; // loại
    public int score; // điểm
    float time_spawn = 3f; // thời gian bắn đạn
    float m_time_spawn = 2; // thời gian bắn đạn thực
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        managerEnemy = GameObject.FindGameObjectWithTag("ManagerEnemy").GetComponent<EnemyManager>();
        if (type == 0 || type == 1)
            hp = new Information(500, 0, 500);
        else if (type == 2)
            hp = new Information(1000, 0, 1000);
        else
            hp = new Information(250, 0, 250);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.win.activeSelf || manager.lose.activeSelf)
            return;
        this.Run();
    }
    void Run()
    {
        float delta = Time.deltaTime;
        m_time_spawn -= delta;
        switch (type)
        {
            case 0:
                {
                    transform.position = transform.position + new Vector3(-speed * delta, -speed * delta, 0);
                    break;
                }
            case 1:
                {
                    transform.position = transform.position + new Vector3(speed * delta, -speed * delta, 0);
                    break;
                }
            case 2:
                {
                    transform.position = transform.position + new Vector3(0, -speed * delta, 0);
                    break;
                }
            case 3:
                {
                    transform.position = transform.position + new Vector3(speed * delta, 0, 0);
                    if (m_time_spawn <= 0)
                    {
                        if ((int)Random.Range(0, 5) == 2)
                            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        m_time_spawn = time_spawn;
                    }
                    break;
                }
            case 4:
                {
                    transform.position = transform.position + new Vector3(-speed * delta, 0, 0);
                    if (m_time_spawn <= 0)
                    {
                        if ((int)Random.Range(0, 5) == 2)
                            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        m_time_spawn = time_spawn;
                    }
                    break;
                }
        }
        if (transform.position.y < -7.1 || (transform.position.x < -10 && type == 2))
            Invoke("Destroy", 0.5f);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            manager.PlayerDamage();
            manager.DestroyEnemyAudio();
            Destroy();
        }
        else if (col.CompareTag("BulletPlayer"))
        {
            Debug.Log(hp.value);
            hp.value -= player.Dame();
            Debug.Log(hp.value);
            if (hp.value <= 0)
            {
                manager.AddScore(score);
                manager.NewItem(transform.position, type);
                manager.DestroyEnemyAudio();
                Destroy();
            }
        }
    }
    public void LevelUp(int n)
    {
        hp.max *= Mathf.Pow(1.5f, n);
        hp.value *= Mathf.Pow(1.5f, n);
    }
    void Destroy()
    {
        managerEnemy.Explosion(transform.position);
        Destroy(gameObject);
    }
}
