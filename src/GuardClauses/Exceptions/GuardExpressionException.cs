using System;

namespace Ardalis.GuardClauses;

public class GuardExpressionException : ArgumentException
{
    public GuardExpressionException(string key) : base(default, key)
    {
        Key = key;
    }
    
    public GuardExpressionException(string message, string key) : base(message, key)
    {
        Key = key;
    }

    public string Key { get; set; }
}
