using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletClass : MonoBehaviour
{
    Animator animator;
    Canvas canvas;
    public float bulletSpeed, bulletDamage;
    public bool isEnemyBullet;


    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }

        if (other.transform.gameObject.tag == "Enemy")
        {
            HittableObject hitObj = other.gameObject.GetComponent<HittableObject>();
            if (hitObj != null)
            {
                hitObj.hits -= bulletDamage;
                if (hitObj.hits <= 0)
                {
                    Destroy(other.gameObject);
                }
            }
            Destroy(gameObject);
        }

        if (other.transform.gameObject.tag == "Player" && isEnemyBullet)
        {
            PlayerMovement playerMov = other.gameObject.GetComponent<PlayerMovement>();

            if (playerMov != null && !playerMov.isDodging) // Verifica que el jugador no esté esquivando
            {
                playerMov.HealthPoints -= bulletDamage;
                if (playerMov.HealthPoints <= 0)
                {
                    animator.SetBool("isDead", true);
                    

                    GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);


                }
            }

            Destroy(gameObject);
        }
    }

    public void Congelacion()
    {
        Time.timeScale = 0;
    }
}
