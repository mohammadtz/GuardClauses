using System;

namespace Ardalis.GuardClauses;

public class GuardNegativeException : ArgumentException
{
    public GuardNegativeException(string key) : base("",key)
    {
        key = key;
    }
    
    public GuardNegativeException(string message, string key) : base(message, key)
    {
        Key = key;
    }

    public string Key { get; set; }
}
