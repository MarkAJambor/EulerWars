using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public Transform self;
    public Vector3 playerIndicator;
    public Vector3 railgunLeadReticle;
    public Vector3 machinegunLeadReticle;
    public Vector3 EMACLeadReticle;
    public Vector3 aimLocation;
    public Rigidbody rb;
    public PlayerManagement playerManager;
    public PlayerMovement playerMovement;
    public GameManager manager;
    public ShipHealthManagement shipHealthManager;
    public Quaternion randomOrientation;
    public Quaternion shipDirection;
    public Quaternion currentModuleDirection;
    public Quaternion quat0;
    public Quaternion quat1;
    public Quaternion quat10;
    public Vector3 playerRelativeAngle;
    public Vector3 relativeVelocity;
    public SlugController slug;
    public ModuleActivater[] modules;
    public ModuleActivater currentModule;
    public PlateManager[] plates;
    public GameObject ship;
    public RaycastHit rayCast;
    public string[] moduleTypeArray;
    public string shipType;
    public bool shouldBackUp = false;
    public bool playerIsVisible = false;
    public bool shouldRetreat = false;
    public bool shouldHide = false;
    public int hideDistance = 100;
    public int x;
    public int y;
    public int z;
    public int playAreaBoundaryRange;
    public int randomDirection;
    public int moduleNumber;
    public int playerTrackerLayermask;
    public float damage;
    public float health;
    public float xAxis;
    public float yAxis;
    public float zAxis;
    public float rotationSpeed;
    public float collisionDuration;
    public float torqueForce;
    public float force;
    public float originalForce;
    public float engineMultiplier = 1;
    public float searchFrequency = 10;
    public float lastSearchTime = 0;
    public float lastSeenTime = 0;
    public float playerRelativeAngleToForward;
    public float backupDuration = 3;
    public float lastBackupTime = 0;
    public float distanceToPlayer;
    public float lastLateralTime = 0;
    public float originalLateralThrustDuration = 10;
    public float lateralThrustDuration;
    public float currentAdjustedFireDelay = 1;
    public float spawnTime;
    public float slowDownTime = 0;
    public float slowDownMultiplier = 1;

    // Use this for initialization
    void Start ()
    {
        hideDistance = 150;
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update ()
    {
		if (health < 0)
        {
            this.destroySelf();
        }
        if (plates != null)
        {
            for (int i = 0; i <= plates.Length; i++)
            {
                if (i == plates.Length)
                {
                    this.destroySelf();
                }
                if (plates[i].health > 0)
                {
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!manager.gameHasEnded && Time.time > spawnTime + 5)
        {
            //if (Time.time > 5)
            //{
            //    if (manager.averageFramerate < manager.goalFramerate)
            //    {
            //        this.setFireRates();
            //    }
            //}
            this.setLeadReticles();
            if (currentModule == null)
            {
                foreach (ModuleActivater module in modules)
                {
                    if (module != null && module.isWeapon)
                    {
                        currentModule = module;
                        break;
                    }
                }
                if (currentModule != null)
                {
                    currentModuleDirection = Quaternion.LookRotation(currentModule.self.position - self.position);
                    shipDirection = new Quaternion(ship.transform.rotation.x, ship.transform.rotation.y, ship.transform.rotation.z, ship.transform.rotation.w);
                    self.rotation = currentModuleDirection;
                    ship.transform.rotation = shipDirection;
                }
            }
            force = originalForce * (5 - (4 / (engineMultiplier / 5 + 1)));
            health = shipHealthManager.shipHealth;
            distanceToPlayer = Vector3.Distance(playerMovement.rb.position, self.position);
            playerRelativeAngleToForward = Vector3.Angle((playerMovement.rb.transform.position - self.position), self.forward);
            //if the AI sees the player

            if (Physics.Raycast(self.position, playerMovement.rb.transform.position - self.position, out rayCast, Mathf.Infinity, playerTrackerLayermask))
            {
                if (rayCast.collider.transform.GetComponentInParent<PlayerManagement>() != null)
                {
                    playerIsVisible = true;
                }
                else
                {
                    playerIsVisible = false;
                }
            }
            if (!playerIsVisible)
            {
                foreach (ModuleActivater module in modules)
                {
                    if (module != null)
                    {
                        if (module.laserSoundEffect != null)
                        {
                            module.laserSoundEffect.Pause();
                        }
                    }
                }
            }
            if (playerRelativeAngleToForward < 120 && playerIsVisible)
            {
                lastSearchTime = Time.time + searchFrequency;
                randomOrientation = Quaternion.LookRotation(playerMovement.rb.position - self.position);
                if (distanceToPlayer > 100)
                {
                    rb.drag = 0.1f;
                }
                else
                {
                    rb.drag = 1;
                }
            }
            else //if the AI didn't see the player
            {
                rb.drag = 1;
                if (Time.time - lastSearchTime > searchFrequency)
                {
                    x = Random.Range(0, playAreaBoundaryRange);
                    y = Random.Range(0, playAreaBoundaryRange);
                    z = Random.Range(0, playAreaBoundaryRange);
                    if (Random.Range(0, 2) == 0)
                    {
                        x *= -1;
                    }
                    if (Random.Range(0, 2) == 0)
                    {
                        y *= -1;
                    }
                    if (Random.Range(0, 2) == 0)
                    {
                        z *= -1;
                    }
                    aimLocation.Set(x, y, z);
                    randomOrientation = Quaternion.LookRotation(aimLocation - self.position);
                    lastSearchTime = Time.time;
                }
                if (Vector3.Distance(new Vector3(0, 0, 0), rb.position) > manager.gameBoundaryDistance)
                {
                    rb.drag = 1;
                    rb.AddForce(-(2 * force * Time.deltaTime * rb.position.x / manager.gameBoundaryDistance), -(2 * force * Time.deltaTime * rb.position.y / manager.gameBoundaryDistance), -(2 * force * Time.deltaTime * rb.position.z / manager.gameBoundaryDistance), ForceMode.Force);
                    aimLocation.Set(0, 0, 0);
                    randomOrientation = Quaternion.LookRotation(aimLocation - self.position);
                    lastSearchTime = Time.time + searchFrequency;
                }
            }
            quat0 = this.transform.rotation;
            quat1 = randomOrientation;
            quat10 = quat1 * Quaternion.Inverse(quat0);
            rb.AddTorque(quat10.x * torqueForce * Time.deltaTime, quat10.y * torqueForce * Time.deltaTime, quat10.z * torqueForce * Time.deltaTime, ForceMode.Force);
            //self.rotation = Quaternion.RotateTowards(self.rotation, randomOrientation, rotationSpeed * Time.deltaTime);
            if (!shouldBackUp)
            {
                if (distanceToPlayer < hideDistance * 2)
                {
                    if (Time.time - lastLateralTime > lateralThrustDuration)
                    {
                        if (randomDirection == 1)
                        {
                            rb.AddRelativeForce(0, slowDownMultiplier * force * Time.deltaTime, 0, ForceMode.Force);
                        }
                        else if (randomDirection == 2)
                        {
                            rb.AddRelativeForce(0, slowDownMultiplier * -force * Time.deltaTime, 0, ForceMode.Force);
                        }
                        else if (randomDirection == 3)
                        {
                            rb.AddRelativeForce(slowDownMultiplier * force * Time.deltaTime, 0, 0, ForceMode.Force);
                        }
                        else if (randomDirection == 4)
                        {
                            rb.AddRelativeForce(slowDownMultiplier * -force * Time.deltaTime, 0, 0, ForceMode.Force);
                        }
                        lateralThrustDuration -= Time.deltaTime;
                        if (lateralThrustDuration < 0)
                        {
                            lateralThrustDuration = originalLateralThrustDuration;
                            lastLateralTime = Time.time;
                        }
                    }
                }

                if ((shipHealthManager.originalShipHealth - shipHealthManager.shipHealth)/shipHealthManager.originalShipHealth > 0.5f)
                {
                    shouldRetreat = true;
                    rb.drag = 1;
                }
                if (shouldRetreat)
                {
                    if (distanceToPlayer > hideDistance)
                    {
                        shouldHide = true;
                    }
                    else
                    {
                        shouldHide = false;
                    }
                    if (shouldHide)
                    {
                        if (distanceToPlayer > hideDistance * 1.1f)
                        {
                            rb.AddRelativeForce(0, 0, 0.5f * slowDownMultiplier * force * Time.deltaTime, ForceMode.Force);
                        }
                    }
                    else
                    {
                        rb.AddRelativeForce(0, 0, -0.5f * slowDownMultiplier * force * Time.deltaTime, ForceMode.Force);
                    }
                }
                else
                {
                    rb.AddRelativeForce(0, 0, slowDownMultiplier * force * Time.deltaTime, ForceMode.Force);
                }
            }
            else
            {
                //get enemy unstuck
                rb.AddRelativeForce(0, 0, -force * Time.deltaTime, ForceMode.Force);
                if (Time.time - lastBackupTime > backupDuration)
                {
                    shouldBackUp = false;
                }
            }
            if (Time.time > slowDownTime)
            {
                slowDownMultiplier = 1;
            }
        }
    }

    public void destroySelf()
    {
        manager.addScore(shipType);
        manager.currentNumberOfEnemies--;
        if (manager.currentNumberOfEnemies == 0)
        {
            if (manager.gameType == "Survival")
            {
                manager.initialNumberOfEnemies++;
                manager.spawnEnemies(manager.initialNumberOfEnemies);
            }
            else if (manager.gameType == "AllShipSurvival")
            {
                manager.spawnEnemies(manager.allShipSurvivalRound);
            }
        }
        GameObject healthBoost = Instantiate(manager.healthBoost);
        healthBoost.transform.position = this.transform.position;
        GameObject particleEffect = Instantiate(manager.shipDestructionEffect, self.transform.position, self.transform.rotation);
        Destroy(particleEffect, 3.5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //slug = collision.collider.gameObject.transform.GetComponentInChildren<SlugController>();
        //if (slug != null)
        //{
        //    damage = manager.collisionCalculator(collision, rb, slug);
        //}
        collisionDuration = 0;
        slug = null;
        //shipHealthManager.addDamage(damage);
        //damage = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        //collisionDuration += Time.deltaTime;
        //if (collisionDuration > searchFrequency)
        //{
        if (Time.time - lastBackupTime > backupDuration * 2)
        {
            lastBackupTime = Time.time;
            shouldBackUp = true;
        }
        else
        {

        }

        //}
    }

    public void setModuleTypes()
    {
        modules = rb.transform.GetComponentsInChildren<ModuleActivater>();
        moduleNumber = 0;
        foreach (ModuleActivater module in modules)
        {
            module.type = moduleTypeArray[moduleNumber];
            module.ammoLayer = 11;
            module.gameObject.layer = 14;
            moduleNumber++;
            module.isEnemyModule = true;
        }
    }

    public void setPlateTypes()
    {
        plates = this.transform.GetComponentsInChildren<PlateManager>();
        foreach (PlateManager plate in plates)
        {
            plate.gameObject.layer = 14;
            plate.gameObject.GetComponent<Renderer>().enabled = true;
            plate.initializePlate();
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
        }
    }

    public void initializeModules()
    {
        modules = rb.transform.GetComponentsInChildren<ModuleActivater>();
        foreach (ModuleActivater module in modules)
        {
            module.openFire = true;
            module.moduleInitializer();
        }
    }

    public void setLeadReticles()
    {
        playerIndicator = playerMovement.transform.position;
        railgunLeadReticle = manager.getInterceptPoint(rb.transform.position, rb.velocity, manager.railgunSpeed, playerMovement.rb.position, playerMovement.rb.velocity);
        machinegunLeadReticle = manager.getInterceptPoint(rb.transform.position, rb.velocity, manager.machinegunSpeed, playerMovement.rb.position, playerMovement.rb.velocity);
        EMACLeadReticle = manager.getInterceptPoint(rb.transform.position, rb.velocity, manager.EMACSpeed, playerMovement.rb.position, playerMovement.rb.velocity);
    }

    public void initializeShip()
    {
        randomOrientation = self.rotation;
        playerManager = FindObjectOfType<PlayerManagement>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        manager = FindObjectOfType<GameManager>();
        shipHealthManager = GetComponentInChildren<ShipHealthManagement>();
        randomDirection = Random.Range(1, 5);
        lateralThrustDuration = originalLateralThrustDuration;
        playerTrackerLayermask = (1 << 0) | (1 << 8) | (1 << 13) | (1 << 15);
        if (shipType == "tetrahedron")
        {
            ship = Instantiate(manager.tetrahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.tetrahedronMass;
            force = manager.defaultForce * 1.1f;
            torqueForce = manager.defaultTorque;
        }
        else if (shipType == "cube")
        {
            ship = Instantiate(manager.cubeShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.cubeMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.cubeVSA) * manager.tetrahedronVSA / manager.cubeVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.cubeVSA) * manager.tetrahedronVSA / manager.cubeVSA;
        }
        else if (shipType == "octahedron")
        {
            ship = Instantiate(manager.octahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.octahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.octahedronVSA) * manager.tetrahedronVSA / manager.octahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.octahedronVSA) * manager.tetrahedronVSA / manager.octahedronVSA;
        }
        else if (shipType == "dodecahedron")
        {
            ship = Instantiate(manager.dodecahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.dodecahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.dodecahedronVSA) * manager.tetrahedronVSA / manager.dodecahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.dodecahedronVSA) * manager.tetrahedronVSA / manager.dodecahedronVSA;
        }
        else if (shipType == "icosahedron")
        {
            ship = Instantiate(manager.icosahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.icosahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.icosahedronVSA) * manager.tetrahedronVSA / manager.icosahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.icosahedronVSA) * manager.tetrahedronVSA / manager.icosahedronVSA;
        }
        else if (shipType == "truncatedTetrahedron")
        {
            ship = Instantiate(manager.truncatedTetrahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedTetrahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedTetrahedronVSA) * manager.tetrahedronVSA / manager.truncatedTetrahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedTetrahedronVSA) * manager.tetrahedronVSA / manager.truncatedTetrahedronVSA;
        }
        else if (shipType == "cuboctahedron")
        {
            ship = Instantiate(manager.cuboctahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.cuboctahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.cuboctahedronVSA) * manager.tetrahedronVSA / manager.cuboctahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.cuboctahedronVSA) * manager.tetrahedronVSA / manager.cuboctahedronVSA;
        }
        else if (shipType == "truncatedCube")
        {
            ship = Instantiate(manager.truncatedCubeShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedCubeMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedCubeVSA) * manager.tetrahedronVSA / manager.truncatedCubeVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedCubeVSA) * manager.tetrahedronVSA / manager.truncatedCubeVSA;
        }
        else if (shipType == "truncatedOctahedron")
        {
            ship = Instantiate(manager.truncatedOctahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedOctahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedOctahedronVSA) * manager.tetrahedronVSA / manager.truncatedOctahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedOctahedronVSA) * manager.tetrahedronVSA / manager.truncatedOctahedronVSA;
        }
        else if (shipType == "rhombicuboctahedron")
        {
            ship = Instantiate(manager.rhombicuboctahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.rhombicuboctahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.rhombicuboctahedronVSA) * manager.tetrahedronVSA / manager.rhombicuboctahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.rhombicuboctahedronVSA) * manager.tetrahedronVSA / manager.rhombicuboctahedronVSA;
        }
        else if (shipType == "truncatedCuboctahedron")
        {
            ship = Instantiate(manager.truncatedCuboctahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedCuboctahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA) * manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA) * manager.tetrahedronVSA / manager.truncatedCuboctahedronVSA;
        }
        else if (shipType == "snubCube")
        {
            ship = Instantiate(manager.snubCubeShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.snubCubeMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.snubCubeVSA) * manager.tetrahedronVSA / manager.snubCubeVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.snubCubeVSA) * manager.tetrahedronVSA / manager.snubCubeVSA;
        }
        else if (shipType == "icosidodecahedron")
        {
            ship = Instantiate(manager.icosidodecahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.icosidodecahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.icosidodecahedronVSA) * manager.tetrahedronVSA / manager.icosidodecahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.icosidodecahedronVSA) * manager.tetrahedronVSA / manager.icosidodecahedronVSA;
        }
        else if (shipType == "truncatedDodecahedron")
        {
            ship = Instantiate(manager.truncatedDodecahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedDodecahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedDodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedDodecahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedDodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedDodecahedronVSA;
        }
        else if (shipType == "truncatedIcosahedron")
        {
            ship = Instantiate(manager.truncatedIcosahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedIcosahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedIcosahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedIcosahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosahedronVSA;
        }
        else if (shipType == "rhombicosidodecahedron")
        {
            ship = Instantiate(manager.rhombicosidodecahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.rhombicosidodecahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA) * manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA) * manager.tetrahedronVSA / manager.rhombicosidodecahedronVSA;
        }
        else if (shipType == "truncatedIcosidodecahedron")
        {
            ship = Instantiate(manager.truncatedIcosidodecahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.truncatedIcosidodecahedronMass;
            force = manager.defaultForce * (manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA;
            torqueForce = manager.defaultTorque * (manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA) * manager.tetrahedronVSA / manager.truncatedIcosidodecahedronVSA;
        }
        else if (shipType == "snubDodecahedron")
        {
            ship = Instantiate(manager.snubDodecahedronShip);
            ship.transform.SetParent(rb.transform, true);
            rb.mass = manager.snubDodecahedronMass;
            force = 0.5f * manager.defaultForce * (manager.tetrahedronVSA / manager.snubDodecahedronVSA) * manager.tetrahedronVSA / manager.snubDodecahedronVSA;
            torqueForce = 0.5f * manager.defaultTorque * (manager.tetrahedronVSA / manager.snubDodecahedronVSA) * manager.tetrahedronVSA / manager.snubDodecahedronVSA;
        }
        //increase size of ship
        if (manager.main == null)
        {
            ship.transform.localScale = new Vector3(manager.shipScaleFactor, manager.shipScaleFactor, manager.shipScaleFactor);
            torqueForce *= manager.shipScaleFactor * manager.shipScaleFactor;
        }
        originalForce = force;
        shipHealthManager = GetComponentInChildren<ShipHealthManagement>();
        shipHealthManager.shipHealth = rb.mass * 10000;
        shipHealthManager.originalShipHealth = shipHealthManager.shipHealth;
        //ship.transform.localScale = new Vector3(1, 1, 1);
        ship.transform.localPosition = new Vector3(0, 0, 0);
        ship.gameObject.layer = 12;

        //set AI difficulty by increasing frequency and duration of lateral thrusts
        lateralThrustDuration = Mathf.Pow(rb.mass, 0.25f) * Random.Range(1, 10);
        originalLateralThrustDuration = lateralThrustDuration;

        //compensate for different control scheme
        torqueForce *= 1.5f;

        this.setModuleTypes();
        this.setPlateTypes();
        this.initializeModules();
        foreach (ModuleActivater mod in modules)
        {
            if (mod.type == "TurretedEMAC" || mod.type == "TurretedMachinegun" || mod.type == "TurretedRailgun" || mod.type == "FixedEMAC" || mod.type == "FixedMachinegun" || mod.type == "FixedRailgun")
            {
                mod.baseDamage *= manager.fireRateModifier;
                mod.shotDelay *= manager.fireRateModifier;
            }
        }

        if (manager.playerData.beginnerModeOn)
        {
            force /= 2;
            originalForce /= 2;
            rb.drag = 2;
        }
        GameObject particleEffect = Instantiate(manager.spawnEffect, self.transform.position, self.transform.rotation);
        Destroy(particleEffect, 3.5f);
        float roundsPerSecond = 0;
        float roundLimit = 60;
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
}
