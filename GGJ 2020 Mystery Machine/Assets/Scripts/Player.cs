using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public static Player Instance;

    [SerializeField]    private bool isWalking;

    [SerializeField]
    private Item heldItem = Item.None;

    public SpriteRenderer heldItemContainer;

    public float movementSpeed = 5f;
    public float stoppingDistance = 0.5f;

    private Vector3 _targetPos;
    private IInteractable _targetInteractable;
    private InteractionType _targetInteractionType = InteractionType.Null;
    private SpriteRenderer _sr;
    private bool justPickedUpItem = false;

    void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(Instance.gameObject);
        }
        Instance = this;
        
        _sr = GetComponent<SpriteRenderer>();
        PutBackItem();
    }

    // checks for click on walkable area and lets player move towards
    void Update()
    {
        if (heldItem != Item.None) {
            Vector3 mPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            // print(Input.mousePosition+ "," + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            heldItemContainer.transform.position = Camera.main.ScreenToWorldPoint(mPos);
        }
        bool leftClick = Input.GetMouseButtonUp(0);
        bool rightClick = Input.GetMouseButtonUp(1);

        if (leftClick || rightClick) {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, LayerMask.GetMask("Interactable"));
            if (rayHit.collider != null) {
                IInteractable interactable = rayHit.collider.GetComponent<IInteractable>();
                _targetInteractable = interactable;
                if (leftClick) _targetInteractionType = InteractionType.InteractWith;
                else if (rightClick) _targetInteractionType = InteractionType.LookAt;
                StartWalking(interactable.GetTransform().position.x);
            } else {
                if (heldItem != Item.None && !justPickedUpItem) {
                    PutBackItem();
                }
                rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50, LayerMask.GetMask("WalkableArea"));
                if (rayHit.collider != null) {
                    StartWalking(rayHit.point.x);
                } 
            }

            if (leftClick && justPickedUpItem) {
                justPickedUpItem = false;
            }
        }

        if (isWalking && Vector3.Distance(transform.position, _targetPos) > stoppingDistance) {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * movementSpeed);
        } else {
            if (_targetInteractable != null) {
                if (_targetInteractionType == InteractionType.LookAt) {
                    _targetInteractable.LookAt();
                } else if (_targetInteractionType == InteractionType.InteractWith) {
                    _targetInteractable.Interact(heldItem);
                }
                _targetInteractionType = InteractionType.Null;
                _targetInteractable = null;
            }
            this.isWalking = false;
        }
    }

    public void HoldItem(Item i, Sprite s) {
        heldItem = i;
        print(s);
        heldItemContainer.sprite = s;
        justPickedUpItem = true;
    }

    public void PutBackItem() {
        if (!justPickedUpItem) {
            heldItemContainer.sprite = null;
            heldItem = Item.None;
        }
    }


    public void StartWalking(float xPos) {
        _targetPos = new Vector3(xPos, transform.position.y, transform.position.z);
        _sr.flipX = _targetPos.x <= transform.position.x;
        isWalking = true;
    } 
}

public enum InteractionType {
    LookAt, InteractWith, Null
}