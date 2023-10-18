using System;

namespace Ardalis.GuardClauses;

public class GuardZeroException : ArgumentException
{
    public GuardZeroException(string key) : base(default, key)
    {
        Key = key;
    }
    
    public GuardZeroException(string message, string key) : base(message, key)
    {
        Key = key;
    }

    public string Key { get; set; }
}
