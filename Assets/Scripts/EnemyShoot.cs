using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet, player;

    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemyShooting", 0, fireRate);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            CancelInvoke("EnemyShooting");
        }

    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement playerHP = player.gameObject.GetComponent<PlayerMovement>();
        if (playerHP.HealthPoints <= 0)
        {
            CancelInvoke("EnemyShooting");
        }
    }

    private void EnemyShooting()
    {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            
            newBullet.gameObject.transform.LookAt(player.transform.position);
            float bulSpeed = newBullet.gameObject.GetComponent<BulletClass>().bulletSpeed;
            newBullet.gameObject.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulSpeed, ForceMode.VelocityChange);
        
    }
}
