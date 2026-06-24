using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu.View;
using Unity.VisualScripting;
using UnityEngine;

public class PANA : MonoBehaviour
{
    [SerializeField] Sprite _panaClose;
    [SerializeField] Sprite _panaOpen;
    [SerializeField] Say _say;
    [SerializeField] Canvas _panaInterface;
    [SerializeField] Canvas _interface;
    [SerializeField] GameObject[] _redButtons;
    Collider2D _col;
    SpriteRenderer _spriteRenderer;
    public int TubesCount = 0;

    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (_col == Physics2D.OverlapPoint(mousePos)) 
                OpenCloseInterface();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _panaOpen;
        collision.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

        if (collision.gameObject.CompareTag("Graber") && Input.GetMouseButtonUp(0))
        {
            Destroy(collision.gameObject);
            TubesCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _panaClose;
        collision.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;

        if (collision.gameObject.CompareTag("Graber") && Input.GetMouseButtonUp(0))
        {
            Destroy(collision.gameObject);
            TubesCount++;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Graber") && Input.GetMouseButtonUp(0))
        {
            Destroy(collision.gameObject);
            TubesCount++;
        }
    }

    public void OpenCloseInterface()
    {
        _panaInterface.enabled = !_panaInterface.enabled;
        _interface.enabled = !_interface.enabled;
    }

    public void StartPANA()
    {
        OpenCloseInterface();
        if (_say._dialogs.Get[6].Mission == true)
        {
            _say._dialogs.Get[8].Mission = true;

            for (int i = 0; i < _redButtons.Length; i++)
            {
                _redButtons[i].gameObject.SetActive(false);
            }

        }
    }
}
