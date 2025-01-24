using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn Instance { get; private set; }

    public Transform[] spawns;
    public GameObject[] enemies;

    private bool spawnCoroutine = true;

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

    public void SetSpawns(Cuadricula<celdaMovimiento> cuadricula)
    {
        if (GameManager.Instance.noEnemySpawn) return;

        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i].transform.position = new Vector3(cuadricula.GetAncho() * cuadricula.GetTamCelda() + 10, 0 ,cuadricula.GetTamCelda()/2 + cuadricula.GetTamCelda() * i);
        }
        StartCoroutine(TimerSpawnEnemies());
    }

    IEnumerator TimerSpawnEnemies()
    {
        while (spawnCoroutine) 
        {
            Instantiate(enemies[0], spawns[Random.Range(0,5)].transform.position, spawns[0].transform.rotation);
            yield return new WaitForSeconds(3f);
        }
    }
}
