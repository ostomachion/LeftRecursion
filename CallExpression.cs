using System.Net.Mime;
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

            var index = context.Index;

            var function = context.Functions[Name];

            // Check for left-recursion.
            var caller = context.CallStack.FirstOrDefault(x => x.Name == Name);
            if (caller?.Index == context.Index)
            {
                if (caller.RecursiveResult.Any())
                    yield return caller.RecursiveResult.Peek();

                yield break;
            }

            context.CallStack.Push(new(Name, context.Index));
            if (caller is not null)
            {
                foreach (var result in caller.RecursiveResult.Reverse())
                {
                    context.CallStack.Peek().RecursiveResult.Push(result);
                }
            }

            var stack = new Stack<IEnumerator<string>>();
            stack.Push(function.Run(context).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    var result = stack.Peek().Current;

                    context.CallStack.Peek().RecursiveResult.Push(result);
                    stack.Push(function.Run(context).GetEnumerator());
                }
                else
                {
                    stack.Pop();
                    if (stack.Any())
                    {
                        var head = context.CallStack.Pop();
                        head.RecursiveResult.TryPeek(out var prevResult);
                        var originalIndex = context.Index;
                        while (head.RecursiveResult.TryPop(out string? result))
                        {
                            context.Index += result.Length - prevResult!.Length;
                            yield return result;
                        }
                        context.Index = originalIndex;
                        context.CallStack.Push(head);
                    }
                }
            }
            context.CallStack.Pop();
        }

        public override string ToString() => $"<{Name}>";
    }
}