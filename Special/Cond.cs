// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            Printer.printCond(t, n, p);
        }
		
		private Node evalExp(Node exp, Environment env){
			//we have to do something here			
		}
		
		public override Node eval(Node exp, Environment env)
		{
			int num = Util.expLength(exp);
			if (num < 2)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			return this.evalExp(exp.getCdr(), env);
		}
    }
}


