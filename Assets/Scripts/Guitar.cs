using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
{
    [SerializeField]
    bool leftHanded = true;
    [SerializeField]
    GameObject noteRoot;
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    AudioSource lowNoteAudioSource;
    [SerializeField]
    AudioSource highNoteAudioSource;

    GameObject pickHand;
    GameObject noteHand;
    GameObject guitarRestingPosition;
    Vector3 velocity = Vector3.zero;

    float lastPickVelocity;
    float pickVelocity;
    Vector3 oldPickTransform;
    Vector3 newPickTransform;

    bool isHoldingLowString;
    bool isHoldingHighString;


    void Start()
    {
        HandOrientation();
    }


    void HandOrientation()
    {
        if (leftHanded)
        {
            pickHand = leftHand;
            noteHand = rightHand;
        }
        else
        {
            pickHand = rightHand;
            noteHand = leftHand;
        }

        guitarRestingPosition = pickHand.GetComponent<Hand>().guitarRestPosition;
        oldPickTransform = pickHand.transform.position;
    }

    void PlayNote()
    {


        if (isHoldingLowString)
        {
            lowNoteAudioSource.Stop();
            lowNoteAudioSource.Play();
        }
        if (isHoldingHighString)
        {
            highNoteAudioSource.Stop();
            highNoteAudioSource.Play();
        }
    }

    void FixedUpdate()
    {
        transform.LookAt(noteHand.transform.position);


    }

    void Update()
    {
        lastPickVelocity = pickVelocity;
        newPickTransform = pickHand.transform.position;
        Vector3 pickMedia = (newPickTransform - oldPickTransform);
        pickVelocity = pickMedia.magnitude / Time.deltaTime;
        oldPickTransform = newPickTransform;
        newPickTransform = pickHand.transform.position;


        if (leftHanded == true)
        {

            //  high note
            if (Input.GetAxis("XRI_Right_Trigger") > 0.5f)
            {
                isHoldingHighString = true;
                highNoteAudioSource.pitch = -2.39f * Vector3.Distance(noteHand.transform.position, noteRoot.transform.position) + 3f;
            }

            if (Input.GetButtonUp("XRI_Right_TriggerButton"))
            {
                isHoldingHighString = false;
                //    StopHighString();
            }

            //  low note
            if (Input.GetAxis("XRI_Right_Grip") > 0.5f)
            {
                isHoldingLowString = true;
                lowNoteAudioSource.pitch = -2.39f * Vector3.Distance(noteHand.transform.position, noteRoot.transform.position) + 2f;
            }

            if (Input.GetButtonUp("XRI_Right_GripButton"))
            {
                isHoldingLowString = false;
                //    StopLowString();
            }

            // Actual Note Playing
            if (Input.GetButton("XRI_Left_GripButton"))
            {
                transform.position = Vector3.SmoothDamp(transform.position, guitarRestingPosition.transform.position, ref velocity, 0.2f * Time.deltaTime);
                if (Input.GetButtonDown("XRI_Left_SecondaryButton"))
                {
                    PlayNote();
                }
            }
        }

    }

    void StopLowString()
    {
        lowNoteAudioSource.Stop();
    }
    void StopHighString()
    {
        highNoteAudioSource.Stop();
    }
}
