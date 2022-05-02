using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometriqueMove : MonoBehaviour
{
    public int my_angles;

    public int my_moveLenght;

    public int my_speed;
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
            if (rotationDone == my_angles)
            {
                return;
            }
            transform.Rotate(0,360/my_angles,0,Space.Self);
            targetPosition = transform.position + transform.forward * my_moveLenght;
            rotationDone += 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, my_speed * Time.deltaTime);
        clock += Time.deltaTime;
        if (clock > 2)
        {
            clock = 0;
            Instantiate(child,this.transform.position,this.transform.rotation);
        }
    }
}
