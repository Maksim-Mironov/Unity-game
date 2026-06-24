using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu.Data;
using TMPro;
using UnityEngine.UI;

namespace Menu.View
{
    public class SayTutorial : MonoBehaviour
    {
        [SerializeField] public Dialogs _dialogs;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _sprite;
        [SerializeField] private GameObject _tutorial;
        [SerializeField] private GameObject _tutorialWindow;
        [SerializeField] private GameObject _tutorialSettings;
        [SerializeField] private GameObject _tutorialArticle;
        [SerializeField] private GameObject _tutorialTableOpen;
        [SerializeField] private GameObject _music;
        [SerializeField] private GameObject _tv;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private GameObject _pana;
        [SerializeField] private GameObject _dragObjects;
        [SerializeField] private bool _bool;

        public int Index;

        private void Start()
        {
            _bool = true;
            if (_dialogs != null) NextDialog();
            _tutorialSettings.SetActive(false);
            _tutorialArticle.SetActive(false);
            _tutorialWindow.SetActive(false);
            _tutorialTableOpen.SetActive(false);
            _music.SetActive(false);
            _tv.SetActive(false);
            _buttons.SetActive(false);
            _pana.SetActive(false);
            _dragObjects.SetActive(false);
        }

        public void NextDialog()
        {

            if (Index == _dialogs.Get.Length) return;

            _text.SetText(_dialogs.Get[Index].Text);
            _sprite.color = new Color(_dialogs.Get[Index].Red, _dialogs.Get[Index].Green, _dialogs.Get[Index].Blue, _dialogs.Get[Index].Alfa);
            if (_dialogs.Get[Index].Object == true)
            {
                if (_dialogs.Get[Index].NumberObject == 1) _tutorialWindow.SetActive(true);
                if (_dialogs.Get[Index].NumberObject == 2)
                {
                    _tutorialSettings.SetActive(true);
                    _tutorialWindow.SetActive(false);
                }
                if (_dialogs.Get[Index].NumberObject == 3)
                {
                    _tutorialArticle.SetActive(true);
                    _tutorialSettings.SetActive(false);
                }
                if (_dialogs.Get[Index].NumberObject == 5)
                {
                    _tutorialArticle.SetActive(false);
                    _music.SetActive(true);
                }
                if (_dialogs.Get[Index].NumberObject == 4)
                {
                    _tutorialTableOpen.SetActive(true);
                    _music.SetActive(false);
                }
                if (_dialogs.Get[Index].NumberObject == 6)
                {
                    _tutorialTableOpen.SetActive(false);
                    _tv.SetActive(true);
                }
                if (_dialogs.Get[Index].NumberObject == 7)
                {
                    _buttons.SetActive(true);
                }
                if (_dialogs.Get[Index].NumberObject == 8)
                {
                    _buttons.SetActive(false);
                    _pana.SetActive(true);
                   if(_bool == true) _tv.SetActive(false);
                }
                if (_dialogs.Get[Index].NumberObject == 9)
                {
                    _dragObjects.SetActive(true);
                    _pana.SetActive(false);
                }
                if (_dialogs.Get[Index].NumberObject == -1) Destroy(_tutorial);
            }
            Debug.Log("_dialogs.Get[Index].Mission = " + _dialogs.Get[Index].Mission);

            if (_dialogs.Get[Index].Mission)
                Index++;
        }

        public void SetBool()
        {
            _bool = false;
        }

    }
}
