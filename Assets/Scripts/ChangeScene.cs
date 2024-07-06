using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a la que quieres cambiar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga un tag "Player"
        {
            SceneManager.LoadScene(sceneName); // Cambia a la escena especificada
        }
    }
}
