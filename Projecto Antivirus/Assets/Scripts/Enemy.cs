using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using DG.Tweening;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    [Range(0,5)]
    [SerializeField] private int speed = 3;
    private int _speed = 0;

    [SerializeField] private BoxCollider enemyDetection = null;

    private void Awake()
    {
        if (enemyDetection == null) enemyDetection = GetComponentInChildren<BoxCollider>();
        _speed = speed;
    }
    void Start()
    {

    }

    void Update()
    {
        if (_speed != 0) Moveleft();

        celdaMovimiento celda = GameManager.Instance.cuadricula.GetCelda(transform.position);

        /*if (celda == null) return;
        if (celda.interiorcelda == celdaMovimiento.InteriorCelda.Aliado) return; //Interactuar con el aliado
        */


    }

    void Moveleft()
    {
        _speed = speed;
        Vector3 movementInput = Vector3.zero;
        movementInput.x = -1;
        transform.position += movementInput.normalized * _speed * Time.deltaTime;
    }

    void MoveCenter()
    {
        transform.DOComplete();
        transform.DOMove(GameManager.Instance.centerEnd.position, 6f).SetEase(Ease.Linear).onComplete = Moveleft;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "End") 
        {
            _speed = 0;
            MoveCenter();
        }
    }
}
