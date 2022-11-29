using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance { get; private set; }
    // Creating a Dynamic List of Scripts of EnemyAI
    private List<EnemyAI> enemyUnitList;

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
        enemyUnitList = new List<EnemyAI>();
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
        // Access to the sender
        EnemyAI enemyAI = sender as EnemyAI;
        // Removes the Dead Enemies from the List
        enemyUnitList.Remove(enemyAI);

        // Access to the UnitsManager and when The List of Units is 0
        // Loads the Victory Scene
        if (GetEnemyUnitsRemaining() == 0)
        {
            // Load Victory Scene
            SceneManager.LoadScene("Victory");
        }
    }

    // sender on the Event is associated to every enemy
    private void EnemyAI_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        // The EnemyAI script is stored in the varible and access to the sender in form of the EnemyAI script
        EnemyAI enemyAI = sender as EnemyAI;
        // Each time an Enemy spawns is added to the List
        enemyUnitList.Add(enemyAI);
    }

    // Method that checks how many Enemies are left
    public int GetEnemyUnitsRemaining()
    {
        // The variable is initialized
        int enemyUnitsRemaining = 0;
        // Cycle to identify the EnemyAI scripts in the List
        foreach (EnemyAI enemyUnit in enemyUnitList)
        {
            enemyUnitsRemaining++;
        }
        // Returns the value of the Units Left
        return enemyUnitsRemaining;
    }
}
