using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrahedronInitializer : MonoBehaviour {

    public GameManager manager;
    public PlayerManagement playerManager;
    public PlayerMovement playerMovement;
    public ModuleActivater module1;
    public ModuleActivater module2;
    public ModuleActivater module3;
    public ModuleActivater module4;
    public ModuleActivater[] ModuleArray;
    public AIController[] enemies;
    public string[] moduleTypeArray;
    public int moduleNumber;

    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManagement>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        ModuleArray = new ModuleActivater[] { module1, module2, module3, module4 };
        moduleTypeArray = playerManager.moduleTypeArray;

        moduleNumber = 0;
        foreach(ModuleActivater module in ModuleArray)
        {
            module.type = moduleTypeArray[moduleNumber];
            moduleNumber++;
        }
        playerManager.modules = ModuleArray;
        playerManager.refreshEnemyList();

        playerMovement.rb.mass = manager.tetrahedronMass;
        playerMovement.force = manager.defaultForce * 1.1f;
        playerMovement.torqueForce = manager.defaultTorque;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate()
    {
        if (playerMovement.fire)
        {
            foreach (ModuleActivater module in ModuleArray)
            {
                module.activateModule();
            }
        }
    }
}
