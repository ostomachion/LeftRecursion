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

            // Check for left-recursion.
            var caller = context.CallStack.FirstOrDefault(x => x.Name == Name);
            if (caller?.Index == context.Index)
            {
                if (caller.RecursiveResult is null)
                {
                    yield break;
                }
                else
                {
                    yield return caller.RecursiveResult;
                    yield break;
                }
            }

            context.CallStack.Push(new(Name, context.Index) { RecursiveResult = caller?.RecursiveResult });

            var stack = new Stack<IEnumerator<string>>();
            stack.Push(function.Run(context).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    var result = stack.Peek().Current;

                    context.CallStack.Peek().RecursiveResult = result; // TODO: Make RecursiveResult a stack?
                    stack.Push(function.Run(context).GetEnumerator());
                }
                else
                {
                    stack.Pop();
                    if (stack.Any())
                    {
                        context.CallStack.Pop();
                        var result = stack.Peek().Current;
                        yield return result;
                        context.CallStack.Push(new(Name, context.Index));
                    }
                }
            }
            context.CallStack.Pop();
        }

        public override string ToString() => $"<{Name}>";
    }
}