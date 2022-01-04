using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public PlayerManagement playerManager;
    public GameManager gameManager;
    public Text scoreText;
    public float time;
    public float score;
    // Use this for initialization
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManagement>();
        gameManager = FindObjectOfType<GameManager>();
        //scoreText = FindObjectOfType<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //scoreText.text = score.ToString("0");
	}

    private void FixedUpdate()
    {
        score += 0.01f;
    }
}
