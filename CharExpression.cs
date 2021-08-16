using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LeftRecursion
{
    public class CharExpression : Expression
    {
        public char Value { get; }

        public CharExpression(char value)
        {
            Value = value;
        }

        public override IEnumerable<string> Run(Context context)
        {
            if (context.Index != context.Input.Length && context.Input[context.Index] == Value)
            {
                context.Index++;
                yield return Value.ToString();
            }
        }

        public override string ToString() => Value.ToString();
    }
}