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
            
        }

        private void End(Context context)
        {
            
        }
    }
}