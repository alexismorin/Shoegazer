using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    float killValue = -3.048f;

    void FixedUpdate () {
        transform.Translate (0f, 0f, 2 * Time.deltaTime);
        if (transform.position.z >= killValue) {
            Destroy (gameObject);
        }
    }
}