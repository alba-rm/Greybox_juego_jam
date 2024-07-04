using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Health playerHealth; // Referencia al script de Health del jugador.
    public Image[] hearts; // Array de imágenes de corazones.
    public Sprite fullHeart; // Imagen de corazón lleno.
    public Sprite emptyHeart; // Imagen de corazón vacío.

    void Update()
    {
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

}
