using System;

namespace Ardalis.GuardClauses;

public class GuardNegativeOrZeroException : ArgumentException
{
    public GuardNegativeOrZeroException(string key) : base("", paramName: key)
    {
        key = key;
    }
    
    public GuardNegativeOrZeroException(string message, string key) : base(message, key)
    {
        Key = key;
    }

    public string Key { get; set; }
}

