using UnityEngine;
using UnityEngine.UI;

public class Powerup_Panel : MonoBehaviour
{
    //UI for showing gun and shield
    public Image bananaGun;
    public Image cherryShield;

    public void ShowGun(bool isGunOn)
    {
        if(isGunOn)
        {
            bananaGun.color = Color.white;
            Debug.Log("Gun is on in UI");
        }
        else
        {
            bananaGun.color = Color.clear;
        }
    }

    public void ShowShield(bool isShieldOn)
    {
        if (isShieldOn)
        {
            cherryShield.color = Color.white;
            Debug.Log("Shield is on in UI");
        }
        else
        {
            cherryShield.color = Color.clear;
        }
    }

}
