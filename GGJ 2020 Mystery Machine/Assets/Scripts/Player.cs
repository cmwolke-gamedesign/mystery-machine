using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public static Player Instance;

    [SerializeField]
    private bool isWalking;

    public SpriteRenderer heldItemContainer;
    [SerializeField]
    private InventoryItem? heldItem;



    public float movementSpeed = 5f;
    public float stoppingDistance = 0.5f;

    private Vector3 _targetPos;
    private IInteractable _targetInteractable;
    private SpriteRenderer _sr;
    void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(Instance.gameObject);
        }
        Instance = this;
        
        _sr = GetComponent<SpriteRenderer>();
    }

    // checks for click on walkable area and lets player move towards
    void Update()
    {
        if (heldItem != null) {
            heldItemContainer.transform.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) {
            
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, LayerMask.GetMask("Interactable"));
            if (rayHit.collider != null) {
                IInteractable interactable = rayHit.collider.GetComponent<IInteractable>();
                print("walking to " + interactable.GetTransform().name);
                _targetInteractable = interactable;
                StartWalking(interactable.GetTransform().position.x);
            } else {
                rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, LayerMask.GetMask("WalkableArea"));
                if (rayHit.collider != null) {
                    StartWalking(rayHit.point.x);
                } else {
                    if (heldItem != null) {
                        PutBackItem();
                    }
                }
            }

        }

        if (isWalking && Vector3.Distance(transform.position, _targetPos) > stoppingDistance) {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * movementSpeed);
        } else {
            if (_targetInteractable != null) {
                _targetInteractable.Interact();
                _targetInteractable = null;
            }
            this.isWalking = false;
        }
    }

    public void HoldItem(InventoryItem i, Sprite s) {
        heldItem = i;
        heldItemContainer.sprite = s;
        heldItemContainer.enabled = true;
    }

    public void PutBackItem() {
        heldItemContainer.sprite = null;
        heldItemContainer.enabled = false;
        heldItem = null;
    }


    private void StartWalking(float xPos) {
        _targetPos = new Vector3(xPos, transform.position.y, transform.position.z);
        _sr.flipX = _targetPos.x <= transform.position.x;
        isWalking = true;
    } 
}
