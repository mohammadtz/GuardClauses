using System;

namespace Ardalis.GuardClauses;

public class GuardInvalidException : GuardNullException
{
    public GuardInvalidException(string key) : base(default, key)
    {
        Key = key;
    }
    
    public GuardInvalidException(string message, string key) : base(message, key)
    {
        Key = key;
    }

    public string Key { get; set; }
}
