using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuertaAutomatica : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Animator animatorPuerta;
    private const string PLAYER_TAG = "Player";
    private bool jugadorDentro = false;

    private void Awake()
    {
        if (animatorPuerta == null)
        {
            Debug.LogError("Falta asignar el Animator!", this);
            enabled = false;
        }
        else
        {
            animatorPuerta.enabled = false; // Desactiva el Animator inicialmente
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG) && !jugadorDentro)
        {
            jugadorDentro = true;
            animatorPuerta.enabled = true;
            animatorPuerta.Play("ANIMPUERTASCLOSET", 0, 0f); // Reinicia la animación
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            jugadorDentro = false;
            // No desactivamos el Animator para permitir que termine la animación
        }
    }
}
