using UnityEngine;

public class Game_Settings : MonoBehaviour
{
    private void Awake()
    {
        //60 fps
        Application.targetFrameRate = 60;
    }
}
