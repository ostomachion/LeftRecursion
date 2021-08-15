using System.Linq;
using System.Collections.Generic;
namespace LeftRecursion
{

    public class Context
    {
        public string Input { get; }
        public int Index { get; set; }
        public Stack<CallInfo> CallStack { get; } = new();
        public Dictionary<string, Expression> Functions { get; } = new();

        public Context(string input)
        {
            Input = input;
        }
    }
}