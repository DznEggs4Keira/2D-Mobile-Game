using System.Collections;
using UnityEngine;

public class Flicker_Light : MonoBehaviour
{
    public GameObject Lights;

    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(Flashing());
    }
    
    IEnumerator Flashing()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        Lights.SetActive(!Lights.activeSelf);    
    }
}
