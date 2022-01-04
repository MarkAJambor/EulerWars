using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour {

    public Text health;
	// Use this for initialization
	void Start ()
    {
        health = FindObjectOfType<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        health.text = "Hello";
	}
}
