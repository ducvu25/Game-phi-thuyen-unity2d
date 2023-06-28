using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject _player; // đối tượng người chơi
    ManagerController manager;
    //public float _distance; 
    //public float speed;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-4, 0, 0);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // FollowPlayer();
        // Vector3 playerPos = _player.transform.position;
        // Vector3 direction = playerPos - transform.position;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // angle = Mathf.Clamp(angle + 180, -80, 80);
        // transform.eulerAngles = new Vector3(0, 0, angle);
    }
    // void FollowPlayer()
    // {
    //     Vector3 distance = this._player.transform.position - transform.position;
    //     if (distance.magnitude > _distance)
    //     {
    //         Vector3 targetPoint = this._player.transform.position - distance.normalized * _distance;
    //         gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPoint, speed * Time.deltaTime);
    //     }
    // }
    // public void SetPlayer(GameObject player)
    // {
    //     _player = player;
    // }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            manager.PlayerDamage();
            Invoke("Destroy", 0.5f);
        }
        else if (col.CompareTag("BulletPlayer"))
        {
            Invoke("Destroy", 0.5f);
        }
    }
    //  void OnCollisionEnter2D(Collision2D col){
    //     if(col.gameObject.CompareTag("Player")){
    //         touch_the_ground = true;
    //         my_anim.SetBool("Jump", false);
    //         effectJump.SetActive(true);
    //     }
    // }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
