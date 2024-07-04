using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSController : MonoBehaviour
{
    private CharacterController _controller;
    private Transform _camera;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _playerSpeed = 5;

    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    private Health _health;

    // Referencia al WeaponManager
    private WeaponManager _weaponManager;

    // Variable para rastrear la dirección actual
    private bool _facingRight = true;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _health = GetComponent<Health>();
        _weaponManager = GetComponent<WeaponManager>(); // Asegúrate de que el WeaponManager está en el mismo GameObject
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Movement();
        Jump();
        
        // Atacar con el arma actual
        if (Input.GetButtonDown("Fire1") && _weaponManager != null)
        {
            _weaponManager.currentWeapon?.Activate();
        }
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        if (direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
            
            // Girar el personaje basado en la dirección de movimiento
            if (_horizontal > 0 && !_facingRight)
            {
                Flip();
            }
            else if (_horizontal < 0 && _facingRight)
            {
                Flip();
            }
        }
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

        if (_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }

        _playerGravity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sensorRadius);
    }
}