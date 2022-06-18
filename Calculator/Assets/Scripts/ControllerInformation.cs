using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControllerInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInformation;
    private char _lastChar;
    
    public void AcceptInformation(Text newTextInformation)
    {
        Display(CheckInformation(newTextInformation));
    }
    public void AllClear()
    {
        _textInformation.text = string.Empty;
    }
    public void ClearLastChar()
    {
        if (_textInformation.text.Length == 0) return;
        _textInformation.text = _textInformation.text.Remove(_textInformation.text.Length - 1);
        if (_textInformation.text.Length - 1 >= 0)
            _lastChar = _textInformation.text[_textInformation.text.Length - 1];
    }
    private void Display(string text)
    {
        if (text != string.Empty)
            _lastChar = char.Parse(text);
        _textInformation.text += text;
    }
    private string CheckInformation(Text newTextInformation)
    {
        char charOperation;
        if (!char.IsDigit(char.Parse(newTextInformation.text)))
        {
            if (char.TryParse(newTextInformation.text, out charOperation))
            {
                if ((charOperation == '(' || charOperation == '-')) return charOperation.ToString();
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
    private bool IsArc(char ñ)
    {
        if (("()").IndexOf(ñ) != -1)
            return true;
        return false;
    }
}
