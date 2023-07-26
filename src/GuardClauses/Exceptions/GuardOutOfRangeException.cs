using System;

namespace Ardalis.GuardClauses;

public class GuardOutOfRangeException : ArgumentOutOfRangeException
{
    public GuardOutOfRangeException(string? message,string key, string? rangeFrom = null, string? rangeTo = null) 
        : base(key, message ?? default)
    {
        Key = key;
        RangeFrom = rangeFrom;
        RangeTo = rangeTo;
    }
    
    public string Key { get; }
    public string? RangeFrom { get; }
    public string? RangeTo { get; }
}

