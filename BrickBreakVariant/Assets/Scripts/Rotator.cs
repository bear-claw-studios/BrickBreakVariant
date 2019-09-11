using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    //Anything below this seems a bit pointless for sensitivity
    private float baseRotateSpeed = 0.25f;
    public bool reverse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Touch touchInput = Input.GetTouch(0);

        if(input != 0.0f)
        {
            AudioManager.Instance.isRotating = true;
            if (reverse)
            {
                transform.Rotate(0.0f, 0.0f, (input * rotateSpeed));
            }
            else
            {
                transform.Rotate(0.0f, 0.0f, -(input * rotateSpeed));
            }
        } else {
            AudioManager.Instance.isRotating = false;
        }
        
        
           if(touchInput.deltaPosition != Vector2.zero)
           {
               float x = touchInput.deltaPosition.x;

               if(x < 0.0f)
               {
                   transform.Rotate(0.0f, 0.0f, (x * rotateSpeed));
               }
               else if(x > 0.0f)
               {
                   transform.Rotate(0.0f, 0.0f, (x * rotateSpeed));
               }
           } 
        
    }

    public void SetRotationSpeed(float speed)
    {
        rotateSpeed = baseRotateSpeed + speed;
    }
}
