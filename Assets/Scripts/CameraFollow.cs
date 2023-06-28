using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // đối tượng cần theo
    Vector3 offset; // độ chênh lệch của cam va nhân vật khi bắt đầu
    public float delay;
    public float max_y, min_y, max_x, min_x; // vị trí hoạt động của cam.
    //Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position; // lấy khoảng cách ban đầu của cam với nhân vật
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPlayer = player.transform.position + offset; // lấy tọa độ mới của cam khi theo nhân vật
        // vt ban dau, vt di chuyen, delay
        transform.position = Vector3.Lerp(transform.position, cameraPlayer, delay * Time.deltaTime);  // caajo nhật vị trí
        // kiểm tra vùng giới hạn
        if (transform.position.y < min_y) // quá thấp thì sẽ để mặc dịnh ở min_y
            transform.position = new Vector3(transform.position.x, min_y, transform.position.z);
        else if (transform.position.y > max_y)
            transform.position = new Vector3(transform.position.x, max_y, transform.position.z);
        if (transform.position.x < min_x)
            transform.position = new Vector3(min_x, transform.position.y, transform.position.z);
        else if (transform.position.x > max_x)
            transform.position = new Vector3(max_x, transform.position.y, transform.position.z);
    }
}
