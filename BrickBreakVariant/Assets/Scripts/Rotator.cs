using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float baseAngle = 0.0f;
    Vector3 pos;
    public bool reverse = false;

    /*
    public float rotateSpeed = 1.0f;
    //Anything below this seems a bit pointless for sensitivity
    private float baseRotateSpeed = 0.25f;
    

    

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
    */

    void OnMouseDown()
    {
        
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        Debug.Log(pos);
        baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        
        //pos = Input.mousePosition;
        //Debug.Log(pos);
    }

    void OnMouseDrag()
    {
        
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        Debug.Log(pos);
        float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
        Debug.Log(ang);
        if(reverse)
            transform.rotation = Quaternion.AngleAxis(-ang, Vector3.forward);
        else
            transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);

        //transform.Rotate(0.0f, 0.0f, (Input.mousePosition.x - pos.x));
    }
}
