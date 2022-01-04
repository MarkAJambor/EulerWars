using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthManagement : MonoBehaviour {

    public PlayerManagement playerManager;
    public SlugController slug;
    public Vector3 relativeVelocity;
    public Rigidbody parentRB;
    public Transform child;
    public float shipHealth;
    public float originalShipHealth;
    public float damage;

	// Use this for initialization
	void Start ()
    {
        parentRB = GetComponentInParent<Rigidbody>();
        child.gameObject.layer = this.gameObject.layer;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addDamage(float damage)
    {
        shipHealth -= damage;
    }
}
