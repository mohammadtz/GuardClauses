using System;

namespace Ardalis.GuardClauses;

public class GuardNotFoundException : Exception
{
    public GuardNotFoundException(string key, string objectName)
        : base($"Queried object {objectName} was not found, Key: {key}")
    {
        Key = key;
    }
    
    public GuardNotFoundException(string key, string objectName, Exception innerException)
        : base($"Queried object {objectName} was not found, Key: {key}", innerException)
    {
        Key = key;
    }

    public string Key { get; set; }
}
