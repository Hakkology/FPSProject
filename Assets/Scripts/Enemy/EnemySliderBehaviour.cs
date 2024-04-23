using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderBehaviour : MonoBehaviour
{
    public Slider hpSlider;
    public Canvas canvas;
    private Camera currentCamera;
    private EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.OnHealthChanged += UpdateHealthDisplay; 
        AssignCamera();
    }

    void OnEnable()
    {
        if (enemyHealth == null)
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged += UpdateHealthDisplay;
        }
    }

    void OnDisable()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged -= UpdateHealthDisplay;
        }
    }

    void Update()
    {
        if (currentCamera != null)
        {
            FaceCamera();
        }
    }

    private void FaceCamera()
    {
        Vector3 directionToCamera = canvas.transform.position - currentCamera.transform.position;
        directionToCamera.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
        canvas.transform.rotation = Quaternion.Slerp(canvas.transform.rotation, targetRotation, Time.deltaTime * 5);
    }

    private void AssignCamera()
    {
        currentCamera = FindCamera();
        if (currentCamera != null && canvas)
        {
            canvas.worldCamera = currentCamera;
            canvas.planeDistance = 1;
        }
    }

    private Camera FindCamera()
    {
        return Camera.main;
    }

    private void UpdateHealthDisplay(int health)
    {
        hpSlider.value = (float)health / enemyHealth.enemyData.enemyHealth;
    }
}
