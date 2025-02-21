using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivaMusica : MonoBehaviour
{
    public GameObject musica;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            musica.SetActive(false);
        }
    }


}
