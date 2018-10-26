using System;
namespace BSBingoApi.Model
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
