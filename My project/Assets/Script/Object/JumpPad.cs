using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector2.up * 160, ForceMode.Impulse);
        collision.gameObject.GetComponent<PlayerController>().Drop();
    }
}
