using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childMove : MonoBehaviour
{
    public int moveLenght;
    public int speed;
    private Vector3 targetPosition;

// Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0,90,0,Space.Self);
        targetPosition = transform.position + transform.forward * moveLenght;
    }

// Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}