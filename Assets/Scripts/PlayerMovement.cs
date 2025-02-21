using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed = 15;
    public float lookSpeed = 0;
    public float invulnerabilityTime = 1.5f; // Tiempo de invulnerabilidad
    public float dodgeCooldown = 1.5f; // Cooldown entre esquivas
    
    public float HealthPoints = 10;

    public bool isDodging = false;
    private bool canDodge = true;


    public Transform aimTarget;
    public GameObject dollyCart;

    public CinemachineVirtualCamera clampCam;

    public HealthBar healthBar;
    public GameObject dodgeEffect;

    public GameObject pauseMenu;
    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(HealthPoints);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if(isPaused) return;

        healthBar.SetHealth(HealthPoints);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if (HealthPoints > 0)
        {
            if (!isDodging)
            {
                LocalMove(h, v, moveSpeed);
                RotationLook(h, v, lookSpeed);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDodge)
            {
                StartCoroutine(Dodge());

            }
        }

        if (HealthPoints <= 0)
        {
            StopDollyCart();
        }

        void StopDollyCart()
        {
            CinemachineDollyCart cart = dollyCart.GetComponent<CinemachineDollyCart>();
            if (cart != null)
            {
                cart.m_Speed = 0; // Detiene el Dolly Cart
            }
        }

        if (HealthPoints <= 0)
        {
            StopShooting();
        }

        void StopShooting()
        {
            PlayerShoot playerShoot = GetComponentInChildren<PlayerShoot>();
            if (playerShoot != null)
            {
                playerShoot.enabled = false; // Desactiva el script de disparo
            }
        }

       
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pausa el tiempo
            pauseMenu.SetActive(true); // Activa el men� de pausa
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            pauseMenu.SetActive(false); // Desactiva el men� de pausa
        }
    }

    void LocalMove(float x, float y, float speed)
    {
        // Mant�n la posici�n Z constante
        //Vector3 currentPosition = transform.localPosition;
        //Vector3 movement = new Vector3(x, y, 0) * speed * Time.deltaTime;

        // Suma el movimiento �nicamente a X e Y
        transform.localPosition += new Vector3(x, y, 0 ) * speed * Time.deltaTime;

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        pos.z = 10f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook (float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean (Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

    IEnumerator Dodge()
    {
        isDodging = true;
        canDodge = false;
        animator.SetBool("isDodging", true); // Activa la animaci�n de esquiva

        // Activa el objeto hijo al esquivar
        if (dodgeEffect != null)
        {
            dodgeEffect.SetActive(true);
        }

        yield return new WaitForSeconds(invulnerabilityTime);

        animator.SetBool("isDodging", false); // Desactiva la animaci�n
        isDodging = false;

        // Desactiva el objeto hijo despu�s de la esquiva
        if (dodgeEffect != null)
        {
            dodgeEffect.SetActive(false);
        }

        yield return new WaitForSeconds(dodgeCooldown - invulnerabilityTime);
        canDodge = true;
    }

   
}
