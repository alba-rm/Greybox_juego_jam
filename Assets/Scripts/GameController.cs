using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject endGameCanvas; // Asigna tu Canvas desde el Inspector
    private int enemyCount;
    [SerializeField] private float delayBeforeEndGameCanvas = 0.5f; // Tiempo de espera antes de mostrar el Canvas

    void Start()
    {
        // Encuentra todos los enemigos al inicio del juego
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        endGameCanvas.SetActive(false); // Asegúrate de que el Canvas está desactivado al inicio
    }

    public void EnemyKilled()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            StartCoroutine(ShowEndGameCanvasAfterDelay());
        }
    }

    private IEnumerator ShowEndGameCanvasAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeEndGameCanvas);
        endGameCanvas.SetActive(true); // Muestra el Canvas de fin de juego
    }
}
