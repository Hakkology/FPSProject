using System;
using TMPro;
using UnityEngine;
public class AmmoTextBehaviour : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI ammoText;
    public AmmoItemBehaviour itemBehaviour;
    
    void Start(){
        SetupCanvas();
    }

    void Update()
    {
        FaceCamera();
    }

    private void FaceCamera()
    {
        if (Camera.main != null)
        {
            Vector3 directionToCamera = canvas.transform.position - Camera.main.transform.position;
            directionToCamera.y = 0; 

            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
            canvas.transform.rotation = Quaternion.Slerp(canvas.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }

    private void SetupCanvas()
    {
        if (canvas)
        {
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 1;
        }
    }

    public void UpdateTextDisplay()
    {
        ammoText.text = itemBehaviour.GetCurrentAmmo().ToString();
    }


}