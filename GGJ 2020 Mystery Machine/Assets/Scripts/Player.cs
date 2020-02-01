using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private bool isWalking;

    public float movementSpeed = 5f;

    private Vector3 _targetPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // layer 9 is "WalkableArea"!
            print(ray.origin + ", " + ray.direction);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100)) {
                print(hit.point);
                _targetPos = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                isWalking = true;
            }
        }

        if (isWalking && Math.Abs(_targetPos.x - transform.position.x) > 1) {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * movementSpeed);
        } else {
            this.isWalking = false;
        }
    }
}
