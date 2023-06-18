using System;
using System.ComponentModel;

namespace Ardalis.GuardClauses;

public class GuardEnumOutOfRangeException : InvalidEnumArgumentException
{
    public GuardEnumOutOfRangeException(string message) : base(message)
    {
    }

    public GuardEnumOutOfRangeException(string key, int input, Type type) : base(key, input, type)
    {
        Key = key;
    }

    public string Key { get; set; }
}

