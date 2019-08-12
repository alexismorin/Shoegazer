using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {
    Animator animator;
    [SerializeField]
    Transform playerLocation;

    void Start () {
        animator = GetComponent<Animator> ();
        animator.SetFloat ("Offset", Random.Range (0.0f, 1.0f));
    }

    void OnAnimatorIK () {

        animator.SetLookAtWeight (1f);
        animator.SetLookAtPosition (playerLocation.position);

    }

}