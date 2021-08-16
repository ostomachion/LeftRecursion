using System.Collections.Generic;

namespace LeftRecursion
{
    public class CallInfo
    {
        public string Name { get; }
        public int Index { get; }
        public string? RecursiveResult { get; set; } = null;

        public CallInfo(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }
}