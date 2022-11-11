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
    Vector3 lookOffset;

    void Start()
    {
        // Cache main camera position
        mainCamPos = Camera.main.transform.position; 
        mainCamPos.z = 0f;

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

    public void ChangeEyeBallColorRed()
    {
        eyeball.GetComponent<Image>().color = Color.red;
    }

    public void ChangeEyeBallColorWhite()
    {
        eyeball.GetComponent<Image>().color = Color.white;
    }

    void Update()
    {
        lookOffset = (mainCamPos - eyeball.position);

        if (lookOffset.magnitude > eyeRadius)
            lookOffset = lookOffset.normalized * eyeRadius;

        lookOffset.Scale(new Vector3(0.5f, 1.0f));

        pupil.position = eyeball.position + lookOffset;
    }
}
