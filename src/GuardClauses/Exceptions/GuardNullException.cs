using System;

namespace Ardalis.GuardClauses;

public class GuardNullException : ArgumentNullException
{
    public GuardNullException(string key) : base(key)
    {
        key = key;
    }
    
    public GuardNullException(string? message, string? key) 
        : base(key, message)
    {
        Key = key;
    }

    public string? Key { get; set; }
}

