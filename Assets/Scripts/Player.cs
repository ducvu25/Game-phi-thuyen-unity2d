using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] Color;
    public GameObject Bullet; // đối tượng viên đạn
    public GameObject Gun; // vị trí súng
    int nGun = 1; // số lượng đạn ra mỗi lần bắn
    public float max_y, min_y, max_x, min_x; // vị trí tối đa, tối thiểu của nhân vật trên màn chơi
    public struct Information // cấu trúc dữ liệu chỉ số
    {
        public float max; // giá trị tối đa 
        public float min; // giá trị tối thiểu
        public float value; // giá trị hiện tại
        public Information(float max, float min, float value) // khởi tạo
        {
            this.max = max;
            this.min = min;
            this.value = value;
        }
    }
    float angleY = 0f; // góc nghiên hiện tại - dùng trong khi nhân vật di chuyển sang 
    Information dame; // chỉ số sát thương nhân vật
    Information speed; // chỉ số tốc độ nhân vật
    Information time_spawn; // chỉ số tốc độ ra đạn
    ManagerController manager; // điều kiển màn chơi
    // Vector3 mousePos;
    // Vector3 distance;
    float angle; // góc xoay hiện tại
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
        transform.position = new Vector3(0, -4, 0);
        speed = new Information(6, 2, 3);
        dame = new Information(1000, 100, 200);
        time_spawn = new Information(0.4f, 0.05f, 0);
        for (int i = 0; i < 4; i++)
        {
            Color[i].SetActive(false);
        }
        Color[PlayerPrefs.GetInt("type")].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.win.activeSelf)
            return;
        time_spawn.value -= Time.deltaTime;
        // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 direction = mousePos - transform.position;
        // angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        // transform.rotation = Quaternion.Euler(0, 0, angle);
        //this.FollowMouse();
        this.Run();
        this.Attack();
    }
    void Run()
    {
        float move_x = Input.GetAxis("Horizontal"); // bam nut trái phải
        float move_y = Input.GetAxis("Vertical"); // bấm nút lên xuống
        // if (speed.value < speed.max)
        //     speed.value += 0.5f;
        // if (aus && nhac[0] && time_nhac_chay <= 0)
        // { // bật nhạc với đk có nhạc đầu vào và thời gian nhạc chạy cũ đã hết tránh đè nhạc
        //     aus.PlayOneShot(nhac[0]); // phát nhạc
        //     time_nhac_chay = 0.5f; // gán lại thời gian nhạc
        // }
        if (transform.position.y < min_y && move_y < 0) // quá thấp thì sẽ để mặc dịnh ở min_y
            move_y = 0;
        else if (transform.position.y > max_y && move_y > 0)
            move_y = 0;
        if (transform.position.x < min_x && move_x < 0)
            move_x = 0;
        else if (transform.position.x > max_x && move_x > 0)
            move_x = 0;
        if (move_x > 0 && angle > -20)
        {
            angleY -= 2;
        }
        else if (move_x < 0 && angle < 20)
            angleY += 2;
        if (angleY != 0)
        {
            angleY += angleY < 0 ? 0.5f : -0.5f;
            Quaternion newRotation = Quaternion.Euler(0, angleY, 0);
            transform.rotation = newRotation;
        }
        Vector3 move = new Vector3(speed.value * Time.deltaTime * move_x,
                                   speed.value * Time.deltaTime * move_y, 0);
        // tạo khoảng di chuyển để cho mượt ta nhân với chênh lệnh thời gian
        transform.position = transform.position + move;
    }
    void Attack()
    {
        if (time_spawn.value <= time_spawn.min)
        {
            time_spawn.value = time_spawn.max;
            if (nGun % 2 == 1)
                Instantiate(Bullet, Gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            for (int i = 0; i < nGun / 2; i++)
            {
                Instantiate(Bullet, Gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 10 * (i + 1))));
                Instantiate(Bullet, Gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 10 * (i + 1))));
            }

        }
    }
    public float Dame() { return dame.value; }
    public void AdddInfformation(float _dame, float _speed, float _time_spawn)
    {
        dame.value += _dame;
        if (dame.value > dame.max)
            dame.value = dame.max;

        speed.value += _speed;
        if (speed.value > speed.max)
        {
            speed.value = speed.max;
        }

        time_spawn.max -= _time_spawn;
        if (time_spawn.max < 0.15f)
            time_spawn.max = 0.15f;

    }
    public void AddNum()
    {
        if (nGun < 5)
            nGun++;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet2"))
        {
            manager.PlayerDamage();
            Destroy(col.gameObject);
        }
    }
    // void FollowMouse()
    // {

    //     if (Input.GetAxisRaw("Fire1") != 0)
    //         distance = mousePos - transform.position;

    //     if (distance.magnitude > 0)
    //     {
    //         Vector3 targetPoint = mousePos;
    //         transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed.value * Time.deltaTime);
    //     }
    // }
}
