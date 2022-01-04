using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour {

    public float health;
    public float originalHealth;
    public float damage;
    public float laserDamageTaken;
    public bool isMainMenu = true;
    public bool isPlayerPlate = false;
    public Rigidbody rb;
    public Vector3 relativeVelocity;
    public ModuleActivater module;
    public ShipHealthManagement shipHealthManager;
    public SlugController slug;
    public GameManager manager;
    public Color initialMaterialColor;
    public Color tempColor;
    public Renderer plateRenderer;

    // Use this for initialization
    void Start ()
    {
        if(module == null)
        {
            this.initializePlate();
        }
        //rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (laserDamageTaken != 0)
        {
            damage = laserDamageTaken * Time.deltaTime;
            health -= damage;
            shipHealthManager.addDamage(damage);
            damage = 0;
            laserDamageTaken = 0;
        }
        if (health <= 0)
        {
            if (module != null)
            {
                module.destroyModule();
            }
            //GetComponent<Renderer>().enabled = true;
        }
        //if (health/originalHealth < 0.5 && isPlayerPlate)
        //{
        //    tempColor = new Color(initialMaterialColor.r, initialMaterialColor.g, initialMaterialColor.b, 1 - 2 * health / originalHealth);
        //    plateRenderer.material.color = tempColor;
        //}
        if (isPlayerPlate)
        {
            tempColor = new Color(initialMaterialColor.r, initialMaterialColor.g, initialMaterialColor.b, Mathf.Pow((1 - health / originalHealth),2));
            plateRenderer.material.color = tempColor;
        }
        else if (!isMainMenu && isPlayerPlate)
        {
            tempColor = new Color(initialMaterialColor.r, initialMaterialColor.g, initialMaterialColor.b, 0);
            plateRenderer.material.color = tempColor;
        }
        if (health <= 0)
        {
            tempColor = new Color(initialMaterialColor.r, initialMaterialColor.g, initialMaterialColor.b, 1);
            plateRenderer.material.color = tempColor;
        }
    }

    public void slugHitDamage(Vector3 velocity, float slugDamage)
    {
        damage = manager.collisionCalculator(rb, velocity, slugDamage);
        health -= damage;
        shipHealthManager.addDamage(damage);
        damage = 0;
    }

    public void initializePlate()
    {
        if (FindObjectOfType<MainMenu>() == null)
        {
            isMainMenu = false;
        }
        if (this.GetComponentInParent<PlayerManagement>() == null)
        {
            isPlayerPlate = false;
        }
        else
        {
            isPlayerPlate = true;
        }
        initialMaterialColor = this.GetComponent<Renderer>().material.color;
        plateRenderer = this.GetComponentInChildren<Renderer>();
        module = GetComponentInChildren<ModuleActivater>();
        shipHealthManager = GetComponentInParent<ShipHealthManagement>();
        rb = shipHealthManager.GetComponentInParent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        health = manager.plateHealth(this.gameObject.tag.ToString()) * 5000;
        originalHealth = health;
        Destroy(this.GetComponent<Rigidbody>());
    }
}
