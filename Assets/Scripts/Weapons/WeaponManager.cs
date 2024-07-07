using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon currentWeapon;

    public Transform weaponHolder;

    public bool HasRoomForWeapon => this.currentWeapon == null;

    private AudioSource audioSource; // Referencia al AudioSource para sonidos de arma

    // AudioClip para el sonido de ataque
    [SerializeField] private AudioClip attackSound;

    void Start()
    {
        Weapon[] weapons = this.GetComponentsInChildren<Weapon>();
        foreach (var w in weapons)
        {
            this.PickUpWeapon(w);
        }

        // Obtener componente AudioSource del GameObject que tiene el WeaponManager
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (this.currentWeapon != null)
            {
                this.currentWeapon.Activate();

                // Reproducir sonido de ataque
                if (attackSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(attackSound);
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (this.currentWeapon != null)
            {
                this.currentWeapon.Throw();
                this.currentWeapon = null;
            }
        }
    }

    public void PickUpWeapon(Weapon weapon)
    {
        weapon.transform.position = this.weaponHolder.position;
        weapon.transform.rotation = this.weaponHolder.rotation;
        weapon.transform.SetParent(this.weaponHolder);

        this.currentWeapon = weapon;
    }
}
