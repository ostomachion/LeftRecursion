using System.Linq;
using System;
using LeftRecursion;

var context = new Context("x?!");

// <f> = <f> ('?' | '!') | 'x'
context.Functions.Add("f",
    new AltExpression(
        new ConcatExpression(
            new CallExpression("f"),
            new AltExpression(
                new CharExpression('?'),
                new CharExpression('!'))),
        new CharExpression('x')));

var expression = new CallExpression("f");
var results = expression.Run(context).ToList();
if (results.Any())
{
    foreach (var result in results)
    {
        Console.WriteLine(result);
    }
}
else
{
    Console.WriteLine("No Match.");
}