using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance { get; private set; }
    // Creating a Dynamic List of Scripts of EnemyAI
    private int _numberOfEnemies = default;

    private void Awake()
    {

        //Por buenas practicas, revisamos que la instancia sea nula y no halla duplicados, NO DEBERIA ejecutarse este bloque, pero
        //lo hara si algun error ocurre
        if (Instance != null)
        {
            //Mostramos el error en la consola
            Debug.LogError("There's more than one UnitsManager " + transform + " - " + Instance);
            //Prevenimos la existencia del duplicado destryllendo el objeto
            Destroy(gameObject);
            //culmona con la ejecucion del metodo
            return;
        }
        //Instanciamos esta clase con las propiedades ya establecidas
        Instance = this;

        // Cleans the List creating a new List every Instance
        _numberOfEnemies = 0;
    }

    private void Start()
    {
        // ****** Subscription to the Event ******
        // In every EnemyAI script when the Event OnAnyUnitSpawned is triggered the 
        // EnemyAI_OnAnyUnitSpawned event will be executed
        EnemyAI.OnAnyUnitSpawned += EnemyAI_OnAnyUnitSpawned;
        TargetLife.OnAnyEnemyDead += TargetLife_OnAnyEnemyDead;
    }

    private void TargetLife_OnAnyEnemyDead(object sender, EventArgs e)
    {
        Debug.Log("Listened to OnEnemyDead");
        Debug.Log(GetEnemyUnitsRemaining());
        _numberOfEnemies--;
        Debug.Log(GetEnemyUnitsRemaining() + "-->");
        // Access to the UnitsManager and when The List of Units is 0
        // Loads the Victory Scene
        if (GetEnemyUnitsRemaining() == 0)
        {
            Debug.Log("We Won");
            // Load Victory Scene
            SceneManager.LoadScene("Victory");
        }
    }

    // sender on the Event is associated to every enemy
    private void EnemyAI_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        // Each time an Enemy spawns is added to the List
        _numberOfEnemies++;
    }

    // Method that checks how many Enemies are left
    public int GetEnemyUnitsRemaining()
    {
        return _numberOfEnemies;        
    }
}
