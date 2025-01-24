using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Cuadricula<celdaMovimiento> cuadricula;
    public Transform centerEnd = null;

    public bool noEnemySpawn = false;
    [HideInInspector] public bool isSpawning = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Utilidades.GetPosicionMundo();

            celdaMovimiento celda = null;

            if (pos.x > 0 && pos.x <= cuadricula.GetAncho() * cuadricula.GetTamCelda() && pos.z > 0 && pos.z <= cuadricula.GetAlto() * cuadricula.GetTamCelda())
            {
                celda = cuadricula.GetCelda(pos);
            }

            if (celda is null) return; //Se ha hecho click fuera del mapa

            if (isSpawning && celda.interiorcelda == celdaMovimiento.InteriorCelda.Vacia) 
            {
                isSpawning = false;
                SpawnAliado.Instance.SetSpawn(cuadricula, celda);
            }

            Debug.Log(celda.interiorcelda);
            
        }
    }

}
