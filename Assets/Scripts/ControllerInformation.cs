using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControllerInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInformation;
    private ControllOperators _controllOperators;
    private char _lastChar;
    private char _lastOperator;

    public void AcceptInformation(Text newTextInformation)
    {
        Display(CheckInformation(newTextInformation));
    }
    public void AllClear()
    {
        _textInformation.text = string.Empty;
        _lastChar = ' ';
        _lastOperator = ' ';
    }
    public void ClearLastChar()
    {
        if (_textInformation.text.Length == 0) return;
        _textInformation.text = _textInformation.text.Remove(_textInformation.text.Length - 1);
        if (_textInformation.text.Length - 1 >= 0)
            _lastChar = _textInformation.text[_textInformation.text.Length - 1];
        SetLastChar();
    }
    private void Start()
    {
        _controllOperators = new ControllOperators();
    }
    private void Display(string text)
    {
        _textInformation.text += text;
        DeleteOtherChar();
    }
    private string CheckInformation(Text newTextInformation)
    {
        SetLastChar();
        char charOperation;
        if (!char.IsDigit(char.Parse(newTextInformation.text)))
        {
            if (char.TryParse(newTextInformation.text, out charOperation))
            {
                if (charOperation == ',' && _lastOperator == ',') return string.Empty;
                else if ((charOperation == '(' || (charOperation == '-' && _lastOperator != '-'))) return charOperation.ToString();
                else if (_textInformation.text.Length != 0)
                {
                    if (!char.IsDigit(_lastChar) && !IsArc(_lastChar) && !IsArc(charOperation))
                    {
                        _textInformation.text = _textInformation.text.Remove(_textInformation.text.Length - 1);
                        newTextInformation.text = charOperation.ToString();
                        return newTextInformation.text;
                    }
                    else return newTextInformation.text;
                }
            }
        }
        else return newTextInformation.text;
        return string.Empty;
    }
    private void SetLastChar()
    {
        for (int i = 0; i < _textInformation.text.Length; i++)
        {
            _lastChar = _textInformation.text[i];
            if (_controllOperators.IsChar(_lastChar, "-+/*^%,"))
                _lastOperator = _lastChar;
        }
    }
    private bool IsArc(char ñ)
    {
        if (("()").IndexOf(ñ) != -1)
            return true;
        return false;
    }
    private void DeleteOtherChar()
    {
        for (int i = 0; i < _textInformation.text.Length; i++)
        {
            if (_controllOperators.IsChar(_textInformation.text[i], "-,+/*^()%0123456789") == false)
            {
                _textInformation.text = _textInformation.text.Remove(i, 1);
                i--;
            }
        }
    }

}