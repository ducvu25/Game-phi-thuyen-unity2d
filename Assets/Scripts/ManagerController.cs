using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{
    public GameObject Finish; // UI
    public GameObject[] Heath; // mạng của nhân vật
    public GameObject cam; // camera
    public GameObject win; // đối tượng hiển thị khi chiến thắng
    public GameObject lose;
    public GameObject[] star; // đánh giá kết quả 
    public GameObject BackGround2; // background màn 2
    public GameObject Round2; // hiển thị khi chuyển sang màn 2
    public List<GameObject> Items; // danh sách các loại vật phẩm
    public Text scoreText; // hiển thị điểm
    public Text timeText; // hiển thị thời gian
    public AudioSource audios; // nhạc nền
    public AudioClip effectAudio; // hiệu ứng âm thanh
    private int playerHealth = 3; // số mạng của nhân vật
    private bool gameOver = false; // kết thúc game
    private int score = 0; // điểm
    float time = 100f; // thời ian 1 map

    // Start is called before the first frame update
    void Start()
    {
        CameraFollow scriptCam = cam.GetComponent<CameraFollow>();
        scriptCam.enabled = false;
        //Finish.SetActive(false);
        //Items = new List<GameObject>(5);
        BackGround2.SetActive(false);
        Round2.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
        scoreText.text = "Score: " + score.ToString();
    }
    void Update()
    {
        if (win.activeSelf || lose.activeSelf)
            return;
        time -= Time.deltaTime;
        if (time <= 0 && !BackGround2.activeSelf)
        {
            BOSS();
            time = 200;
        }
        if (time <= 0 && BackGround2.activeSelf)
            Win();
        //Debug.Log(time);
        string timeT = time.ToString();
        timeText.text = "Time: " + timeT.Substring(0, timeT.IndexOf('.') + 3) + "s";
        // Win();
    }
    public void PlayerDamage()
    {
        this.playerHealth--;
        Heath[this.playerHealth].SetActive(false);
        if (this.playerHealth <= 0)
        {
            lose.SetActive(true);
        }
    }
    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }
    public void AddHeath()
    {
        if (playerHealth < 3)
        {
            Heath[playerHealth].SetActive(true);
            playerHealth++;
        }
    }
    public void NewItem(Vector3 index, int type)
    {
        int x;
        if (type == 0 || type == 1)
            x = (int)Random.Range(0, 100) % 10;
        else if (type == 2)
            x = (int)Random.Range(0, 100) % 5;
        else
            x = (int)Random.Range(0, 100) % 20;
        if (x == 0)
            Instantiate(Items[(int)Random.Range(0, Items.Count)], index, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    void Win()
    {
        win.SetActive(true);
        for (int i = 0; i < playerHealth; i++)
        {
            star[i].SetActive(true);
        }
    }
    void BOSS()
    {
        CameraFollow scriptCam = cam.GetComponent<CameraFollow>();
        scriptCam.enabled = true;
        BackGround2.SetActive(true);
        Round2.SetActive(true);
        Invoke("OffRound2", 1);
        EnemyManager managerBoss = GameObject.FindGameObjectWithTag("ManagerEnemy").GetComponent<EnemyManager>();
        managerBoss.round = 2;
    }
    void OffRound2()
    {
        Round2.SetActive(false);

    }
    public void DestroyEnemyAudio()
    {
        if (audios && effectAudio)
        {
            audios.PlayOneShot(effectAudio);
        }
    }
}