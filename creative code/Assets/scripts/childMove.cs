using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class childMove : MonoBehaviour
{
    public int my_moveLenght;
    public int my_speed;
    public GameObject center;
    public Vector3 normalDeplacement;
    private Vector3 targetPosition;
    private int storedSpeed;

// Start is called before the first frame update
    void Start()
    {
        storedSpeed = my_speed;
        my_speed = 0;
        // rotate toward center modulo random
        transform.LookAt(center.transform.position);
        transform.Rotate(0,Random.Range(5f,20f),0,Space.Self);
        targetPosition = transform.position + transform.forward * my_moveLenght;
        normalDeplacement = targetPosition - transform.position;
    }

// Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, my_speed * Time.deltaTime);
    }

    public void OnEventStart()
    {
        my_speed = storedSpeed;
        Debug.Log("event receive" + my_speed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision" + other);
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("tag");
            targetPosition = other.transform.position;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if ( transform.position.x < other.transform.position.x)
            {
                targetPosition = transform.position;
            }
            else
            {
                childMove otherMove = other.gameObject.GetComponent<childMove>();
                Vector3 cross = Vector3.Cross(otherMove.normalDeplacement.normalized,normalDeplacement.normalized);
                transform.LookAt(transform.position + cross);
                targetPosition = transform.position + transform.forward * my_moveLenght;
            }
            Debug.Log("tag");
            targetPosition = other.transform.position;
        }
    }
}