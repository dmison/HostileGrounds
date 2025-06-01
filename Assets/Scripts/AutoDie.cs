using System.Collections;
using UnityEngine;

public class AutoDie : MonoBehaviour
{
    [SerializeField] private float _lifeTimeSeconds;

    void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_lifeTimeSeconds);
        Destroy(gameObject);
    }
}

