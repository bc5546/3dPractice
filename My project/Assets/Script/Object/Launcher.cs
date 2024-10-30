using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private Dictionary<GameObject,float> objects = new Dictionary<GameObject, float>();
    public float launchTime;
    private void OnCollisionEnter(Collision collision)
    {
        objects.Add(collision.gameObject,Time.time);
    }

    private void OnCollisionExit(Collision collision)
    {
        objects.Remove(collision.gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var obj in objects)
        {
            if (Time.time - obj.Value > launchTime)
            {
                obj.Key.GetComponent<Rigidbody>().AddForce(Vector2.up * 160, ForceMode.Impulse);
            }
        }
    }
}
