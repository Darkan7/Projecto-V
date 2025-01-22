using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class PruebaMapa : MonoBehaviour
{
    private Cuadricula<celdaMovimiento> cuadricula;

    [SerializeField] Transform centerEnd = null;

    private void Start()
    {
        cuadricula = new Cuadricula<celdaMovimiento>(10, 5, 5f, Vector3.zero, (Cuadricula<celdaMovimiento> cuadr, int x, int y) => new celdaMovimiento(cuadr,x,y));

        EnemySpawn.Instance.SetSpawns(cuadricula);
        centerEnd.position = new Vector3(-5, 0, cuadricula.GetAlto() * cuadricula.GetTamCelda() / 2);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Utilidades.GetPosicionMundo();
            
            celdaMovimiento celda = cuadricula.GetCelda(pos);

            if (celda != null)
            {
                Debug.Log(celda);
                Debug.Log(Utilidades.GetCentroCelda(celda.x, celda.y, cuadricula.GetTamCelda()));
            }
        }
    }
    
    
}

public class celdaMovimiento
{
    private string texto = "";

    enum CentroCelda { Aliado, Enemigo, Vacia};

    //Cuadrícula al que pertenece esta celda y posición en ella
    public Cuadricula<celdaMovimiento> cuadricula;
    public int x;
    public int y;

    public celdaMovimiento(Cuadricula<celdaMovimiento> cuadricula, int x, int y)
    {
        this.cuadricula = cuadricula;
        this.x = x;
        this.y = y;
        texto = this.x + " - " + this.y;
    }

    public override string ToString()
    {
        return texto;
    }

}
