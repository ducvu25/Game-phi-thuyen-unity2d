using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int heath;
    public float dame, speed, time_spawn, num;
    Player player;
    ManagerController manager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
    }
    void Update()
    {
        if (transform.position.y <= -7)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (heath == 1)
            {
                manager.AddHeath();
            }
            else if (num == 1)
            {
                player.AddNum();
            }
            else
            {
                player.AdddInfformation(dame, speed, time_spawn);
            }
            Destroy(gameObject);
        }
    }
}
