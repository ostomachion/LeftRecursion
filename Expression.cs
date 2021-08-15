using System;
using System.Collections.Generic;
using System.Linq;

namespace LeftRecursion
{
    public abstract class Expression
    {
        public abstract IEnumerable<string> Run(Context context);
    }
}