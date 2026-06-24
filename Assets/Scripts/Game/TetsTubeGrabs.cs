using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu.View;
using UnityEngine;

public class TetsTubeGrabs : MonoBehaviour
{

    [SerializeField] Say _say;
    [SerializeField] GameObject _testTube;
    [SerializeField] GameObject _startPos;
    [SerializeField] Menumanadger _menuMan;
    [SerializeField] GameObject _prefab;
    public GameObject[] Poses;
    public bool[] IsPosFree;
    int _index;
    bool _bool = true;

    bool _canMove;
    bool _dragging;
    Collider2D _col;
    Rigidbody2D _rb;
    Transform _trans;
    [SerializeField] GameObject _button;

    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _trans = GetComponent<Transform>();
        _canMove = false;
        _dragging = false;

    }

    void Update()
    {
        if (!IsPosFree.Last())
        {
            _say._dialogs.Get[6].Mission = true;

            if (_bool)
            {
                _trans.localPosition = new Vector3(-19, 6, 3);
                _trans.localScale = new Vector3(0.15f, 0.15f);
                _bool = false;
                _menuMan.TableOpenClose(false);
            }

        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (!IsPosFree.Last())
        {
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



    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TestTube") && Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < IsPosFree.Length; i++)
            {
                if (IsPosFree[i] == true)
                {
                    _index = i;
                    break;
                }

            }

            if (IsPosFree[_index])
            {
                IsPosFree[_index] = false;
                GameObject testTube = Instantiate(_prefab, Poses[_index].transform);
                testTube.transform.localPosition = Vector3.zero;
                testTube = Instantiate(_testTube, _startPos.transform);
                testTube.transform.localPosition = Vector3.zero;
                _testTube = testTube;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TestTube") && Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < IsPosFree.Length; i++)
            {
                if (IsPosFree[i] == true)
                {
                    _index = i;
                    break;
                }

            }

            if (IsPosFree[_index])
            {
                IsPosFree[_index] = false;
                GameObject testTube = Instantiate(_testTube, Poses[_index].transform);
                testTube.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                testTube.GetComponent<BoxCollider2D>().enabled = false;
                testTube.transform.localPosition = Vector3.zero;
                testTube = Instantiate(_testTube, _startPos.transform);
                testTube.transform.localPosition = Vector3.zero;
                _testTube = testTube;
                Destroy(collision.gameObject);
            }
        }

    }


}
