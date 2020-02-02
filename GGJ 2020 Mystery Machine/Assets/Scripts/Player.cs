using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public SpriteRenderer playerSpriteRender;
    private bool justPickedUpItem = false;

    private bool _mayControl = true;
    
    private Animator _anim;
    void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(Instance.gameObject);
        }
        Instance = this;
        _anim = GetComponentInChildren<Animator>();
        playerSpriteRender = GetComponentInChildren<SpriteRenderer>();
    }

    // checks for click on walkable area and lets player move towards
    void Update()
    {
        if (heldItem != Item.None) {
            Vector3 mPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            // print(Input.mousePosition+ "," + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            heldItemContainer.transform.position = Camera.main.ScreenToWorldPoint(mPos);
        }
        if (_mayControl) {
            // check if arrived at destination
            if (isWalking) {
                if (Math.Abs(transform.position.x - _targetPos.x) > stoppingDistance) {
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
                    _anim.SetBool("isWalking", isWalking);
                }
            }
        }
    }

    public void HoldItem(Item i, Sprite s) {
        Cursor.visible = false;
        heldItem = i;
        heldItemContainer.sprite = s;
        //justPickedUpItem = true;
    }

    public void PutBackItem() {
        if (!justPickedUpItem) {
            heldItemContainer.sprite = null;
            heldItem = Item.None;
            Cursor.visible = true;
        }
    }

    public void StartWalkingToInteractable(float xPos, IInteractable i, InteractionType ixType) {
        if (!_mayControl) return; 
        _targetInteractionType = ixType;
        _targetInteractable = i;
        _targetPos = new Vector3(xPos, transform.position.y, transform.position.z);
        playerSpriteRender.flipX = _targetPos.x <= transform.position.x;
        isWalking = true;
        _anim.SetBool("isWalking", isWalking);
    }

    public void StartWalkingTo(BaseEventData bed) {
        if (!_mayControl) return; 
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 50);
        if (rayHit.collider != null) {
            _targetPos = new Vector3(rayHit.point.x, transform.position.y, transform.position.z);
            playerSpriteRender.flipX = _targetPos.x <= transform.position.x;
            isWalking = true;
            _anim.SetBool("isWalking", isWalking);
        }
        PutBackItem();
    }
    
    public void TogglePlayerControls() {
        _mayControl = !_mayControl;
    }

    public void SetPlayerControls(bool val) {
        _mayControl = val;
    }
}

public enum InteractionType {
    LookAt, InteractWith, Null
}