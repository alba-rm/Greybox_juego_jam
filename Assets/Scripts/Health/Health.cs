using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth; // Cambiado a público para que HeartManager pueda acceder.
    public bool isPlayer; // Variable para identificar si el objeto es el jugador.

    public float percent
    {
        get { return this.currentHealth / this.maxHealth; }
    }

    protected virtual void Awake()
    {
        this.currentHealth = this.maxHealth;
    }

    public virtual void Restore(float amount)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth + amount, 0, this.maxHealth);
    }

    public virtual void Damage(float amount)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth - amount, 0, this.maxHealth);

        if (this.currentHealth == 0)
        {
            this.Die();
        }
    }

    public virtual void Die()
    {
        if (isPlayer)
        {
            SceneManager.LoadScene("Death"); // Cambia "GameOverScene" por el nombre de tu escena de Game Over
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool IsDead()
    {
        return this.currentHealth <= 0;
    }
}
