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
        CrearNivel();
    }


    private void Update()
    {

    }
    
    public void CrearNivel()
    {
        cuadricula = new Cuadricula<celdaMovimiento>(10, 5, 5f, Vector3.zero, (Cuadricula<celdaMovimiento> cuadr, int x, int y) => new celdaMovimiento(cuadr, x, y));

        GameManager.Instance.cuadricula = cuadricula;
        EnemySpawn.Instance.SetSpawns(cuadricula);
        centerEnd.position = new Vector3(-5, 0, cuadricula.GetAlto() * cuadricula.GetTamCelda() / 2);
    }
    
}

public class celdaMovimiento
{
    private string texto = "";

    public enum InteriorCelda { Aliado, Vacia};

    //Cuadrícula al que pertenece esta celda y posición en ella
    public Cuadricula<celdaMovimiento> cuadricula;
    public int x;
    public int y;
    public InteriorCelda interiorcelda;

    public celdaMovimiento(Cuadricula<celdaMovimiento> cuadricula, int x, int y, InteriorCelda contenedorCelda = InteriorCelda.Vacia)
    {
        this.cuadricula = cuadricula;
        this.x = x;
        this.y = y;
        interiorcelda = contenedorCelda;
        texto = this.x + " - " + this.y;

    }

    public override string ToString()
    {
        return texto;
    }

}
