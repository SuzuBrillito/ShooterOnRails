using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public ParticleSystem healParticles; // Referencia al sistema de partículas
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

            // Activar partículas antes de destruir el objeto
            if (healParticles != null)
            {
                healParticles.transform.parent = null; // Separa las partículas del objeto Heal
                healParticles.Play(); // Reproduce las partículas
                Destroy(healParticles.gameObject, healParticles.main.duration); // Destruye las partículas después de su duración
            }

            Destroy(gameObject); // Destruye el objeto Heal
        }
    }
}
