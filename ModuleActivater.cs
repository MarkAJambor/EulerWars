using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ModuleActivater : MonoBehaviour
{

    public GameObject ammo;
    public GameObject railgunAmmo;
    public GameObject machinegunAmmo;
    public GameObject EMACAmmo;
    public GameObject turretRailgunAmmo;
    public GameObject turretMachinegunAmmo;
    public GameObject turretEMACAmmo;
    public GameObject torpedo;
    public GameObject shot;
    public GameObject leadReticle;
    public GameObject shield;
    public GameObject squareReticle;
    public GameObject pentagonReticle;
    public GameObject hexagonReticle;
    public GameObject octagonReticle;
    public GameObject decagonReticle;
    public GameObject tempReticle;
    public GameObject moduleObject;
    public PlateManager parentPlate;
    public SlugController slug;
    public SlugController impactSlug;
    public Transform self;
    public Transform tempTransform;
    public PlayerMovement playerMovement;
    public PlayerManagement playerManager;
    public ShipHealthManagement shipHealthManager;
    public GameManager manager;
    public Rigidbody temp;
    public AIController[] enemies;
    public AIController enemy;
    public AIController myEnemyBody;
    public ShieldController shieldController;
    public Vector3 relativeVelocity;
    public Vector3 leadReticleForEnemy;
    public Rigidbody rb;
    public Transform reticle;
    public Transform child;
    public ModuleActivater targetModule;
    public PlateManager targetPlate;
    public RaycastHit rayCast;
    public AudioSource laserSoundEffect;
    public Quaternion originalOrientation;
    public string type;
    public int ammoLayer;
    public int layerMask;
    public int moduleNumber;
    public float damage;
    public float lastShot = 0;
    public float laserDelay = 0.3f;
    public float shotDelay;
    public float spawnOffset;
    public float speed;
    public float rotationSpeed;
    public float angleToReticle;
    public float angleToEnemy;
    public float baseDamage;
    public float damageMultiplier;
    public float shieldSize;
    public float health;
    public float originalHealth;
    public float laserDamageTaken;
    public float laserDamageDealt;
    public int enemyIndex;
    public bool disableEnemyWeapons = false;
    public bool isMainMenu = true;
    public bool openFire = false;
    public bool isTurret = false;
    public bool enemySighted = false;
    public bool aimAtReticle;
    public bool isEnemyModule;
    public bool isWeapon = false;

    // Use this for initialization
    void Start()
    {
        if(FindObjectOfType<MainMenu>() == null)
        {
            isMainMenu = false;
            laserSoundEffect = this.gameObject.AddComponent<AudioSource>();
            laserSoundEffect.clip = manager.laserSoundEffect;
            laserSoundEffect.loop = true;
            laserSoundEffect.spatialBlend = 1;
            laserSoundEffect.outputAudioMixerGroup = manager.GetComponent<AudioSource>().outputAudioMixerGroup;
            laserSoundEffect.volume = 0.2f;
        }
        else
        {
            originalOrientation = new Quaternion(this.transform.localRotation.x, this.transform.localRotation.y, this.transform.localRotation.z, this.transform.localRotation.w);
        }

        //set this to true to stop enemy from firing
        disableEnemyWeapons = false;
    }

    public void FixedUpdate()
    {
        if (manager != null && !manager.gameHasEnded)
        {
            if (!isEnemyModule)
            {
                this.shootEnemy();
            }
            else
            {
                if (myEnemyBody.playerIsVisible)
                {
                    if (!disableEnemyWeapons)
                    {
                        this.shootPlayer();
                    }
                }
            }

            //take damage from laser
            if (laserDamageTaken != 0)
            {
                damage = laserDamageTaken * Time.deltaTime;
                health -= damage;
                shipHealthManager.addDamage(damage);
                if (health <= 0)
                {
                    GameObject temp = Instantiate(manager.moduleDestructionEffect, playerManager.transform);
                    temp.transform.localPosition = this.transform.localPosition;
                    Destroy(this.gameObject);
                }
                damage = 0;
                laserDamageTaken = 0;
            }

            if (isEnemyModule && type == "Missile")
            {
                this.activateModule();
            }
        }
    }

    public void activateModule()
    {
        if (type == "FixedLaser" || type == "TurretedLaser")
        {
            if (!laserSoundEffect.isPlaying)
            {
                laserSoundEffect.Play();
            }
            targetModule = null;
            targetPlate = null;
            if (Physics.Raycast(reticle.transform.position, reticle.transform.up, out rayCast, Mathf.Infinity, layerMask))
            {
                targetModule = rayCast.collider.transform.GetComponentInParent<ModuleActivater>();
                targetPlate = rayCast.collider.transform.GetComponent<PlateManager>();
                if (targetModule != null)
                {
                    targetModule.laserDamageTaken = laserDamageDealt;
                }
                else if (targetPlate != null)
                {
                    targetPlate.laserDamageTaken = laserDamageDealt;
                }
                if (rayCast.collider != null)
                {
                    if ((Time.time - lastShot) > laserDelay)
                    {
                        GameObject impactEffect = Instantiate(manager.laserEffect, rayCast.point, Quaternion.identity);
                        impactEffect.transform.localScale = new Vector3(1, 1, 1);
                        //impactEffect.transform.SetParent(rayCast.collider.transform);
                        //impactEffect.transform.localPosition = new Vector3(0, 0, 0);
                        Destroy(impactEffect, 3f);
                        lastShot = Time.time;
                    }
                }
            }

            //create hit effect if raycast hits something
        }
        else if (type != "Shield" && ammo != null)
        {
            if (Time.time - lastShot > shotDelay)
            {
                //tempTransform = self.transform;
                //tempTransform.Translate(0, 0, 0.2f);
                shot = Instantiate(ammo, reticle.position, reticle.rotation);
                shot.transform.parent = self.transform;
                slug = self.GetComponentInChildren<SlugController>();
                slug.manager = manager;
                slug.damage = baseDamage * damageMultiplier;
                slug.gameObject.layer = ammoLayer;
                slug.type = type;
                shot.transform.parent = null;
                temp = shot.GetComponent<Rigidbody>();
                temp.velocity = temp.transform.up * speed;
                temp.velocity += playerMovement.rb.velocity;
                //self.Translate(0, 0, -0.2f);
                lastShot = Time.time;
                if (ammoLayer == 9)
                {
                    slug.enemy = playerManager.selectedEnemy;
                }
                else if (ammoLayer == 11)
                {
                    slug.player = playerMovement;
                }
            }
        }
    }

    public void shootEnemy()
    {
        if (!isMainMenu)
        {
            if (isTurret)
            {
                if (openFire)
                {
                    if (enemySighted && (enemy != null))
                    {
                        if (aimAtReticle)
                        {
                            self.rotation = Quaternion.RotateTowards(self.rotation, Quaternion.LookRotation(leadReticle.transform.position - self.position), rotationSpeed * Time.deltaTime);
                            if (Vector3.Angle((leadReticle.transform.position - self.position), self.forward) <= 5)
                            {
                                this.activateModule();
                            }
                        }
                        else if (Vector3.Angle((enemy.rb.position - self.position), (self.position - playerMovement.rb.position)) <= 90)
                        {
                            //self.LookAt(enemies[0].rb.position);
                            self.rotation = Quaternion.RotateTowards(self.rotation, Quaternion.LookRotation(enemy.rb.position - self.position), rotationSpeed * Time.deltaTime);
                            if (Vector3.Angle((enemy.rb.position - self.position), self.forward) <= 5)
                            {
                                this.activateModule();
                            }
                        }
                        else
                        {
                            enemySighted = false;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            if (enemies[i] == null)
                            {
                                playerManager.refreshEnemyList();
                                i = enemies.Length;
                            }
                            else
                            {
                                if (Vector3.Angle((enemies[i].rb.position - self.position), (self.position - playerMovement.rb.position)) <= 90)
                                {
                                    enemy = enemies[i];
                                    enemySighted = true;
                                    i = enemies.Length;
                                }
                                else
                                {
                                    enemySighted = false;
                                }
                            }
                        }
                    }
                    if ((playerManager.selectedEnemy != null) && (Vector3.Angle((leadReticle.transform.position - self.position), (self.position - playerMovement.rb.position)) <= 90))
                    {
                        enemySighted = true;
                        enemy = playerManager.selectedEnemy;
                        aimAtReticle = true;
                    }
                    else
                    {
                        aimAtReticle = false;
                    }
                }
            }
        }
    }

    public void shootPlayer()
    {
        if (isTurret)
        {
            if (type == "TurretedLaser")
            {
                leadReticleForEnemy = myEnemyBody.playerIndicator;
            }
            else if (type == "TurretedRailgun")
            {
                leadReticleForEnemy = myEnemyBody.railgunLeadReticle;
            }
            else if (type == "TurretedMachinegun")
            {
                leadReticleForEnemy = myEnemyBody.machinegunLeadReticle;
            }
            else if (type == "TurretedEMAC")
            {
                leadReticleForEnemy = myEnemyBody.EMACLeadReticle;
            }
            if (Vector3.Angle((leadReticleForEnemy - self.position), (self.position - myEnemyBody.rb.position)) <= 90)
            {
                //self.LookAt(playerMovement.rb.position);
                self.rotation = Quaternion.RotateTowards(self.rotation, Quaternion.LookRotation(playerMovement.rb.position - self.position), rotationSpeed * Time.deltaTime);
                //self.rotation = Quaternion.RotateTowards(self.rotation, Quaternion.LookRotation(leadReticleForEnemy - self.position), rotationSpeed * Time.deltaTime);
                if (Vector3.Angle((leadReticleForEnemy - self.position), self.forward) <= 30)
                {
                    this.activateModule();
                }
            }
        }
    }

    public void destroyModule()
    {
        shipHealthManager.addDamage(health);
        GameObject temp = Instantiate(manager.moduleDestructionEffect, playerManager.transform);
        temp.transform.localPosition = this.transform.localPosition;
        temp.transform.parent = playerManager.transform;
        Destroy(this.gameObject);
    }

    public void slugHitDamage(Vector3 velocity, float slugDamage)
    {
        damage = manager.collisionCalculator(rb, velocity, slugDamage);
        health -= damage;
        shipHealthManager.addDamage(damage);
        if (health <= 0)
        {
            GameObject temp = Instantiate(manager.moduleDestructionEffect, this.transform);
            temp.transform.position = this.transform.position;
            temp.transform.parent = playerManager.transform;
            Debug.Log("should have spawned");
            Destroy(this.gameObject);
        }
        damage = 0;
    }

    public void clearModule()
    {
        if(moduleObject != null)
        {
            Destroy(moduleObject.gameObject);
        }
        if(shield != null)
        {
            Destroy(shield.gameObject);
        }
        if (tempReticle != null)
        {
            tempReticle.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            reticle.GetComponent<Renderer>().enabled = true;
        }
        self.localRotation = new Quaternion(-1, 0, 0, 1);
    }

    public void moduleInitializer()
    {
        if (type == "FixedRailgun" && isEnemyModule)
        {
            type = "TurretedRailgun";
        }
        else if (type == "FixedMachinegun" && isEnemyModule)
        {
            type = "TurretedMachinegun";
        }
        else if (type == "FixedEMAC" && isEnemyModule)
        {
            type = "TurretedEMAC";
        }
        else if (type == "FixedLaser" && isEnemyModule)
        {
            type = "TurretedLaser";
        }
        reticle.GetComponent<Renderer>().enabled = true;
        playerMovement = FindObjectOfType<PlayerMovement>();
        manager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManagement>();
        shipHealthManager = GetComponentInParent<ShipHealthManagement>();
        rb = shipHealthManager.GetComponentInParent<Rigidbody>();
        myEnemyBody = GetComponentInParent<AIController>();
        parentPlate = GetComponentInParent<PlateManager>();
        baseDamage = manager.damage(type);
        damageMultiplier = manager.damageMultiplier(self.transform.parent.tag);
        shieldSize = manager.shieldSize(self.transform.parent.tag);
        shield = null;
        if (tempReticle == null)
        {
            if (self.transform.parent.tag == "square")
            {
                tempReticle = Instantiate(squareReticle, reticle.transform);
                tempReticle.transform.rotation = new Quaternion(0, 0, 0, 0);
                tempReticle.transform.localScale = new Vector3(1, 1, 1);
            }
            if (self.transform.parent.tag == "pentagon")
            {
                tempReticle = Instantiate(pentagonReticle, reticle.transform);
                tempReticle.transform.rotation = new Quaternion(0, 0, 0, 0);
                tempReticle.transform.localScale = new Vector3(1, 1, 1);
            }
            if (self.transform.parent.tag == "hexagon")
            {
                tempReticle = Instantiate(hexagonReticle, reticle.transform);
                tempReticle.transform.rotation = new Quaternion(0, 0, 0, 0);
                tempReticle.transform.localScale = new Vector3(1, 1, 1);
            }
            if (self.transform.parent.tag == "octagon")
            {
                tempReticle = Instantiate(octagonReticle, reticle.transform);
                tempReticle.transform.rotation = new Quaternion(0, 0, 0, 0);
                tempReticle.transform.localScale = new Vector3(1, 1, 1);
            }
            if (self.transform.parent.tag == "decagon")
            {
                tempReticle = Instantiate(decagonReticle, reticle.transform);
                tempReticle.transform.rotation = new Quaternion(0, 0, 0, 0);
                tempReticle.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            tempReticle.GetComponent<Renderer>().enabled = true;
        }
        if (self.transform.parent.tag != "triangle")
        {
            reticle.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            reticle.GetComponent<Renderer>().enabled = true;
        }

        //remember to set the "type" variable from the ship initializer before calling this method
        if (type == "FixedRailgun")
        {
            shotDelay = manager.railgunShotDelay;
            speed = manager.fixedRailgunSpeed;
            ammo = railgunAmmo;
            isWeapon = true;
            moduleObject = Instantiate(manager.railgunModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(200, 100, 200);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "FixedMachinegun")
        {
            shotDelay = manager.machinegunShotDelay;
            speed = manager.fixedMachinegunSpeed;
            ammo = machinegunAmmo;
            isWeapon = true;
            moduleObject = Instantiate(manager.machinegunModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(200, 100, 200);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "FixedEMAC")
        {
            shotDelay = manager.EMACShotDelay;
            speed = manager.fixedEMACSpeed;
            ammo = EMACAmmo;
            isWeapon = true;
            moduleObject = Instantiate(manager.EMACModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(200, 100, 200);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "TurretedRailgun")
        {
            shotDelay = manager.railgunShotDelay;
            speed = manager.railgunSpeed;
            ammo = turretRailgunAmmo;
            isTurret = true;
            leadReticle = manager.railgunLeadReticle;
            isWeapon = true;
            moduleObject = Instantiate(manager.railgunModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(100, 100, 100);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "TurretedMachinegun")
        {
            shotDelay = manager.machinegunShotDelay;
            speed = manager.machinegunSpeed;
            ammo = turretMachinegunAmmo;
            isTurret = true;
            leadReticle = manager.machinegunLeadReticle;
            isWeapon = true;
            moduleObject = Instantiate(manager.machinegunModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(100, 100, 100);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "TurretedEMAC")
        {
            shotDelay = manager.EMACShotDelay;
            speed = manager.EMACSpeed;
            ammo = turretEMACAmmo;
            isTurret = true;
            leadReticle = manager.EMACLeadReticle;
            isWeapon = true;
            moduleObject = Instantiate(manager.EMACModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(100, 100, 100);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "Shield")
        {
            shield = Instantiate(manager.shield);
            shield.transform.SetParent(self.transform, true);
            shield.transform.position = self.transform.position;
            shield.gameObject.layer = this.gameObject.layer;
            shieldController = GetComponentInChildren<ShieldController>();
            shieldController.shieldSize = shieldSize;
            moduleObject = Instantiate(manager.shieldModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(2, 2, 2);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.5f);
            if (tempReticle != null)
            {
                tempReticle.GetComponent<Renderer>().enabled = false;
            }
            reticle.GetComponent<Renderer>().enabled = false;
        }
        if (type == "Torpedo")
        {
            shotDelay = manager.torpedoShotDelay;
            speed = 0;
            ammo = torpedo;
            isWeapon = true;
            moduleObject = Instantiate(manager.torpedoModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(3, 3, 3);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "Missile")
        {
            shotDelay = manager.torpedoShotDelay;
            speed = manager.missileSpeed;
            ammo = manager.missile;
            isWeapon = true;
            moduleObject = Instantiate(manager.missileModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(100, 100, 100);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "Engine")
        {
            shotDelay = 0;
            speed = 0;
            ammo = null;
            if (FindObjectOfType<MainMenu>() == null)
            {
                if (myEnemyBody == null)
                {
                    playerMovement.engineMultiplier += manager.edgeNumber(self.transform.parent.tag) / 10;
                }
                else
                {
                    myEnemyBody.engineMultiplier += manager.edgeNumber(self.transform.parent.tag) / 10;
                }
            }
            moduleObject = Instantiate(manager.engineModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(3, 3, 3);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.5f);
            reticle.GetComponent<Renderer>().enabled = false;
            if (tempReticle != null)
            {
                tempReticle.GetComponent<Renderer>().enabled = false;
            }
        }
        if (type == "FixedLaser")
        {
            shotDelay = 0;
            speed = 0;
            ammo = null;
            isTurret = false;
            laserDamageDealt = 2 * manager.edgeNumber(self.transform.parent.tag) * manager.baseLaserDamage;
            isWeapon = true;
            moduleObject = Instantiate(manager.laserModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(100, 100, 100);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        if (type == "TurretedLaser")
        {
            shotDelay = 0;
            speed = 0;
            ammo = null;
            isTurret = true;
            laserDamageDealt = (Mathf.Pow(manager.edgeNumber(self.transform.parent.tag), 2) - 8) * manager.baseLaserDamage;
            leadReticle = manager.enemyIndicator;
            isWeapon = true;
            moduleObject = Instantiate(manager.laserModule);
            moduleObject.transform.SetParent(self.transform, true);
            moduleObject.transform.position = self.transform.position;
            moduleObject.gameObject.layer = this.gameObject.layer;
            moduleObject.transform.localScale = new Vector3(100, 100, 100);
            moduleObject.transform.localPosition = new Vector3(0, 0, 0.25f);
            moduleObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }

        if (myEnemyBody == null && FindObjectOfType<MainMenu>() == null && moduleObject != null)
        {
            Color tmp = moduleObject.GetComponent<Renderer>().material.color;
            tmp.a = 0.05f;
            moduleObject.GetComponent<Renderer>().material.color = tmp;
        }
        if (this.GetComponentInParent<PlayerManagement>() && shield != null)
        {
            shield.GetComponent<Renderer>().enabled = false;
        }
        rotationSpeed = manager.turretRotationSpeed;
        if (myEnemyBody != null)
        {
            rotationSpeed *= 2;
        }
        health = manager.plateHealth(parentPlate.tag.ToString()) * 2000f;
        originalHealth = health;
        child.gameObject.layer = this.gameObject.layer;
        layerMask = (1 << 0) | (1 << 12) | (1 << 13) | (1 << 14) | (1 << 15) | (1 << 16);
        if (FindObjectOfType<MainMenu>() != null)
        {
            if (FindObjectOfType<MainMenu>().shieldToggle.isOn == false)
            {
                if (shield != null)
                {
                    shield.GetComponent<Renderer>().enabled = false;
                }
            }

            //self.rotation = originalOrientation;
            self.localRotation = new Quaternion(-1, 0, 0, 1);
            if (type == "TurretedLaser" || type == "TurretedEMAC" || type == "TurretedMachinegun" || type == "TurretedRailgun")
            {
                self.localRotation = Quaternion.Euler(self.localRotation.eulerAngles.x + 45, self.localRotation.eulerAngles.y + 45, 0);
            }
            else
            {
                //self.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (type == null)
        {
            GetComponentInChildren<Collider>().enabled = false;
        }
        baseDamage *= manager.fireRateModifier;
        shotDelay *= manager.fireRateModifier;
    }
}
