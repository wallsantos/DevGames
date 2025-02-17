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
    public GameObject[] Lapis;
    public GameObject[] LapisPreto;
    private int LapisAtivos=3;

    void Start()
    {
        _playerRigidibody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

    }

    void Update()
    {
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
    public void upUpdateLife(bool acao){
        if(!acao){
            int i = 2;
            while(i>0){
                if(Lapis[i].GetComponent<SpriteRenderer>().sortingOrder==10){
                    Lapis[i].GetComponent<SpriteRenderer>().sortingOrder=0;
                    LapisPreto[i].GetComponent<SpriteRenderer>().sortingOrder=10;
                    i=0;
                    LapisAtivos-=1;
                }
                i=i-1;
            }
        }else{
            int i = 1;
            while(i<=2){
                if(Lapis[i].GetComponent<SpriteRenderer>().sortingOrder==0){
                    Lapis[i].GetComponent<SpriteRenderer>().sortingOrder=10;
                    LapisPreto[i].GetComponent<SpriteRenderer>().sortingOrder=0;
                    i=3;
                    LapisAtivos+=1;
                }
                i = i + 1;
            }
        }
    }
    public bool EndGame(){
        if(LapisAtivos==1){
            return true;
        }else{
            return false;
        }
    }
}
