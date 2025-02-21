using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parada : MonoBehaviour
{

    public GameObject dollyCart;
    public GameObject FloresDestruibles;

    

    // Update is called once per frame
    void Update()
    {
        if (FloresDestruibles == null)
        {
            CinemachineDollyCart cart = dollyCart.GetComponent<CinemachineDollyCart>();
            cart.m_Speed = 7;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            CinemachineDollyCart cart = dollyCart.GetComponent<CinemachineDollyCart>();
            cart.m_Speed = 0;
        }
    }
    
}
