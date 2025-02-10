using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidibody2D;
    private Animator _playerAnimator;
    public float _playerSpeed;
    private Vector2 _playerDirection;
    public bool canMove = true;

    void Start()
    {
        _playerRigidibody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(canMove);
        if (!canMove)
        {
            _playerDirection = Vector2.zero;
            _playerAnimator.SetInteger("MovimentoH", 0);
            return;
        }
        float valueHorizontal = Input.GetAxisRaw("Horizontal");
        float valueVertical = Input.GetAxisRaw("Vertical");
        _playerDirection = new Vector2(valueHorizontal, valueVertical);
        if(valueHorizontal != 0 || valueVertical != 0){
            if(valueHorizontal != 0 || valueVertical != 0){
                _playerAnimator.SetInteger("MovimentoH", 1);    
            }
        }else{
            _playerAnimator.SetInteger("MovimentoH", 0);
        }
        Flip();
    }
    void FixedUpdate(){
        if(canMove){
            _playerRigidibody2D.MovePosition(_playerRigidibody2D.position + _playerDirection * _playerSpeed * Time.fixedDeltaTime);
        }
    }
    void Flip(){
        if(_playerDirection.x > 0){
            transform.eulerAngles = new Vector2(0f, 0f);
        }else if(_playerDirection.x < 0){
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
}
