using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullarOrden : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    private int siguientePaso=0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso += 1;
            if(siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0;
            }
            Girar();
        }

    }
    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[siguientePaso].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<VidaJugador>().TomarDaño(5);
        }
    }
}
