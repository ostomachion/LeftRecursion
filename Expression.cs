using System;
using System.Collections.Generic;
using System.Linq;

namespace LeftRecursion
{
    public abstract class Expression
    {
        public abstract IEnumerable<string> RunInternal(Context context);

        public IEnumerable<string> Run(Context context)
        {
            Begin(context);
            foreach (var result in RunInternal(context))
            {
                End(context);
                yield return result;
                Begin(context);
            }
            End(context);
        }

        private void Begin(Context context)
        {
            if (this is CallExpression c)
            {
                context.CallStack.Push(new CallInfo { Name = c.Name, Index = context.Index });
            }

            if (context.CallStack.Count > 1000)
                throw new Exception("Nope.");
        }

        private void End(Context context)
        {
            if (this is CallExpression)
            {
                context.CallStack.Pop();
            }
        }
    }
}