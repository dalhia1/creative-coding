using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeometriqueMove : MonoBehaviour
{
    public int my_angles;

    public int my_moveLenght;

    public int my_speed;
    public GameObject child;
    public GameObject centerObject;

    private float clock;
    private float anglesDegree;
    private int rotationDone = 0;
    private Vector3 targetPosition;
    private Vector3 center;
    private GameObject newChild;
    private UnityEvent m_MyEvent;

    private bool firsttime;
    // Start is called before the first frame update
    void Start()
    {
        anglesDegree = 360/my_angles;
        var rot = Quaternion.AngleAxis((180- anglesDegree)/2 + anglesDegree,Vector3.up);
        // that's a local direction vector that points in forward direction but also 45 upwards.
        var lDirection = rot * Vector3.forward;
        var rot2 = Quaternion.AngleAxis((180- anglesDegree)/2,Vector3.up);
        // that's a local direction vector that points in forward direction but also 45 upwards.
        var lDirection2 = rot2 * Vector3.forward;
        Debug.Log("vect1"+lDirection+"vect2"+lDirection2);
        
        //transform.position = new Vector3(-my_moveLenght, 0, 0); je dois trouver le centre de la figure.
        bool ok = Math3d.LineLineIntersection(out center, transform.position, lDirection,
            transform.position + (Vector3.back * my_moveLenght), lDirection2);
        if (ok)
        {
            Debug.Log("VAR");
            centerObject.transform.position = center;
        }
            
        targetPosition = transform.position;
        
        
        
        clock = 0;
        firsttime = true;
        
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == transform.position)
        {
            if ( rotationDone == my_angles)
            {
                
                if (firsttime &&m_MyEvent != null)
                {
                    firsttime = false;
                    m_MyEvent.Invoke();
                    Debug.Log("event sent");
                }

                return;
            }
            
            transform.Rotate(0,anglesDegree,0,Space.Self);
            targetPosition = transform.position + transform.forward * my_moveLenght;
            rotationDone += 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, my_speed * Time.deltaTime);
        clock += Time.deltaTime;
        if (clock > 2)
        {
            clock = 0;
            newChild = Instantiate(child,this.transform.position,this.transform.rotation);
            childMove compo = newChild.GetComponent<childMove>();
            m_MyEvent.AddListener(compo.OnEventStart);
        }
    }
}
