using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroller : MonoBehaviour
{
    private float lengthX;
    private float lengthY;
    private float starPositionX;
    private float starPositionY;
    private Transform cam;
    public float parallaxEffectX;
    public float parallaxEffectY;
    // Start is called before the first frame update
    void Start()
    {
        starPositionX = transform.position.x;
        starPositionY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float restartPositionX = cam.transform.position.x * (1 - parallaxEffectX);
        float restartPositionY = cam.transform.position.y * (1 - parallaxEffectY);

        float distanceX = (cam.transform.position.x - starPositionX) * parallaxEffectX;
        float distanceY = (cam.transform.position.y - starPositionY) * parallaxEffectY;
        transform.position = new Vector3(starPositionX - distanceX, starPositionY - distanceY, transform.position.z);

        if (restartPositionX > starPositionX + lengthX)
        {
            starPositionX += lengthX;
        }
        else if (restartPositionX < starPositionX - lengthX)
        {
            starPositionX -= lengthX;
        }

        //if (restartPositionY > starPositionY + lengthY)
        //{
        //    starPositionY += lengthY;
        //}
        //else if (restartPositionY < starPositionY - lengthY)
        //{
        //    starPositionY -= lengthY;
        //}
    }
}
