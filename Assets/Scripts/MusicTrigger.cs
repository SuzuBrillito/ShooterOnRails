using UnityEngine;
using System.Collections;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource musicToActivate;   // Música que se activará
    public AudioSource musicToDeactivate; // Música que se desactivará
    public float fadeDuration = 1.5f;     // Duración de la transición

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador entró al trigger
        {
            StartCoroutine(FadeAudio(musicToDeactivate, musicToActivate, fadeDuration));
        }
    }

    IEnumerator FadeAudio(AudioSource fromAudio, AudioSource toAudio, float duration)
    {
        float time = 0f;
        float startVolumeFrom = fromAudio.volume;

        // Establecer el volumen de la nueva música en 0 antes de activarla
        toAudio.volume = 0f;
        toAudio.enabled = true;  // Activamos el AudioSource para que empiece a reproducirse

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            fromAudio.volume = Mathf.Lerp(startVolumeFrom, 0f, t); // Reduce volumen de la música actual
            toAudio.volume = Mathf.Lerp(0f, 0.85f, t); // Aumenta volumen de la nueva música desde 0

            yield return null;
        }

        fromAudio.volume = 0f;
        toAudio.volume = 0.85f;

        fromAudio.enabled = false; // Desactiva el AudioSource de la música anterior una vez terminado el fade-out
    }
}


