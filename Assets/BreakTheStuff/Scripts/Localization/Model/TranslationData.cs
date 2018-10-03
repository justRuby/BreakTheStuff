using System;
using UnityEngine;

namespace BreakTheStuff.Localization.Models
{
    [Serializable]
    public class TranslationData
    {
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }

        [SerializeField] string _name;
        [SerializeField] string _value;
    }
}
