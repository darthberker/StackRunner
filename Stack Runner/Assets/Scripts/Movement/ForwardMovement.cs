using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    public static float moveSpeed =4;
    public static bool start=false;
    [SerializeField] private GameObject character;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = character.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            animator.SetBool("Start", true);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);

        }
    }
}
