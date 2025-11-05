using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class LogicaPersonaje1 : MonoBehaviour
{
    public int fuerzaExtra = 0;
    //CORRER
    public int velCorrer;

    public float velocidadMovimiento = 5.0f;
    public float velocidadRotación = 200.0f;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;

    public float velocidadInicial;
    public float velocidadAgachado;

    public CapsuleCollider colParado;
    public CapsuleCollider colAgachado;
    public GameObject cabeza;
    public LogicaCabeza logicaCabeza;
    public bool estoyAgachado;

    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoDeGolpe = 10.0f;


    // Se llama a Start antes de la primera actualización de fotograma

    void Start()

    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();

        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento*0.5f;
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            transform.Rotate(0, x * Time.deltaTime * velocidadRotación, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
        }

        if (avanzoSolo)
        {
            rb.linearVelocity = transform.forward * impulsoDeGolpe;
        }

    }

    // Se llama a Update una vez por fotograma

    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Return) && puedoSaltar &&  !estoyAtacando)
        {
            anim.SetTrigger("golpeo");
            estoyAtacando = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadMovimiento = velCorrer;
            if (y > 0)
            {
                anim.SetBool("correr", true);
            }
            else
            {
                anim.SetBool("correr", false);
            }
        }
        else
        {
            anim.SetBool("correr", false);
            velocidadMovimiento = velocidadInicial;
        }

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        if (puedoSaltar)
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("salte", true);
                    rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    anim.SetBool("agachado", true);
                    velocidadMovimiento = velocidadAgachado;

                    colAgachado.enabled = true;
                    colParado.enabled = false;

                    cabeza.SetActive(true);
                    estoyAgachado = true;
                }
                else
                {
                    if (logicaCabeza.contadorDeColision <= 0)
                    {
                        anim.SetBool("agachado", false);
                        velocidadMovimiento = velocidadInicial;

                        cabeza.SetActive(false);
                        colAgachado.enabled = false;
                        colParado.enabled = true;
                        estoyAgachado = false;
                    }
                }
            }


            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            EstoyCayendo();
        }
    }

    public void EstoyCayendo()
    {
        rb.AddForce(fuerzaExtra * Physics.gravity);

        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }

    public void DejeDeGolpear()
    {
        estoyAtacando = false;
        avanzoSolo = false;
    }

    public void AvanzoSolo()  { avanzoSolo = true;}    

    public void DejoDeAvanzar()  { avanzoSolo = false;}
}