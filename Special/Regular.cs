// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }
		
		
		public override Node eval(Node exp, Environment env)
		{
			int num = Util.expLength(exp);
			if (num < 1)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			Node node = exp.getCar().eval(env);
			Node args = Util.mapEval(exp.getCdr(), env);
			return node.apply(args);
		}
    }
}


