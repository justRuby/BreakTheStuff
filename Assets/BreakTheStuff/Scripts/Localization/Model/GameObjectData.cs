using System;
using UnityEngine;
using UnityEngine.UI;

namespace BreakTheStuff.Localization.Models
{
    [Serializable]
    public class GameObjectData
    {
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Text Object
        {
            get
            {
                return _object;
            }
        }

        [SerializeField] string _name;
        [SerializeField] Text _object;
    }
}


