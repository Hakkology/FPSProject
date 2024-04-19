using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    public List<GameObject> gunGameObjects; 
    private int currentGunIndex = -1; 

    void Update()
    {
        // Key bindings for switching guns, can be extended as needed
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SwitchGun(0); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SwitchGun(1); 
        }
    }

    private void SwitchGun(int gunIndex)
    {
        if (gunIndex != currentGunIndex && gunIndex < gunGameObjects.Count)
        {
            if (currentGunIndex != -1) {
                gunGameObjects[currentGunIndex].SetActive(false);
            }

            gunGameObjects[gunIndex].SetActive(true);
            currentGunIndex = gunIndex; 
        }
    }
}
