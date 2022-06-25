using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Calculator : MonoBehaviour
{
    private ControllOperators controllOperators;
    private double result = 0;
    private string output = string.Empty;
    private Stack<char> operStack = new Stack<char>();
    private Stack<double> temp = new Stack<double>();
    private string currentNumber = string.Empty;
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
        output = string.Empty;
        operStack = new Stack<char>();
        try
        {
            MakeExpression(input);
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";
            return output;
        }
        catch
        {
            return "Вираз невірний!!!";
        }
    }
    private void MakeExpression(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (IsDelimeter(input[i])) continue;
            i = AllDistribute(input, i);
        }
    }
    private int AllDistribute(string input, int i)
    {
        int y = i;
        y = DistributeNumbers(input, y);
        DistributeOperators(input[i]);
        return y;
    }
    private int DistributeNumbers(string input, int i)
    {
        if (Char.IsDigit(input[i]) || input[i] == '-')
        {
            while (!IsDelimeter(input[i]) && !controllOperators.IsChar(input[i]))
            {
                output += input[i];
                i++;
                if (i == input.Length) break;
            }
            i--;
        }   
        return i;
    }
    private void DistributeOperators(char c)
    {
        if (controllOperators.IsChar(c))
        {
            if (c == '(')
                operStack.Push(c);
            else
                    if (c == ')')
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
                    if (GetPriority(c) <= GetPriority(operStack.Peek()))
                        output += operStack.Pop().ToString();
                operStack.Push(char.Parse(c.ToString()));
            }
            output += " ";
        }
    }

    private string Counting(string input)
    {
        result = 0;
        temp = new Stack<double>();
        try
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]) || input[i] == '-')
                    i = WriteNumbers(input, i);
                else if (controllOperators.IsChar(input[i]))
                    CountingTwoNumbers(input[i]);
            }
            return temp.Peek().ToString();
        }
        catch
        {
            return "Вираз невірний!!!";
        }
    }
    private int WriteNumbers(string input, int i)
    {
        currentNumber = string.Empty;

        while (!IsDelimeter(input[i]) && !controllOperators.IsChar(input[i]))
        {
            currentNumber += input[i];
            i++;
            if (i == input.Length) break;
        }
        temp.Push(double.Parse(currentNumber));
        i--;
        return i;
    }
    private void CountingTwoNumbers(char c)
    {
        double a = temp.Pop();
        double b = temp.Pop();

        switch (c)
        {
            case '+': result = b + a; break;
            case '*': result = b * a; break;
            case '/': result = b / a; break;
            case '%': result = b * (a / 100); break;
            case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
        }
        temp.Push(result);
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
