using System.Collections;
using UnityEngine;

public class EnemyCoroutineController : MonoBehaviour
{
    public void ExecuteCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopAllCoroutinesSafe()
    {
        StopAllCoroutines();
    }
}
