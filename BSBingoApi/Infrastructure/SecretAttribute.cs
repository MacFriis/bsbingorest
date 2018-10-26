using System;
namespace BSBingoApi.Infrastructure
{


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SecretAttribute : Attribute
    {
    }
}
