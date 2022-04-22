using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometriqueMove : MonoBehaviour
{
    public int angles;

    public int moveLenght;

    public int speed;
    public GameObject child;

    private float clock;
    private int rotationDone = 0;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        clock = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == transform.position)
        {
            if (rotationDone == angles)
            {
                return;
            }
            transform.Rotate(0,360/angles,0,Space.Self);
            targetPosition = transform.position + transform.forward * moveLenght;
            rotationDone += 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        clock += Time.deltaTime;
        if (clock > 2)
        {
            clock = 0;
            Instantiate(child,this.transform.position,this.transform.rotation);
        }
    }
}
