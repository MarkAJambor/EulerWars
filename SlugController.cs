using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugController : MonoBehaviour
{

    public Rigidbody self;
    public Rigidbody target;
    public GameManager manager;
    public AIController enemy;
    public PlayerMovement player;
    public Vector3 velocity;
    public string type;
    public float force;
    public float missileMaxSpeed;
    public float damage;
    public float startTime;
    public float lifeTime = 10;
    private GameObject particleEffect;
    public GameObject railgunEffect;
    public GameObject machinegunEffect;
    public GameObject EMACEffect;
    public GameObject plasmaEffect;
    public GameObject explosionEffect;
    private ModuleActivater module;
    private PlateManager plate;
    private ShieldController shield;
    // Use this for initialization
    void Start()
    {
        //manager = FindObjectOfType<GameManager>();
        startTime = Time.time;
        force = 2f;
        missileMaxSpeed = 10;
        railgunEffect = manager.railgunEffect;
        machinegunEffect = manager.machinegunEffect;
        EMACEffect = manager.EMACEffect;
        plasmaEffect = manager.plasmaEffect;
        explosionEffect = manager.explosionEffect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > lifeTime)
        {
            //this.spawnEffect();
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            target = player.rb;
        }
        else if (enemy != null)
        {
            target = enemy.rb;
        }
        if (type == "Missile")
        {
            if (target != null)
            {
                //force based on distance
                //force = 0.5f + Mathf.Pow(Mathf.Pow(5, (10 / Vector3.Distance(self.position, target.position))), 0.5f);
                //force = 1.15f;
                force = 2.3f;
                self.drag = 1f;
                self.rotation = Quaternion.LookRotation(target.position - self.position);
                //if((target.velocity - self.velocity).magnitude > 2)
                //{
                    self.AddRelativeForce(0, 0, force * Time.deltaTime, ForceMode.Force);
                //}
            }
            else
            {
                this.spawnEffect();
                Destroy(this.gameObject);
            }
        }
        else if (type == "Torpedo")
        {
            self.AddRelativeForce(0, force * Time.deltaTime, 0, ForceMode.Force);
        }
        velocity = self.velocity;
    }

    public void spawnEffect()
    {
        if (type == "TurretedRailgun" || type == "FixedRailgun")
        {
            particleEffect = Instantiate(railgunEffect, self.transform.position, self.transform.rotation);
        }
        else if (type == "TurretedMachinegun" || type == "FixedMachinegun")
        {
            particleEffect = Instantiate(machinegunEffect, self.transform.position, self.transform.rotation);
        }
        else if (type == "TurretedEMAC" || type == "FixedEMAC")
        {
            particleEffect = Instantiate(EMACEffect, self.transform.position, self.transform.rotation);
        }
        else if (type == "Missile")
        {
            particleEffect = Instantiate(explosionEffect, self.transform.position, self.transform.rotation);
        }
        else if (type == "Torpedo")
        {
            particleEffect = Instantiate(plasmaEffect, self.transform.position, self.transform.rotation);
        }
        if (Random.Range(1, 100000) == 37)
        {
            manager.obviousChickenCluck.Play();
        }
        Destroy(particleEffect, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        shield = collision.collider.GetComponent<ShieldController>();
        if (shield == null)
        {
            module = collision.collider.GetComponentInParent<ModuleActivater>();
            plate = collision.collider.GetComponent<PlateManager>();
            if (module != null)
            {
                if (type == "Missile")
                {
                    module.health -= damage;
                    module.shipHealthManager.addDamage(damage);
                }
                else
                {
                    module.slugHitDamage(velocity, damage);
                }
            }
            else if (plate != null)
            {
                if (type == "Missile")
                {
                    plate.health -= damage;
                    plate.shipHealthManager.addDamage(damage);
                }
                else
                {
                    plate.slugHitDamage(velocity, damage);
                }
            }
            if (type == "Torpedo")
            {
                if (collision.collider.GetComponentInParent<AIController>() != null)
                {
                    collision.collider.GetComponentInParent<AIController>().slowDownMultiplier = 0.2f;
                    collision.collider.GetComponentInParent<AIController>().slowDownTime = Time.time + 10;
                }
            }
        }
        else
        {
            if (type == "Missile")
            {
                shield.shieldHealth -= damage * 0.1f;
            }
            else
            {
                if (shield.enemyBody != null)
                {
                    shield.shieldSound.pitch = 1.25f;
                    shield.shieldSound.Play();
                }
                else
                {
                    shield.shieldSound.Play();
                }
                shield.slugHitDamage(velocity, damage);
            }
        }
        if (manager.numberOfEffects < manager.maxNumberOfEffects)
        {
            this.spawnEffect();
        }
        Destroy(this.gameObject);
    }

    //public void OnTriggerEnter(Collider collider)
    //{
    //    Debug.Log("triggered");
    //    shield = collider.GetComponent<ShieldController>();
    //    if (shield == null)
    //    {
    //        if (type == "Missile")
    //        {
    //            //Vector3 temp = velocity.normalized;
    //            //temp *= Mathf.Sqrt(velocity.magnitude);
    //            //velocity = temp;
    //        }
    //        module = collider.GetComponentInParent<ModuleActivater>();
    //        plate = collider.GetComponent<PlateManager>();
    //        if (module != null)
    //        {
    //            module.slugHitDamage(velocity, damage);
    //        }
    //        else if (plate != null)
    //        {
    //            plate.slugHitDamage(velocity, damage);
    //        }
    //    }
    //    else
    //    {
    //        shield.shieldHealth -= manager.collisionCalculator(shield.rb, this.self.velocity, (float)damage / 4f);
    //    }
    //    this.spawnEffect();
    //    Destroy(this.gameObject);        
    //}
}