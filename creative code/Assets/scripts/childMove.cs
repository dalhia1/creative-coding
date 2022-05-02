using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childMove : MonoBehaviour
{
    public int my_moveLenght;
    public int my_speed;
    private Vector3 targetPosition;
    private int storedSpeed;

// Start is called before the first frame update
    void Start()
    {
        storedSpeed = my_speed;
        my_speed = 0;
        // rotate toward center modulo random
        transform.Rotate(0,90,0,Space.Self);
        targetPosition = transform.position + transform.forward * my_moveLenght;
    }

// Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, my_speed * Time.deltaTime);
    }

    void OnEventStart()
    {
        my_speed = storedSpeed;
        
    }
    
}