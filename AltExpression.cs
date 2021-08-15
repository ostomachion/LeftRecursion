using System.Linq;
using System;
using System.Collections.Generic;

namespace LeftRecursion
{
    public class AltExpression : Expression
    {
        public IEnumerable<Expression> Expressions { get; }

        public AltExpression(params Expression[] expressions)
        {
            Expressions = expressions;
        }
        
        public override IEnumerable<string> Run(Context context)
        {
            foreach (var expression in Expressions)
            {
                foreach (var result in expression.Run(context))
                {
                    yield return result;
                }
            }
        }

        public override string ToString() => String.Join('|', Expressions.Select(x => x.ToString()));
    }
}