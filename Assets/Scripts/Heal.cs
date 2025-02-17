using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public ParticleSystem healParticles; // Referencia al sistema de part�culas
    public float healAmount = 10f; // Cantidad de vida a recuperar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Recuperar vida del jugador
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.HealthPoints += healAmount;
            }

            // Activar part�culas antes de destruir el objeto
            if (healParticles != null)
            {
                healParticles.transform.parent = null; // Separa las part�culas del objeto Heal
                healParticles.Play(); // Reproduce las part�culas
                Destroy(healParticles.gameObject, healParticles.main.duration); // Destruye las part�culas despu�s de su duraci�n
            }

            Destroy(gameObject); // Destruye el objeto Heal
        }
    }
}
