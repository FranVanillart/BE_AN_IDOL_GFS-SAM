using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [Header("Configuración")]
    public float fadeDuration = 1.0f; // Duración del fade en segundos
    public Color fadeColor = Color.black; // Color del fade

    [Header("Referencias")]
    public Image fadeImage; // Imagen para el efecto de fade (debe estar en un Canvas)

    private void Start()
    {
        // Inicializa el fade in al cargar la escena
        if (fadeImage != null)
        {
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
            StartCoroutine(Fade(0)); // Fade in (alpha 1 -> 0)
        }
    }

    // Cuando el jugador entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransitionToNextLevel());
        }
    }

    private IEnumerator TransitionToNextLevel()
    {
        // Fade out (alpha 0 -> 1)
        yield return StartCoroutine(Fade(1));

        // Carga el siguiente nivel
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0); // Vuelve al menú principal
        }
    }

    // Corrutina para el efecto de fade
    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null) yield break;

        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return null;
        }
    }
}
