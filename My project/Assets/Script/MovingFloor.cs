using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 end= new Vector3(8,0,13.34f);
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == end)
        {
            end = new Vector3(end.x*-1, 0, 13.34f);
        } 
        gameObject.transform.position = Vector3.MoveTowards(transform.position, end, moveSpeed);
    }
}
