using System;
using TMPro;
using UnityEngine;


namespace Menu.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(Dialogs))]
    public class Dialogs : ScriptableObject
    {

        [System.Serializable]
        public class Dialog
        {
            [SerializeField] private string _name;
            [SerializeField][TextArea(minLines: 5, maxLines: 10)] private string _text;

            public string Name => _name;
            public string Text => _text;
            public float Red;
            public float Green;
            public float Blue;
            public float Alfa;
            public bool Object = false;
            public int NumberObject;
            public bool Mission = true;

        }

        [SerializeField] private Dialog[] _dialogs;

        public Dialog[] Get => _dialogs;
    }
}
