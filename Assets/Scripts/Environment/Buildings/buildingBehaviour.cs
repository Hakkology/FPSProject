using UnityEngine;

public class RandomizeTransform : MonoBehaviour
{
    void Start()
    {
        float randomScaleY = Random.Range(20.0f, 60.0f);
        transform.localScale = new Vector3(transform.localScale.x, randomScaleY, transform.localScale.z);

        float yPos = randomScaleY / 2.0f;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}