using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAliado : MonoBehaviour
{
    public static SpawnAliado Instance { get; private set; }

    [SerializeField] private GameObject[] aliados;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ButtonSpawn()
    {
        GameManager.Instance.isSpawning = true;

    }
    public void SetSpawn(Cuadricula<celdaMovimiento> cuadricula, celdaMovimiento celda)
    {
        Instantiate(aliados[0], Utilidades.GetCentroCelda(celda.x, celda.y, cuadricula.GetTamCelda()), aliados[0].transform.rotation);
        celda.interiorcelda = celdaMovimiento.InteriorCelda.Aliado;

    }
}
