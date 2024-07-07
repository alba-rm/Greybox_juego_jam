using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método para cargar la escena del juego
    public void PlayGame()
    {
        // Asegúrate de que el nombre de la escena esté correcto
        SceneManager.LoadScene("Nivel Casa");
    }

    // Método para salir del juego
    public void ExitGame()
    {
        // Esto funcionará en el build del juego, no en el editor de Unity
        Application.Quit();
        // Para el editor de Unity, esto es útil para ver que el botón funciona
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
