using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModule : MonoBehaviour {

    public int x;
    public int y;
    public int z;
    public float refreshFrequency = 0.3f;
    public float lastRefreshTime = 0;
    public Vector3 aimLocation;
    public Quaternion randomOrientation;
    public GameManager manager;

	// Use this for initialization
	void Start ()
    {
        refreshFrequency = 1;
        manager = FindObjectOfType<GameManager>();
        x = Random.Range(0, manager.gameBoundaryDistance);
        y = Random.Range(0, manager.gameBoundaryDistance);
        z = Random.Range(0, manager.gameBoundaryDistance);
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
        randomOrientation = Quaternion.LookRotation(aimLocation - this.transform.position);
    }

    private void FixedUpdate()
    {
        if (Time.time - lastRefreshTime > refreshFrequency)
        {
            lastRefreshTime = Time.time;
            x = Random.Range(0, manager.gameBoundaryDistance);
            y = Random.Range(0, manager.gameBoundaryDistance);
            z = Random.Range(0, manager.gameBoundaryDistance);
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
            randomOrientation = Quaternion.LookRotation(aimLocation - this.transform.position);
        }
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, randomOrientation, 50 * Time.deltaTime);
    }
}
