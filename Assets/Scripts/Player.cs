using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         XRDevice.SetTrackingSpaceType (TrackingSpaceType.RoomScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
