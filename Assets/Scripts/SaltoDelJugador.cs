using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoDelJugador : MonoBehaviour
{
    [Header("Referencias")]

    private Rigidbody2D rbd2D;

    [Header("Salto")]

    [SerializeField] private float fuerzaSalto;

    [SerializeField] private Transform controladorSuelo;

    [SerializeField] private Vector2 dimensionesCaja;

    [SerializeField] private LayerMask queEsSuelo;

    private bool enSuelo;

    private bool saltar;

    [Header("SaltoRegulable")]

    [Range(0, 1)] [SerializeField] private float multiplicadorCancelarSalto;

    [SerializeField] private float multiplicadorGravedad;

    private float escalaGravedad;

    private bool botonSaltoArriba = true;

    void Start()
    {
        rbd2D = GetComponent<Rigidbody2D>();

        escalaGravedad = rbd2D.gravityScale;
    }

  
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            saltar = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            //BotonSaltoAarriba
            BotonSaltoArriba();
        }

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0, queEsSuelo);
    }

    private void FixedUpdate()
    {
        if (saltar && botonSaltoArriba && enSuelo)
        {
            Saltar();
        }

        if (rbd2D.velocity.y < 0 && !enSuelo)
        {
            rbd2D.gravityScale = escalaGravedad * multiplicadorGravedad;
        }
        else
        {
            rbd2D.gravityScale = escalaGravedad;
        }

        saltar = false;

    }

    private void Saltar()
    {
        rbd2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        enSuelo = false;
        saltar = false;
        botonSaltoArriba = false;
    }

    private void BotonSaltoArriba()
    {
        if (rbd2D.velocity.y > 0)
        {
            rbd2D.AddForce(Vector2.down * rbd2D.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
        }

        botonSaltoArriba = true;
        saltar = false; 

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
