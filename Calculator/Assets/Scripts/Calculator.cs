using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Calculator : MonoBehaviour
{
    private ControllOperators controllOperators;
    public void Сalculate(TextMeshProUGUI textInformation)
    {
        textInformation.text = Calculating(controllOperators.EditingOperators(textInformation.text));
    }
    private void Start()
    {
        controllOperators = new ControllOperators();
    }
    private string Calculating(string input) => Counting(GetExpression(input));
    private string GetExpression(string input)
    {
        string output = string.Empty;
        Stack<char> operStack = new Stack<char>();

        try
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;
                if (Char.IsDigit(input[i]) || input[i] == '-')
                {
                    while (!IsDelimeter(input[i]) && !controllOperators.IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    i--;
                }

                if (controllOperators.IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operStack.Push(input[i]);
                    else
                    if (input[i] == ')')
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString();
                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                    output += " ";
                }
            }
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";
            return output;
        }
        catch
        {
            return "Вираз невірний!!!";
        }
    }
    private string Counting(string input)
    {
        double result = 0;
        Stack<double> temp = new Stack<double>();

        try
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]) || input[i] == '-')
                {
                    string currentNumber = string.Empty;

                    while (!IsDelimeter(input[i]) && !controllOperators.IsOperator(input[i]))
                    {
                        currentNumber += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(currentNumber));
                    i--;
                }
                else if (controllOperators.IsOperator(input[i]))
                {
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i])
                    {
                        case '+': result = b + a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                        case '%': result = b * (a / 100); break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek().ToString();
        }
        catch
        {
            return "Вираз невірний!!!";
        }
    }
    private bool IsDelimeter(char c)
    {
        if ((" ".IndexOf(c) != -1))
            return true;
        return false;
    }
    private byte GetPriority(char c)
    {
        switch (c)
        {
            case '(': return 0;
            case ')': return 1;
            case '+': return 2;
            case '*': return 3;
            case '/': return 3;
            case '%': return 4;
            case '^': return 5;
            default: return 6;
        }
    }
}
