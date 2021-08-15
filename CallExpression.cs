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

        public override IEnumerable<string> Run(Context context)
        {
            if (context.CallStack.Count == 64)
                throw new Exception("Nope.");

            var function = context.Functions[Name];

            // If the same function calls itself before making any progress.
            if (context.CallStack.Any(x => x.Name == Name) && context.CallStack.Peek().Index == context.Index)
            {
                if (context.State == State.Normal)
                {
                    // Trying the function again will lead to a loop.
                    context.State = State.LeftRecursive;
                    yield break;
                }
                else if (context.State == State.LeftRecursive)
                {
                    // Return the stored value.
                    var result = context.RecursiveResult ?? throw new InvalidOperationException("Null.");
                    context.RecursiveResult = null;
                    context.State = State.Normal; // Is this right?
                    yield return result;
                }
                else
                {
                    throw new InvalidOperationException("This shouldn't happen.");
                }
            }

            context.CallStack.Push(new() { Name = Name, Index = context.Index });
            foreach (var result in function.Run(context))
            {
                context.CallStack.Pop();
                if (context.State == State.LeftRecursive)
                {
                    context.RecursiveResult = result;
                    // Recurse.
                    foreach (var innerResult in Run(context))
                    {
                        yield return result;
                    }
                }
                yield return result;
                context.CallStack.Push(new() { Name = Name, Index = context.Index });
            }
            context.CallStack.Pop();
        }

        public override string ToString() => $"<{Name}>";
    }
}