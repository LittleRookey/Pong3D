using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeBallMovement : MonoBehaviour
{
    public GameController gameController; // gamecontroller reference
    public Transform pupil; // pupil transform 
    public Transform eyeball; // eyeball transform
    public float eyeRadius = 1f; // radius of the eyeball

    private 
    Vector3 mainCamPos; // position of Main Camera
    Vector3 lookOffset; // lookOffset of eyeball

    void Start()
    {
        // Cache main camera position
        mainCamPos = Camera.main.transform.position; 
        mainCamPos.z = 0f; // set it to 0 for natural looking of eyeball

    }

    private void OnEnable()
    {
        GameController.Instance.OnGameEnd += ChangeEyeBallColorRed;
        GameController.Instance.OnGameRestart += ChangeEyeBallColorWhite;
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameEnd -= ChangeEyeBallColorRed;
        GameController.Instance.OnGameRestart -= ChangeEyeBallColorWhite;
    }

    /// <summary>
    /// changes the color of the eyeball to red
    /// </summary>
    public void ChangeEyeBallColorRed()
    {
        eyeball.GetComponent<Image>().color = Color.red;
    }

    /// <summary>
    /// changes the color of the eyeball to white
    /// </summary>
    public void ChangeEyeBallColorWhite()
    {
        eyeball.GetComponent<Image>().color = Color.white;
    }

    void Update()
    {
        lookOffset = (mainCamPos - eyeball.position); // distance from camera to eyeball

        if (lookOffset.magnitude > eyeRadius) // limits the lookOffset
            lookOffset = lookOffset.normalized * eyeRadius;

        lookOffset.Scale(new Vector3(0.5f, 1.0f));

        pupil.position = eyeball.position + lookOffset;
    }
}
