using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Ray ray;
    public GameObject bullet;
    public float bulletFireRate;
    public Vector3 hitpoint;
    public LayerMask myLayerMask;
    
    public Animator animator;
    public bool isAttacking = false;
    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("bulletShoot", 0, bulletFireRate);
            animator.SetBool("isAttacking", true);
            isAttacking = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("bulletShoot");
            animator.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }

    private void bulletShoot()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask, QueryTriggerInteraction.Ignore);
        Debug.Log(hit.collider.name);

        hitpoint = hit.point;
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 BulletDirection = hitpoint - transform.position;

        newBullet.gameObject.transform.LookAt(hitpoint);
        float bulSpeed = newBullet.gameObject.GetComponent<BulletClass>().bulletSpeed;
        newBullet.gameObject.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulSpeed, ForceMode.VelocityChange);
    }
}
