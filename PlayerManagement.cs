using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class PlayerManagement : MonoBehaviour {

    public SphereCollider sphere;
    public float health = 10;
    public float score = 0;
    public float totalShieldHealth;
    public float currentShieldHealth;
    public float enemyTotalShieldHealth;
    public float enemyCurrentShieldHealth;
    public float currentAdjustedFireDelay = 1;
    private int moduleNumber;
    public Vector3 directionToEnemy;
    public Camera enemyCamera;
    public GameManager manager;
    public PlayerMovement playerMovement;
    public GameObject railgunLeadReticle;
    public GameObject machinegunLeadReticle;
    public GameObject EMACLeadReticle;
    public GameObject fixedRailgunLeadReticle;
    public GameObject fixedMachinegunLeadReticle;
    public GameObject fixedEMACLeadReticle;
    public GameObject enemyIndicator;
    public GameObject selfObject;
    public GameObject ship;
    public GameObject autofire;
    public ShipHealthManagement shipHealthManager;
    public ShipHealthManagement enemyShipHealthManager;
    public ModuleActivater[] modules;
    public PlateManager[] plates;
    public AIController[] enemies;
    public AIController selectedEnemy;
    public string[] moduleTypeArray;
    public string shipType;
    public ShieldController[] shields;
    public ShieldController[] enemyShields;
    public AudioSource healthPickup;
    public Slider healthSlider;
    public Slider shieldHealthSlider;
    public Slider enemyShieldHealthSlider;
    public Slider enemyHealthSlider;
    public Slider numberOfEnemiesSlider;
    public Text healthText;
    public Text shieldHealthText;
    public Text enemyShieldHealthText;
    public Text enemyHealthText;
    public Text numberOfEnemies;
    public Text scoreText;
    public Text boundaryWarning;
    public Text motionDrag;
    public Text rotationDrag;
    public Text velocity;
    public Text rotationLock;
    public Text fineControl;
    public Text distanceIndicator;
    public Text raceDistanceIndicator;

    // Use this for initialization
    void Start ()
    {
        //Application.targetFrameRate = 60;
    }

    public void initializeAtStart()
    {
        manager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        PostProcessingBehaviour PPP = FindObjectOfType<PostProcessingBehaviour>();
        this.initializeShip();
        this.setModuleTypes();
        this.setPlateTypes();
        this.initializeModules();
        this.refreshEnemyList();
        this.selectFowardmostEnemy();
        this.setFireRates();
        playerMovement.initializePM();
        sphere.enabled = true;
        sphere.enabled = false;
        if (manager.gameType == "Race")
        {
            Destroy(enemyHealthSlider);
            enemyHealthText.enabled = false;
            numberOfEnemies.enabled = false;
            Destroy(numberOfEnemiesSlider);
            //if (PPP != null)
            //{
            //    PPP.profile.bloom.enabled = false;
            //    Debug.Log("disabled bloom");
            //}
            //else
            //{
            //    Debug.Log("didn't find PPP");
            //}

        }
        shields = this.GetComponentsInChildren<ShieldController>();
        if (shields.Length == 0)
        {
            shieldHealthSlider.gameObject.SetActive(false);
            shieldHealthText.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("location") == "mirrorField")
        {
            this.transform.position = new Vector3(0, 0, -175);
        }
        GameObject particleEffect = Instantiate(manager.spawnEffect, this.transform.position, this.transform.rotation);
        Destroy(particleEffect, 3.5f);
        float roundsPerSecond = 0;
        float roundLimit = 75;
        foreach (ModuleActivater mod in modules)
        {
            if (mod.type == "TurretedEMAC" || mod.type == "TurretedMachinegun" || mod.type == "TurretedRailgun" || mod.type == "FixedEMAC" || mod.type == "FixedMachinegun" || mod.type == "FixedRailgun")
            {
                roundsPerSecond += 1 / mod.shotDelay;
            }
        }
        if (roundsPerSecond > roundLimit)
        {
            foreach (ModuleActivater mod in modules)
            {
                if (mod.type == "TurretedEMAC" || mod.type == "TurretedMachinegun" || mod.type == "TurretedRailgun" || mod.type == "FixedEMAC" || mod.type == "FixedMachinegun" || mod.type == "FixedRailgun")
                {
                    mod.shotDelay *= roundsPerSecond / roundLimit;
                    mod.baseDamage *= roundsPerSecond / roundLimit;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        if (!manager.gameHasEnded)
        {
            if (manager.averageFramerate < manager.goalFramerate && (Time.time - manager.lastFireRateAdjustment > manager.fireRateAjustmentDelay))
            {
                manager.lastFireRateAdjustment = Time.time;
                this.setFireRates();
                foreach (AIController enemy in enemies)
                {
                    enemy.setFireRates();
                }
            }
            health = shipHealthManager.shipHealth;
            if (health <= 0)
            {
                playerMovement.isPlayable = false;
                manager.EndGame();
                Destroy(ship);
                GameObject particleEffect = Instantiate(manager.shipDestructionEffect, this.transform.position, this.transform.rotation);
                Destroy(particleEffect, 3.5f);
            }
            if (plates != null)
            {
                for (int i = 0; i <= plates.Length; i++)
                {
                    if (i == plates.Length)
                    {
                        playerMovement.isPlayable = false;
                        manager.EndGame();
                        Destroy(ship);
                        GameObject particleEffect = Instantiate(manager.shipDestructionEffect, this.transform.position, this.transform.rotation);
                        Destroy(particleEffect, 3.5f);
                    }
                    if (plates[i].health > 0)
                    {
                        break;
                    }
                }
            }
            numberOfEnemiesSlider.value = enemies.Length;
            numberOfEnemies.text = "Enemies remaining: " + enemies.Length.ToString();
            healthText.text = "Health: " + shipHealthManager.shipHealth.ToString("0");
            if (health < 0)
            {
                healthText.text = "Health: 0";
            }
            healthSlider.value = shipHealthManager.shipHealth / shipHealthManager.originalShipHealth;
            if (selectedEnemy != null)
            {
                enemyShields = this.selectedEnemy.GetComponentsInChildren<ShieldController>();
                if (enemyShields != null)
                {

                    enemyCurrentShieldHealth = 0;
                    enemyTotalShieldHealth = 0;
                    foreach (ShieldController shield in enemyShields)
                    {
                        if (shield != null)
                        {
                            enemyTotalShieldHealth += shield.maxShieldHealth;
                            enemyCurrentShieldHealth += shield.shieldHealth;
                        }
                    }
                    enemyShieldHealthText.gameObject.SetActive(true);
                    enemyShieldHealthSlider.gameObject.SetActive(true);
                    enemyShieldHealthSlider.maxValue = enemyTotalShieldHealth;
                    enemyShieldHealthSlider.value = enemyCurrentShieldHealth;
                    enemyShieldHealthText.text = "Shield Health: " + enemyCurrentShieldHealth.ToString("0");
                }
                if (enemyShields.Length == 0)
                {
                    enemyShieldHealthText.gameObject.SetActive(false);
                    enemyShieldHealthSlider.gameObject.SetActive(false);
                }
            }
            if (shields != null)
            {
                currentShieldHealth = 0;
                totalShieldHealth = 0;
                foreach(ShieldController shield in shields)
                {
                    if (shield != null)
                    {
                        totalShieldHealth += shield.maxShieldHealth;
                        currentShieldHealth += shield.shieldHealth;
                    }
                }
                shieldHealthSlider.maxValue = totalShieldHealth;
                shieldHealthSlider.value = currentShieldHealth;
                shieldHealthText.text = "Shield Health: " + currentShieldHealth.ToString("0");
            }
            if (manager.gameType != "Race")
            {
                if (selectedEnemy != null)
                {
                    if (enemyShipHealthManager == null)
                    {
                        enemyShipHealthManager = selectedEnemy.gameObject.GetComponentInChildren<ShipHealthManagement>();
                    }
                    if (enemyShipHealthManager != null)
                    {
                        if (enemyShipHealthManager.shipHealth > 0)
                        {
                            enemyHealthText.text = "Enemy Health: " + enemyShipHealthManager.shipHealth.ToString("0");
                            enemyHealthSlider.value = enemyShipHealthManager.shipHealth / enemyShipHealthManager.originalShipHealth;
                        }
                        else
                        {
                            enemyHealthText.text = "Enemy Health: " + 0.ToString();
                            enemyHealthSlider.value = 0;
                        }
                    }

                }

                if (enemies.Length == 0)
                {
                    this.refreshEnemyList();
                }
                else
                {
                    if (selectedEnemy == null)
                    {
                        this.refreshEnemyList();
                    }
                    else
                    {
                        enemyIndicator.transform.position = selectedEnemy.transform.position;
                        railgunLeadReticle.transform.position = manager.getInterceptPoint(playerMovement.rb.position, playerMovement.rb.velocity, manager.railgunSpeed, enemyIndicator.transform.position, selectedEnemy.rb.velocity);
                        machinegunLeadReticle.transform.position = manager.getInterceptPoint(playerMovement.rb.position, playerMovement.rb.velocity, manager.machinegunSpeed, enemyIndicator.transform.position, selectedEnemy.rb.velocity);
                        EMACLeadReticle.transform.position = manager.getInterceptPoint(playerMovement.rb.position, playerMovement.rb.velocity, manager.EMACSpeed, enemyIndicator.transform.position, selectedEnemy.rb.velocity);
                        fixedRailgunLeadReticle.transform.position = manager.getInterceptPoint(playerMovement.rb.position, playerMovement.rb.velocity, manager.fixedRailgunSpeed, enemyIndicator.transform.position, selectedEnemy.rb.velocity);
                        fixedMachinegunLeadReticle.transform.position = manager.getInterceptPoint(playerMovement.rb.position, playerMovement.rb.velocity, manager.fixedMachinegunSpeed, enemyIndicator.transform.position, selectedEnemy.rb.velocity);
                        fixedEMACLeadReticle.transform.position = manager.getInterceptPoint(playerMovement.rb.position, playerMovement.rb.velocity, manager.fixedEMACSpeed, enemyIndicator.transform.position, selectedEnemy.rb.velocity);
                    }
                }
                if (selectedEnemy != null)
                {
                    distanceIndicator.text = "Enemy Distance: " + (Vector3.Distance(playerMovement.rb.position, selectedEnemy.transform.position) * 10).ToString("0");
                }
            }
            if (playerMovement.fire || playerMovement.autofire)
            {
                foreach (ModuleActivater module in modules)
                {
                    if (module != null)
                    {
                        module.activateModule();
                    }
                }
            }
            else
            {
                foreach (ModuleActivater module in modules)
                {
                    if (module != null && module.laserSoundEffect != null)
                    {
                        module.laserSoundEffect.Pause();
                    }
                }
            }
            if (playerMovement.autofire)
            {
                autofire.SetActive(true);
            }
            else
            {
                autofire.SetActive(false);
            }
        }
        if (manager.gameType == "Race")
        {
            raceDistanceIndicator.text = "Distance from center: " + (Vector3.Distance(this.transform.position, new Vector3(0, 0, 0)) * 10).ToString("0") + "/10000";
        }

        if (selectedEnemy != null)
        {
            enemyCamera.gameObject.SetActive(true);
            directionToEnemy = this.transform.position - selectedEnemy.transform.position;
            enemyCamera.transform.position = 3 * directionToEnemy.normalized * manager.shipScaleFactor + selectedEnemy.transform.position;
            enemyCamera.transform.rotation = Quaternion.LookRotation(-directionToEnemy);
        }
        else
        {
            enemyCamera.gameObject.SetActive(false);
        }
    }

    public void setModuleTypes()
    {
        modules = selfObject.transform.GetComponentsInChildren<ModuleActivater>();
        moduleNumber = 0;
        foreach (ModuleActivater module in modules)
        {
            module.moduleNumber = moduleNumber;
            module.type = moduleTypeArray[moduleNumber];
            module.ammoLayer = 9;
            module.gameObject.layer = 15;
            moduleNumber++;
            module.isEnemyModule = false;
        }
    }

    public void setPlateTypes()
    {
        plates = selfObject.transform.GetComponentsInChildren<PlateManager>();
        foreach (PlateManager plate in plates)
        {
            plate.gameObject.layer = 15;
            plate.initializePlate();
            //plate.GetComponent<Renderer>().enabled = false;
        }
    }

    public void initializeModules()
    {
        modules = selfObject.transform.GetComponentsInChildren<ModuleActivater>();
        foreach (ModuleActivater module in modules)
        {
            module.openFire = true;
            module.moduleInitializer();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if ((collision.collider.name.ToString() == "Turret Railgun Slug(Clone)") || (collision.collider.name.ToString() == "Turret Machinegun Slug(Clone)") || (collision.collider.name.ToString() == "Turret EMAC Slug(Clone)"))
    //    //{
    //    //    Debug.Log("Self hit");
    //    //}
    //    //else
    //    //{
    //    //    health -= 1f;
    //    //}
    //}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "HealthBoost")
        {
            healthPickup.time = 0;
            healthPickup.Play();
            Destroy(collision.collider.gameObject);
            foreach(PlateManager plate in plates)
            {
                if (plate != null)
                {
                    if (plate.health > plate.originalHealth * 0.9f)
                    {
                        shipHealthManager.addDamage(-(plate.originalHealth - plate.health));
                        plate.health = plate.originalHealth;
                    }
                    else
                    {
                        shipHealthManager.addDamage(-plate.originalHealth * 0.1f);
                        plate.health += plate.originalHealth * 0.1f;
                    }
                }
            }
            foreach(ModuleActivater module in modules)
            {
                if (module != null)
                {
                    if (module.health > module.originalHealth * 0.9f)
                    {
                        shipHealthManager.addDamage(-(module.originalHealth - module.originalHealth));
                        module.health = module.originalHealth;
                    }
                    else
                    {
                        shipHealthManager.addDamage(-module.originalHealth * 0.1f);
                        module.health += module.originalHealth * 0.1f;
                    }
                }
            }
        }
    }

    public void refreshEnemyList()
    {
        enemies = FindObjectsOfType<AIController>();
        foreach (ModuleActivater module in modules)
        {
            module.enemies = enemies;
            module.enemySighted = false;
        }
        if (enemies.Length > 0)
        {
            this.selectFowardmostEnemy();
        }
    }

    public void selectFowardmostEnemy()
    {
        foreach (AIController enemy in enemies)
        {
            if (selectedEnemy == null)
            {
                selectedEnemy = enemy;
            }
            else if (Vector3.Angle((enemy.rb.position - playerMovement.rb.position), playerMovement.playerCamera.transform.forward) <= Vector3.Angle((selectedEnemy.rb.position - playerMovement.rb.position), playerMovement.playerCamera.transform.forward))
            {
                selectedEnemy = enemy;
            }
        }
        if (selectedEnemy != null)
        {
            enemyShipHealthManager = selectedEnemy.gameObject.GetComponentInChildren<ShipHealthManagement>();
            manager.shipTypeIndicator.text = manager.convertShipTypeToReadable(selectedEnemy.shipType);
        }
    }

    public void setFireRates()
    {
        float adjustedFireRate = manager.fireDelay();
        adjustedFireRate = Mathf.Min(adjustedFireRate, 1.5f);
        if (manager.fireRateModifier * adjustedFireRate < 50)
        {
            foreach (ModuleActivater mod in modules)
            {
                if (mod.type == "TurretedEMAC" || mod.type == "TurretedMachinegun" || mod.type == "TurretedRailgun" || mod.type == "FixedEMAC" || mod.type == "FixedMachinegun" || mod.type == "FixedRailgun")
                {
                    mod.baseDamage *= adjustedFireRate;
                    mod.shotDelay *= adjustedFireRate;
                }
            }
            manager.fireRateModifier *= adjustedFireRate;
        }
    }

    public void initializeShip()
    {
        if (shipType == "tetrahedron")
        {
            ship = Instantiate(manager.tetrahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.tetrahedronMass;
            playerMovement.force = manager.defaultForce;
            playerMovement.torqueForce = manager.defaultTorque;
        }
        else if (shipType == "cube")
        {
            ship = Instantiate(manager.cubeShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.cubeMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.cubeVSA) * manager.tetrahedronVSA / manager.cubeVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.cubeVSA) * manager.tetrahedronVSA / manager.cubeVSA;
        }
        else if (shipType == "octahedron")
        {
            ship = Instantiate(manager.octahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.octahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.octahedronVSA) * manager.tetrahedronVSA / manager.octahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.octahedronVSA) * manager.tetrahedronVSA / manager.octahedronVSA;
        }
        else if (shipType == "dodecahedron")
        {
            ship = Instantiate(manager.dodecahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.dodecahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.dodecahedronVSA) * manager.tetrahedronVSA / manager.dodecahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.dodecahedronVSA) * manager.tetrahedronVSA / manager.dodecahedronVSA;
        }
        else if (shipType == "icosahedron")
        {
            ship = Instantiate(manager.icosahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.icosahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.icosahedronVSA) * manager.tetrahedronVSA / manager.icosahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.icosahedronVSA) * manager.tetrahedronVSA / manager.icosahedronVSA;
        }
        else if (shipType == "truncatedTetrahedron")
        {
            ship = Instantiate(manager.truncatedTetrahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedTetrahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedTetrahedronVSA) * manager.tetrahedronVSA / manager.truncatedTetrahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedTetrahedronVSA) * manager.tetrahedronVSA / manager.truncatedTetrahedronVSA;
        }
        else if (shipType == "cuboctahedron")
        {
            ship = Instantiate(manager.cuboctahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.cuboctahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.cuboctahedronVSA) * manager.tetrahedronVSA / manager.cuboctahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.cuboctahedronVSA) * manager.tetrahedronVSA / manager.cuboctahedronVSA;
        }
        else if (shipType == "truncatedCube")
        {
            ship = Instantiate(manager.truncatedCubeShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedCubeMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedCubeVSA) * manager.tetrahedronVSA / manager.truncatedCubeVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedCubeVSA) * manager.tetrahedronVSA / manager.truncatedCubeVSA;
        }
        else if (shipType == "truncatedOctahedron")
        {
            ship = Instantiate(manager.truncatedOctahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedOctahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedOctahedronVSA) * manager.tetrahedronVSA / manager.truncatedOctahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedOctahedronVSA) * manager.tetrahedronVSA / manager.truncatedOctahedronVSA;
        }
        else if (shipType == "rhombicuboctahedron")
        {
            ship = Instantiate(manager.rhombicuboctahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.rhombicuboctahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.rhombicuboctahedronVSA) * manager.tetrahedronVSA / manager.rhombicuboctahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.rhombicuboctahedronVSA) * manager.tetrahedronVSA / manager.rhombicuboctahedronVSA;
        }
        else if (shipType == "truncatedCuboctahedron")
        {
            ship = Instantiate(manager.truncatedCuboctahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedCuboctahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA) * manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA) * manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA;
        }
        else if (shipType == "snubCube")
        {
            ship = Instantiate(manager.snubCubeShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.snubCubeMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.snubCubeVSA) * manager.tetrahedronVSA / manager.snubCubeVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.snubCubeVSA) * manager.tetrahedronVSA / manager.snubCubeVSA;
        }
        else if (shipType == "icosidodecahedron")
        {
            ship = Instantiate(manager.icosidodecahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.icosidodecahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.icosidodecahedronVSA) * manager.tetrahedronVSA / manager.icosidodecahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.icosidodecahedronVSA) * manager.tetrahedronVSA / manager.icosidodecahedronVSA;
        }
        else if (shipType == "truncatedDodecahedron")
        {
            ship = Instantiate(manager.truncatedDodecahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedDodecahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedDodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedDodecahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedDodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedDodecahedronVSA;
        }
        else if (shipType == "truncatedIcosahedron")
        {
            ship = Instantiate(manager.truncatedIcosahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedIcosahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedIcosahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedIcosahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosahedronVSA;
        }
        else if (shipType == "rhombicosidodecahedron")
        {
            ship = Instantiate(manager.rhombicosidodecahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.rhombicosidodecahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA) * manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA) * manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA;
        }
        else if (shipType == "truncatedIcosidodecahedron")
        {
            ship = Instantiate(manager.truncatedIcosidodecahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.truncatedIcosidodecahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA;
        }
        else if (shipType == "snubDodecahedron")
        {
            ship = Instantiate(manager.snubDodecahedronShip);
            ship.transform.SetParent(playerMovement.rb.transform, true);
            playerMovement.rb.mass = manager.snubDodecahedronMass;
            playerMovement.force = manager.defaultForce * (manager.tetrahedronVSA / manager.snubDodecahedronVSA) * manager.tetrahedronVSA / manager.snubDodecahedronVSA;
            playerMovement.torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.snubDodecahedronVSA) * manager.tetrahedronVSA / manager.snubDodecahedronVSA;
        }
        //increase size of ship
        if (manager.main == null)
        {
            ship.transform.localScale = new Vector3(manager.shipScaleFactor, manager.shipScaleFactor, manager.shipScaleFactor);
            playerMovement.torqueForce *= manager.shipScaleFactor * manager.shipScaleFactor;
        }
        playerMovement.originalForce = playerMovement.force;
        playerMovement.originalTorqueForce = playerMovement.torqueForce;
        shipHealthManager = this.gameObject.GetComponentInChildren<ShipHealthManagement>();
        shipHealthManager.shipHealth = playerMovement.rb.mass * 10000;
        shipHealthManager.originalShipHealth = shipHealthManager.shipHealth;
        //ship.transform.localScale = new Vector3(1, 1, 1);
        ship.transform.localPosition = new Vector3(0, 0, 0);
        ship.gameObject.layer = 13;
    }
}
