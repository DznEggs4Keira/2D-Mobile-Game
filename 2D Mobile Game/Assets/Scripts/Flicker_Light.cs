using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker_Light : MonoBehaviour
{
    public GameObject Lights;

    private void Update()
    {
        StartCoroutine(Flashing());
    }
    // Update is called once per frame
    IEnumerator Flashing()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        Lights.SetActive(!Lights.activeSelf);    
    }
}
