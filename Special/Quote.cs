// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
	public Quote() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printQuote(t, n, p);
        }
		
		public override Node eval(Node exp, Environment env)
		{
			int num = Util.expLength(exp);
			if (num != 2)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			return exp.getCdr().getCar();
		}
    }
}

