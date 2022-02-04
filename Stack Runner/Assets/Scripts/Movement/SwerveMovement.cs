using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    public static float swerveSpeed = 0.5f;
    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;

        if (ForwardMovement.start)
        {
            transform.Translate(swerveAmount, 0, 0);

            if (transform.position.x < -1.7)
            {
                transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > 2.2)
            {
                transform.position = new Vector3(2.2f, transform.position.y, transform.position.z);
            }
        }

    }
}
