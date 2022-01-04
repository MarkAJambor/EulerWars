using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{ 
    public GameManager manager;
    public PlayerManagement playerManager;
    public Rigidbody rb;
    public Transform shipTransform;
    public ModuleActivater weapon;
    public Camera enemyCamera;
    public Camera playerCamera;
    public GameObject playerCore;
    public GameObject xVelocity;
    public GameObject yVelocity;
    public GameObject zVelocity;
    public GameObject velocity;
    public Vector3 localVelocity;
    public Vector3 tempScale;
    public Vector3 tempPosition;
    public Vector3 velocityPosition;
    public Vector3 indicatorPosition;
    public Vector3 forwardVector;
    public Transform[] playerCoreComponents;
    public Slider playerBoostSlider;
    public AudioSource boostRechargeSound;
    public string lastMovementKey;
    public float lastRotationX;
    public float lastRotationy;
    public float lastRotationz;
    public float lastRotationw;
    public float force;
    public float originalForce;
    public float originalTorqueForce;
    public float engineMultiplier = 1;
    public float torqueForce = 50f;
    public float tempScalingFactor;
    public float boostDrainRate = 20;
    public float boostRechargeRate = 20;
    public float boostTapDelay = 0.25f;
    public float boostDelayStartTime = 0;
    public float boostMultiplier;
    public float currentBoostMultiplier = 1;
    public bool isPlayable = true;
    public bool shipRotationLocked = true;
    public bool forward = false;
    public bool backward = false;
    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;
    public bool turnLeft = false;
    public bool turnRight = false;
    public bool rotateClockwise = false;
    public bool rotateCounterClockwise = false;
    public bool rotateUp = false;
    public bool rotateDown = false;
    public bool rotationStop = false;
    public bool motionStop = false;
    public bool fire = false;
    public bool fineControlOn = false;
    public bool autofire = false;
    public bool canBoost = true;
    public bool shouldBoost = false;
    

    public float motionDragRate = 1;
    public float rotationDragRate = 1;

    private void Start()
    {
        indicatorPosition = new Vector3(2000, 2000, 2006);
        forwardVector = new Vector3(1, 0, 0);
    }

    private void Update()
    {
        //control inputs
        if (isPlayable)
        {
            if (playerBoostSlider.value > 0)
            {
                canBoost = true;
            }
            else
            {
                canBoost = false;
            }
            if (Time.time - boostDelayStartTime > boostTapDelay)
            {
                lastMovementKey = null;
            }
            if (Input.GetKeyUp(manager.playerData.controls[13]) && motionStop)
            {
                motionStop = false;
                playerManager.motionDrag.text = "Motion drag off";
                playerManager.motionDrag.color = Color.red;
            }
            else if (Input.GetKeyUp(manager.playerData.controls[13]) && !motionStop)
            {
                motionStop = true;
                playerManager.motionDrag.text = "Motion drag on";
                playerManager.motionDrag.color = Color.white;
            }
            if (Input.GetKeyUp(manager.playerData.controls[14]) && rotationStop)
            {
                rotationStop = false;
                playerManager.rotationDrag.text = "Rotation drag off";
                playerManager.rotationDrag.color = Color.red;
            }
            else if (Input.GetKeyUp(manager.playerData.controls[14]) && !rotationStop)
            {
                rotationStop = true;
                playerManager.rotationDrag.text = "Rotation drag on";
                playerManager.rotationDrag.color = Color.white;
            }
            if (Input.GetKeyUp(KeyCode.Tab) && shipRotationLocked)
            {
                shipRotationLocked = false;
                playerManager.rotationLock.text = "Rotation lock off";
                playerManager.rotationLock.color = Color.red;
            }
            else if(Input.GetKeyUp(KeyCode.Tab) && !shipRotationLocked)
            {
                shipRotationLocked = true;
                playerManager.rotationLock.text = "Rotation lock on";
                playerManager.rotationLock.color = Color.white;
            }
            if (Input.GetKeyDown(manager.playerData.controls[12]) && !fineControlOn)
            {
                fineControlOn = true;
                playerManager.fineControl.text = "Fine control on";
                playerManager.fineControl.color = Color.red;
            }
            else if (Input.GetKeyDown(manager.playerData.controls[12]) && fineControlOn)
            {
                fineControlOn = false;
                playerManager.fineControl.text = "Fine control off";
                playerManager.fineControl.color = Color.white;
            }
            if (Input.GetKeyDown(manager.playerData.controls[6]))
            {
                if (lastMovementKey == null)
                {
                    lastMovementKey = manager.playerData.controls[6];
                    boostDelayStartTime = Time.time;
                }
                else
                {
                    if (lastMovementKey == manager.playerData.controls[6])
                    {
                        //check for boost
                        if (canBoost)
                        {
                            shouldBoost = true;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        lastMovementKey = manager.playerData.controls[6];
                        boostDelayStartTime = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(manager.playerData.controls[7]))
            {
                if (lastMovementKey == null)
                {
                    lastMovementKey = manager.playerData.controls[7];
                    boostDelayStartTime = Time.time;
                }
                else
                {
                    if (lastMovementKey == manager.playerData.controls[7])
                    {
                        //check for boost
                        if (canBoost)
                        {
                            shouldBoost = true;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        lastMovementKey = manager.playerData.controls[7];
                        boostDelayStartTime = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(manager.playerData.controls[8]))
            {
                if (lastMovementKey == null)
                {
                    lastMovementKey = manager.playerData.controls[8];
                    boostDelayStartTime = Time.time;
                }
                else
                {
                    if (lastMovementKey == manager.playerData.controls[8])
                    {
                        //check for boost
                        if (canBoost)
                        {
                            shouldBoost = true;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        lastMovementKey = manager.playerData.controls[8];
                        boostDelayStartTime = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(manager.playerData.controls[9]))
            {
                if (lastMovementKey == null)
                {
                    lastMovementKey = manager.playerData.controls[9];
                    boostDelayStartTime = Time.time;
                }
                else
                {
                    if (lastMovementKey == manager.playerData.controls[9])
                    {
                        //check for boost
                        if (canBoost)
                        {
                            shouldBoost = true;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        lastMovementKey = manager.playerData.controls[9];
                        boostDelayStartTime = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(manager.playerData.controls[10]))
            {
                if (lastMovementKey == null)
                {
                    lastMovementKey = manager.playerData.controls[10];
                    boostDelayStartTime = Time.time;
                }
                else
                {
                    if (lastMovementKey == manager.playerData.controls[10])
                    {
                        //check for boost
                        if (canBoost)
                        {
                            shouldBoost = true;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        lastMovementKey = manager.playerData.controls[10];
                        boostDelayStartTime = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(manager.playerData.controls[11]))
            {
                if (lastMovementKey == null)
                {
                    lastMovementKey = manager.playerData.controls[11];
                    boostDelayStartTime = Time.time;
                }
                else
                {
                    if (lastMovementKey == manager.playerData.controls[11])
                    {
                        //check for boost
                        if (canBoost)
                        {
                            shouldBoost = true;
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        lastMovementKey = manager.playerData.controls[11];
                        boostDelayStartTime = Time.time;
                    }
                }
            }
            if (Input.GetKeyUp(manager.playerData.controls[8]) || Input.GetKeyUp(manager.playerData.controls[9]) || Input.GetKeyUp(manager.playerData.controls[10]) || Input.GetKeyUp(manager.playerData.controls[11]))
            {
                shouldBoost = false;
            }
            if (Input.GetKey(manager.playerData.controls[10]))
            {
                forward = true;
            }
            else
            {
                forward = false;
            }
            if (Input.GetKey(manager.playerData.controls[11]))
            {
                backward = true;
            }
            else
            {
                backward = false;
            }
            if (Input.GetKey(manager.playerData.controls[8]))
            {
                left = true;
            }
            else
            {
                left = false;
            }
            if (Input.GetKey(manager.playerData.controls[9]))
            {
                right = true;
            }
            else
            {
                right = false;
            }
            if (Input.GetKey(manager.playerData.controls[6]))
            {
                up = true;
            }
            else
            {
                up = false;
            }
            if (Input.GetKey(manager.playerData.controls[7]))
            {
                down = true;
            }
            else
            {
                down = false;
            }
            if (Input.GetKey(manager.playerData.controls[4]))
            {
                rotateClockwise = true;
            }
            else
            {
                rotateClockwise = false;
            }
            if (Input.GetKey(manager.playerData.controls[5]))
            {
                rotateCounterClockwise = true;
            }
            else
            {
                rotateCounterClockwise = false;
            }
            if (Input.GetKey(manager.playerData.controls[0]))
            {
                rotateUp = true;
            }
            else
            {
                rotateUp = false;
            }
            if (Input.GetKey(manager.playerData.controls[1]))
            {
                rotateDown = true;
            }
            else
            {
                rotateDown = false;
            }
            if (Input.GetKey(manager.playerData.controls[2]))
            {
                turnLeft = true;
            }
            else
            {
                turnLeft = false;
            }
            if (Input.GetKey(manager.playerData.controls[3]))
            {
                turnRight = true;
            }
            else
            {
                turnRight = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                fire = true;
            }
            else
            {
                fire = false;
            }
            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                playerManager.selectFowardmostEnemy();
            }
            if (Input.GetKeyDown(KeyCode.Space) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                if (autofire)
                {
                    autofire = false;
                }
                else
                {
                    autofire = true;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (shouldBoost)
        {
            currentBoostMultiplier = boostMultiplier;
            playerBoostSlider.value -= boostDrainRate * Time.deltaTime;
            if (!boostRechargeSound.isPlaying)
            {
                boostRechargeSound.Play();
            }
            boostRechargeSound.pitch = 0.5f * playerBoostSlider.value / playerBoostSlider.maxValue;
            boostRechargeSound.volume = 0.5f * (1 - (playerBoostSlider.value / playerBoostSlider.maxValue));
            //boostRechargeSound.Stop();
        }
        else
        {
            currentBoostMultiplier = 1;
            playerBoostSlider.value += boostRechargeRate * Time.deltaTime;
            if (boostRechargeSound.isPlaying)
            {
                boostRechargeSound.pitch = 0.5f * playerBoostSlider.value / playerBoostSlider.maxValue;
                boostRechargeSound.volume = 0.5f * (1 - (playerBoostSlider.value / playerBoostSlider.maxValue));
            }
            else
            {
                boostRechargeSound.Play();
                boostRechargeSound.pitch = 0.5f * playerBoostSlider.value / playerBoostSlider.maxValue;
                boostRechargeSound.volume = 0.5f * (1 - (playerBoostSlider.value / playerBoostSlider.maxValue));
            }
        }
        if (playerBoostSlider.value <= 0)
        {
            shouldBoost = false;
            canBoost = false;
        }
        if (playerBoostSlider.value >= playerBoostSlider.maxValue)
        {
            boostRechargeSound.Stop();
        }
        
        force = originalForce * (5-(4/(engineMultiplier / 5 + 1)));
        torqueForce = originalTorqueForce * (5 - (4 / (engineMultiplier / 5 + 1)));
        if (fineControlOn)
        {
            torqueForce *= 0.2f;
        }
        //5-(4/(x/5 + 1)
        //process movement and drag options
        if (isPlayable && !manager.gameHasEnded)
        {
            if (motionStop)
            {
                rb.drag = motionDragRate;
            }
            if (!motionStop)
            {
                rb.drag = 0;
            }
            if (rotationStop)
            {
                rb.angularDrag = rotationDragRate;
            }
            if (!rotationStop)
            {
                rb.angularDrag = 0;
            }
            if (forward)
            {
                rb.AddForce(playerCamera.transform.forward * force * Time.deltaTime * currentBoostMultiplier, ForceMode.Force);
            }
            if (backward)
            {
                rb.AddForce(-playerCamera.transform.forward * force * Time.deltaTime * currentBoostMultiplier, ForceMode.Force);
            }
            if (left)
            {
                rb.AddForce(-playerCamera.transform.right * force * Time.deltaTime * currentBoostMultiplier, ForceMode.Force);
            }
            if (right)
            {
                rb.AddForce(playerCamera.transform.right * force * Time.deltaTime * currentBoostMultiplier, ForceMode.Force);
            }
            if (up)
            {
                rb.AddForce(playerCamera.transform.up * force * Time.deltaTime * currentBoostMultiplier, ForceMode.Force);
            }
            if (down)
            {
                rb.AddForce(-playerCamera.transform.up * force * Time.deltaTime * currentBoostMultiplier, ForceMode.Force);
            }
            if (shipRotationLocked)
            {
                if (rotateClockwise)
                {
                    rb.AddTorque(-playerCamera.transform.forward * torqueForce * Time.deltaTime, ForceMode.Force);
                }
                if (rotateCounterClockwise)
                {
                    rb.AddTorque(playerCamera.transform.forward * torqueForce * Time.deltaTime, ForceMode.Force);
                }
                if (rotateUp)
                {
                    rb.AddTorque(-playerCamera.transform.right * torqueForce * Time.deltaTime, ForceMode.Force);
                }
                if (rotateDown)
                {
                    rb.AddTorque(playerCamera.transform.right * torqueForce * Time.deltaTime, ForceMode.Force);
                }
                if (turnLeft)
                {
                    rb.AddTorque(-playerCamera.transform.up * torqueForce * Time.deltaTime, ForceMode.Force);
                }
                if (turnRight)
                {
                    rb.AddTorque(playerCamera.transform.up * torqueForce * Time.deltaTime, ForceMode.Force);
                }
            }
            else
            {
                if (rotateClockwise)
                {
                    playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.AngleAxis(-100 * Time.deltaTime, Vector3.forward);
                }
                if (rotateCounterClockwise)
                {
                    playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.AngleAxis(100 * Time.deltaTime, Vector3.forward);
                }
                if (rotateUp)
                {
                    playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.AngleAxis(-100 * Time.deltaTime, Vector3.right);
                }
                if (rotateDown)
                {
                    playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.AngleAxis(100 * Time.deltaTime, Vector3.right);
                }
                if (turnLeft)
                {
                    playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.AngleAxis(-100 * Time.deltaTime, Vector3.up);
                }
                if (turnRight)
                {
                    playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.AngleAxis(100 * Time.deltaTime, Vector3.up);
                }
            }
        }
        if (Vector3.Distance(new Vector3(0, 0, 0), rb.position) > manager.gameBoundaryDistance && manager.gameType != "Race")
        {
            playerManager.boundaryWarning.text = "Arena Containment Activated";
            rb.drag = 1;
            rb.AddForce(-(2 * force * Time.deltaTime * rb.position.x / manager.gameBoundaryDistance), -(2 * force * Time.deltaTime * rb.position.y / manager.gameBoundaryDistance), -(2 * force * Time.deltaTime * rb.position.z / manager.gameBoundaryDistance), ForceMode.Force);
        }
        else
        {
            playerManager.boundaryWarning.text = "";
        }
        playerManager.velocity.text = ((10f*rb.velocity.magnitude).ToString("0"));

        //update velocity + acceleration indicator
        localVelocity = playerCamera.transform.InverseTransformDirection(rb.velocity);

        //localVelocity = playerCamera.transform.InverseTransformDirection(playerCamera.velocity);

        //update xVelocity
        if (localVelocity.x > -0.0001f && localVelocity.x < 0.0001f)
        {
            //xVelocity.gameObject.SetActive(false);
        }
        else
        {
            xVelocity.gameObject.SetActive(true);
            if (localVelocity.x < 0f)
            {
                if (left && !right)
                {
                    xVelocity.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if ((!left && motionStop) || right)
                {
                    xVelocity.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (!motionStop && !(left ^ right))
                {
                    xVelocity.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            if (localVelocity.x > 0f)
            {
                if (right && !left)
                {
                    xVelocity.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if ((!right && motionStop) || left)
                {
                    xVelocity.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (!motionStop && !(left ^ right))
                {
                    xVelocity.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            tempScalingFactor = Mathf.Log(1 + Mathf.Abs(localVelocity.x), 5) * (localVelocity.x / Mathf.Abs(localVelocity.x));
            tempScale = new Vector3(0.5f, tempScalingFactor, 0.5f);
            xVelocity.transform.localScale = tempScale;
            tempPosition = new Vector3(2000 + tempScalingFactor, 2000, 2006);
            xVelocity.transform.position = tempPosition;
        }

        //update yVelocity
        if (localVelocity.y > -0.0001f && localVelocity.y < 0.0001f)
        {
            //yVelocity.gameObject.SetActive(false);
        }
        else
        {
            yVelocity.gameObject.SetActive(true);
            if (localVelocity.y < 0f)
            {
                if (down && !up)
                {
                    yVelocity.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if ((!down && motionStop) || up)
                {
                    yVelocity.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (!motionStop && !(down ^ up))
                {
                    yVelocity.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            if (localVelocity.y > 0f)
            {
                if (up && !down)
                {
                    yVelocity.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if ((!up && motionStop) || down)
                {
                    yVelocity.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (!motionStop && !(up ^ down))
                {
                    yVelocity.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            tempScalingFactor = Mathf.Log(1 + Mathf.Abs(localVelocity.y), 5) * (localVelocity.y / Mathf.Abs(localVelocity.y));
            tempScale = new Vector3(0.5f, tempScalingFactor, 0.5f);
            yVelocity.transform.localScale = tempScale;
            tempPosition = new Vector3(2000, 2000 + tempScalingFactor, 2006);
            yVelocity.transform.position = tempPosition;
        }

        //update zVelocity
        if (localVelocity.z > -0.0001f && localVelocity.z < 0.0001f)
        {
            //zVelocity.gameObject.SetActive(false);
        }
        else
        {
            zVelocity.gameObject.SetActive(true);
            if (localVelocity.z < 0f)
            {
                if (backward && !forward)
                {
                    zVelocity.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if ((!backward && motionStop) || forward)
                {
                    zVelocity.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (!motionStop && !(backward ^ forward))
                {
                    zVelocity.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            if (localVelocity.z > 0f)
            {
                if (forward && !backward)
                {
                    zVelocity.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                if ((!forward && motionStop) || backward)
                {
                    zVelocity.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (!motionStop && !(forward ^ backward))
                {
                    zVelocity.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            tempScalingFactor = Mathf.Log(1 + Mathf.Abs(localVelocity.z), 5) * (localVelocity.z / Mathf.Abs(localVelocity.z));
            tempScale = new Vector3(0.5f, tempScalingFactor, 0.5f);
            zVelocity.transform.localScale = tempScale;
            tempPosition = new Vector3(2000, 2000, 2006 + tempScalingFactor);
            zVelocity.transform.position = tempPosition;
        }

        velocityPosition.x = xVelocity.transform.position.x;
        velocityPosition.y = yVelocity.transform.position.y;
        velocityPosition.z = zVelocity.transform.position.z;
        velocity.transform.localScale = new Vector3(1, 1, (velocityPosition - indicatorPosition).magnitude * 2);
        velocity.transform.position = velocityPosition;
        if(velocityPosition != indicatorPosition)
        {
            velocity.transform.rotation = Quaternion.LookRotation(velocityPosition - indicatorPosition);
        }
    }

    public void initializePM()
    {
        manager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManagement>();
        playerCoreComponents = playerCore.GetComponentsInChildren<Transform>();
        shipTransform = playerCoreComponents[2];
    }
}
