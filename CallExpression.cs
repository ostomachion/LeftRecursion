using System.Collections.Generic;

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
            var function = context.Functions[Name];
            foreach (var result in function.Run(context))
            {
                yield return result;
            }
        }

        public override string ToString() => $"<{Name}>";
    }
}