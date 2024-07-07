using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrolling,
        Chasing,
        Attacking
    }

    public State currentState;

    private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float detectionRange = 15;
    [SerializeField] private float attackRange = 5;

    private Health playerHealth;
    [SerializeField] private int damage = 1;

    [SerializeField] private float attackCooldown = 2f; // Tiempo de espera entre ataques
    private float attackTimer = 0f; // Temporizador para controlar el cooldown

    private GameController gameController;
    Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        anim = GetComponentInChildren<Animator>();

        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = player.GetComponent<Health>();
        }

        // Encuentra el GameController
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            SetRandomPoint(); // Establece el primer punto de patrulla al iniciar el juego
        }
        currentState = State.Patrolling; // Comienza patrullando
    }

    void Update()
    {
        if (player == null) return; // Salir si no hay jugador

        // Verifica si el jugador está muerto
        if (playerHealth != null && playerHealth.IsDead())
        {
            // Detener cualquier acción adicional si el jugador está muerto
            agent.isStopped = true;
            return;
        }

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                Attack();
                break;
        }
        anim.SetFloat("VelX", 0);
        anim.SetFloat("VelZ", agent.velocity.magnitude);
    }

    void Patrol()
    {
        // Si detecta al jugador, cambia a estado de persecución
        if (IsInRange(detectionRange))
        {
            currentState = State.Chasing;
            agent.isStopped = false; // Comienza a moverse hacia el jugador
            return;
        }

        // Si llega al punto de patrulla, elige un nuevo punto
        if (agent.remainingDistance < 0.5f && currentState == State.Patrolling)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        // Si el jugador está fuera del rango de detección, vuelve a patrullar
        if (!IsInRange(detectionRange))
        {
            currentState = State.Patrolling;
            agent.isStopped = true; // Detiene el movimiento del agente
            return;
        }

        // Si está en rango de ataque, cambia a estado de ataque
        if (IsInRange(attackRange))
        {
            currentState = State.Attacking;
            agent.isStopped = false; // Detiene el movimiento del agente mientras ataca
            return;
        }

        // Sigue moviéndose hacia el jugador
        agent.destination = player.position;
    }

    void Attack()
{
    // Controlar el cooldown entre ataques
    if (Time.time >= attackTimer)
    {
        // Reducir la salud del jugador
        if (playerHealth != null)
        {
            playerHealth.Damage((float)damage); // Usar el método Damage con float
        }

        // Reiniciar el temporizador de cooldown
        attackTimer = Time.time + attackCooldown;
    }
    // Aquí deberías implementar la lógica de ataque al jugador
    Debug.Log("Atacando al jugador");

    // Después de atacar, vuelve a perseguir al jugador
    currentState = State.Chasing;
}


    void SetRandomPoint()
    {
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }
    }

    bool IsInRange(float range)
    {
        return player != null && Vector3.Distance(transform.position, player.position) < range;
    }

    void OnDrawGizmos()
    {
        // Dibuja los gizmos para los puntos de patrulla, rango de detección y rango de ataque
        Gizmos.color = Color.blue;
        foreach (Transform point in patrolPoints)
        {
            Gizmos.DrawWireSphere(point.position, 1f);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnDestroy()
    {
        // Notificar al GameController que este enemigo ha sido destruido
        if (gameController != null)
        {
            gameController.EnemyKilled();
        }
    }
}
