using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {

    public float shieldHealth;
    public float maxShieldHealth;
    public float shieldSize;
    public float currentShieldSize;
    public float originalShieldSize;
    public float regenRate;
    public float shieldBreakTime;
    public float damage;
    public SlugController slug;
    public Transform self;
    public GameManager manager;
    //public Rigidbody rb;
    public AudioSource shieldSound;
    public SphereCollider selfCollider;
    public AIController enemyBody;
    public bool inGame;
    public string type;
	// Use this for initialization
	void Start ()
    {
        manager = FindObjectOfType<GameManager>();
        self = GetComponent<Transform>();
        selfCollider = GetComponent<SphereCollider>();
        enemyBody = GetComponentInParent<AIController>();
        if(FindObjectOfType<MainMenu>() == null)
        {
            inGame = true;
        }
        else
        {
            inGame = false;
        }
        shieldHealth = 500 * Mathf.Pow(shieldSize, 5f);
        maxShieldHealth = shieldHealth;
        regenRate = maxShieldHealth * 0.01f;
        originalShieldSize = 10 * shieldSize;
        currentShieldSize = originalShieldSize;
        transform.localScale = new Vector3(currentShieldSize, currentShieldSize, currentShieldSize);
        if (this.gameObject.layer == 14)
        {
            this.gameObject.layer = 10;
        }
        else if (this.gameObject.layer == 15)
        {
            this.gameObject.layer = 8;
        }
        if (FindObjectOfType<MainMenu>() != null)
        {
            this.gameObject.layer = 2;
        }
    }

    private void FixedUpdate()
    {
        selfCollider.enabled = false;
        selfCollider.enabled = true;
        if (shieldHealth < maxShieldHealth)
        {
            shieldHealth += regenRate * (shieldHealth / maxShieldHealth) * Time.deltaTime;
        }
        currentShieldSize = originalShieldSize * (shieldHealth / maxShieldHealth);
        transform.localScale = new Vector3(currentShieldSize, currentShieldSize, currentShieldSize);
        if (shieldHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void slugHitDamage(Vector3 velocity, float slugDamage)
    {
        damage = manager.collisionCalculator(this.GetComponentInParent<Rigidbody>(), velocity, slugDamage);
        shieldHealth -= damage;
        damage = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (inGame)
        {
            if (enemyBody != null)
            {
                shieldSound.pitch = 1.25f;
                shieldSound.Play();
            }
            else
            {
                shieldSound.Play();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (inGame)
        {
            shieldSound.Play();
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    slug = collision.collider.gameObject.transform.GetComponentInChildren<SlugController>();
    //    if (slug != null)
    //    {
    //        Debug.Log("slug type: " + slug.type);
    //        damage = manager.collisionCalculator(rb, slug.velocity, slug.damage);
    //        shieldHealth -= damage;
    //        Destroy(slug.gameObject);
    //    }
    //    slug = null;
    //    damage = 0;

    //    if (shieldHealth < 0)
    //    {
    //        shieldHealth = 0;
    //        shieldBreakTime = Time.time;
    //    }
    //}
}
