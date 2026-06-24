using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugAndDrop : MonoBehaviour
{
    bool _canMove;
    bool _dragging;
    Collider2D _col;
    Transform _trans;
    Rigidbody2D _rb;
    [SerializeField] GameObject _button;
    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        _trans = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _canMove = false;
        _dragging = false;

    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



        if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {
            if (_col == Physics2D.OverlapPoint(mousePos))
            {
                _canMove = true;
            }
            else
            {
                _canMove = false;
            }
            if (_canMove)
            {
                _dragging = true;
            }


        }
        if (_dragging)
        {
            this.transform.position = mousePos;
            _rb.velocity = Vector2.zero;
            _button.SetActive(false);
        }
        if (Input.GetMouseButtonUp(0) || Input.touchCount == 1)
        {
            _canMove = false;
            _dragging = false;
            _button.SetActive(true);
        }
    }



}
