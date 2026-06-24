using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu.Data;
using TMPro;
using UnityEngine.UI;

namespace Menu.View
{
    public class Say : MonoBehaviour
    {
        [SerializeField] public Dialogs _dialogs;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _sprite;
        [SerializeField] private GameObject _potapo;

        public int Index;

        private void Start()
        {
            _dialogs.Get[6].Mission = false;
            _dialogs.Get[8].Mission = false;
            if (_dialogs != null) NextDialog();
            _potapo.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _dialogs.Get[6].Mission = true;
                _dialogs.Get[8].Mission = true;
            }

            if(Index >= 62) _potapo.SetActive(true);

        }

        public void NextDialog()
        {


            if (Index == _dialogs.Get.Length) return;

            _name.SetText(_dialogs.Get[Index].Name);
            _text.SetText(_dialogs.Get[Index].Text);
            _sprite.color = new Color(_dialogs.Get[Index].Red, _dialogs.Get[Index].Green, _dialogs.Get[Index].Blue, _dialogs.Get[Index].Alfa);
            Debug.Log("_dialogs.Get[Index].Mission = " + _dialogs.Get[Index].Mission);

            if (_dialogs.Get[Index].Mission)
                Index++;
        }
    }
}
