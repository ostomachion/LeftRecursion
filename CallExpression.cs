using System;
using System.Collections.Generic;
using System.Linq;

namespace LeftRecursion
{
    public class CallExpression : Expression
    {
        public string Name { get; }

        public CallExpression(string name)
        {
            Name = name;
        }

        public override IEnumerable<string> RunInternal(Context context)
        {
            if (context.CallStack.Count == 64)
                throw new Exception("Nope.");

            var function = context.Functions[Name];

            // If the same function calls itself before making any progress.
            if (context.CallStack.Any(x => x.Name == Name) && context.CallStack.Peek().Index == context.Index)
            {
                // Trying the function again will lead to a loop.
                yield break;
            }

            context.CallStack.Push(new() { Name = Name, Index = context.Index });
            foreach (var result in function.Run(context))
            {
                context.CallStack.Pop();
                yield return result;
                context.CallStack.Push(new() { Name = Name, Index = context.Index });
            }
            context.CallStack.Pop();
        }

        public override string ToString() => $"<{Name}>";
    }
}