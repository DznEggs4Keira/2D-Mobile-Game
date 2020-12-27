using UnityEngine;

public class Background_Scroller : MonoBehaviour
{
    [SerializeField]
    Material mat;

    public float vertMove = 0.5f;

    Vector2 offset;

    private void Start()
    {
        offset = new Vector2(0, vertMove);

        //reset background texture
        mat.mainTextureOffset = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
