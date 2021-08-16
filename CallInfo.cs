using System.Collections.Generic;

namespace LeftRecursion
{
    public class CallInfo
    {
        public string Name { get; }
        public int Index { get; }
        public Stack<string> RecursiveResult { get; } = new();

        public CallInfo(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }
}