using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometriqueMove : MonoBehaviour
{
    public int angles;

    public int moveLenght;

    public int speed;

    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == transform.position)
        {
            transform.Rotate(0,360/angles,0,Space.Self);
            targetPosition = transform.position + transform.forward * moveLenght;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }
}
