using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
        }
    }
}
