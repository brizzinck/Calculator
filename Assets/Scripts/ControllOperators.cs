using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllOperators
{
    public string EditingOperators(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '-')
            {
                input = MinusOperator(input, i);
                i++;
            }
            else if (input[i] == '(' || input[i] == ')')
            {
                input = ArcOperator(input, i);
                input = ChangingCharactersArc(input, i);
            }
        }
        return input;
    }
    public bool IsChar(char ñ, string chars = "+/*^()%")
    {
        if (chars.IndexOf(ñ) != -1)
            return true;
        return false;
    }
    private string MinusOperator(string input, int i)
    {
        int y = i--;
        if (y <= 0) return input;
        if (!IsChar(input[i], "(+/*^%"))
        {
            input = input.Insert(y, "+");
        }
        return input;
    }
    private string ArcOperator(string input, int i)
    {
        int y = i;
        if (input[i] == '(') y = i - 1;
        if (input[i] == ')') y = i + 1;
        if (y >= input.Length) return input;
        else if (y < 0 || IsChar(input[y++], "-+/*^%")) return input;
        else input = input.Insert(y, "*");
        return input;
    }
    private string ChangingCharactersArc(string input, int i)
    {
        if (i - 1 <= 0) return input;
        else if (input[i] == '(' && input[i - 1] == '-')
        {
            input = input.Remove(i - 1, 1);
            for (int y = i - 1; y < input.Length; y++)
            {
                if (IsChar(input[y], "(+/*^%"))
                {
                    input = input.Insert(y + 1, "-");
                    y++;
                }
                else if (input[y] == '-')
                {
                    input = input.Remove(y, 1);
                    input = input.Insert(y, "+");
                }
                else if (input[y] == ')') break;
            }
        }
        return input;
    }
}