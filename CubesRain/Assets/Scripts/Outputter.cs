using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Outputter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public int Value { get; private set; }

    public void SetValue(int newValue) 
    {
        _text.text = newValue.ToString();
    }
}
