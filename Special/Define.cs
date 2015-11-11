// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }
		
		//check if valid statement
		private bool checkSymbols(Node parms)
		{
			return parms.isNull() || parms.isSymbol() || (parms.isPair() && parms.getCar().isSymbol() && this.checkSymbols(parms.getCdr()));
		}
		
		public override Node eval(Node exp, Environment env){
			//to do stuff
		}

    }
}


