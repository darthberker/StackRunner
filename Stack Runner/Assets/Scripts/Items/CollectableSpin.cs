using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private bool isSpin=true;
    [SerializeField] private bool isHorizontalMoveable = false;
    private bool xMove;
    [SerializeField] private bool isVerticalMoveable = false;
    private bool zMove;
    private float zMax;
    private float zMin;
    // Start is called before the first frame update
    void Start()
    {
        zMax = transform.position.z + 5;
        zMin = transform.position.z - 4;
        zMove = true;
        xMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpin)
        {
            if (gameObject.CompareTag("Obstacle"))
            {
                transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
            }
        }

        if (isHorizontalMoveable)
        {
            if (transform.position.x > 4)
            {
                xMove = false;
            }
            else if(transform.position.x< -3)
            {
                xMove = true;
            }
            if (xMove)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            
        }
        if (isVerticalMoveable)
        {
            
            if (transform.position.z <= zMin)
            {
                zMove = true;
            }
            else if (transform.position.z >= zMax)
            {
                zMove = false;
            }

            if (zMove)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);
            }
        }

    }
}
