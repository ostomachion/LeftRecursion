using System;
using System.Collections.Generic;
using System.Linq;

namespace LeftRecursion
{
    public class ConcatExpression : Expression
    {
        public IEnumerable<Expression> Expressions { get; }

        public ConcatExpression(params Expression[] expressions)
        {
            Expressions = expressions;
        }

        public override IEnumerable<string> Run(Context context)
        {
            if (!Expressions.Any())
            {
                yield return "";
                yield break;
            }

            var stack = new Stack<IEnumerator<string>>();
            stack.Push(Expressions.First().Run(context).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == Expressions.Count())
                    {
                        yield return String.Join("", stack.Reverse().Select(x => x.Current));
                    }
                    else
                    {
                        stack.Push(Expressions.ElementAt(stack.Count).Run(context).GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();
                }
            }
        }

        public override string ToString() => String.Join("", Expressions.Select(x => x.ToString()));
    }
}