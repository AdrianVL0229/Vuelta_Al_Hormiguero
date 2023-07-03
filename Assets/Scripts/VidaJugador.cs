using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VidaJugador : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private BarraDeVida barraDeVida;
    public event EventHandler MuerteJugador;
    private Animator animator;
    private Rigidbody2D rb2D;

    private void Start()
    {
        vida = maximoVida;
        barraDeVida.InicializarBarraDeVida(vida);
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }


    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraDeVida.CambiarVidaActual(vida);
        if (vida <=0)
        {
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
       
    }

   

    public void Curar(float curacion)
    {
        barraDeVida.CambiarVidaActual(vida);
        if ((vida + curacion) > maximoVida)
        {
            vida = maximoVida;
        }
        else
        {
            vida += curacion;
        }
    }

}
