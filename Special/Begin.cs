// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }
		
		//added this
		public override Node eval(Node exp, Environment env)
		{
			int num = Util.expLength(exp);
			if (num < 2)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			return Util.begin(exp.getCdr(), env);
		}
		
    }
}

