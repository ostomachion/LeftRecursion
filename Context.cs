using System.Linq;
using System.Collections.Generic;
namespace LeftRecursion
{
    public enum State
    {
        Normal,
        LeftRecursive,
    }

    public class Context
    {
        public string Input { get; }
        public int Index { get; set; }
        public State State { get; set; } = State.Normal;
        public Stack<CallInfo> CallStack { get; } = new();
        public Dictionary<string, Expression> Functions { get; } = new();
        
        public string? RecursiveResult = null;

        public Context(string input)
        {
            Input = input;
        }
    }
}