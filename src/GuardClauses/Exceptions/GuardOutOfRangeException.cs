using System;

namespace Ardalis.GuardClauses;

public class GuardOutOfRangeException : ArgumentOutOfRangeException
{
    public GuardOutOfRangeException(string? message,string key) : base(key, message ?? default)
    {
        Key = key;
    }
    
    public string Key { get; set; }
}

