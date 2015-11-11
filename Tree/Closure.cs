// Closure.java -- the data structure for function closures

// Class Closure is used to represent the value of lambda expressions.
// It consists of the lambda expression itself, together with the
// environment in which the lambda expression was evaluated.

// The method apply() takes the environment out of the closure,
// adds a new frame for the function call, defines bindings for the
// parameters with the argument values in the new frame, and evaluates
// the function body.

using System;

namespace Tree
{
    public class Closure : Node
    {
        private Node fun;		// a lambda expression
        private Environment env;	// the environment in which
                                        // the function was defined

        public Closure(Node f, Environment e)	{ fun = f;  env = e; }

        public Node getFun()		{ return fun; }
        public Environment getEnv()	{ return env; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public /* override */ bool isProcedure()	{ return true; }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Procedure");
            if (fun != null)
                fun.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
        public override Node apply (Node args)
        {
            Node car = this.fun.getCdr().getCar();
			Node cdr = this.fun.getCdr().getCdr();
			Environment environment = new Environment(this.env);
			Closure.pairup(car, args, environment);
			return Util.begin(cdr, environment);
        }
		
		//Function for pairing up arguments with corresponding symbols
		private static void pairup(Node parms, Node args, Environment env)
		{
			//if null return nothing
			if (parms.isNull() && args.isNull())
			{
				return;
			}
			//check the symbol type
			if (parms.isSymbol())
			{
				env.define(parms, args);
			}
			//depending on the parameter, check the # of required valid arguments
			else if (parms.isNull() || args.isNull())
			{
				Console.get_Error().WriteLine("Error: wrong number of arguments");
			}
			//look for values in different env w/ closure pointer? idk how to explain this
			else if (parms.isPair() && args.isPair())
			{
				env.define(parms.getCar(), args.getCar());
				Closure.pairup(parms.getCdr(), args.getCdr(), env);
			}
			else
			{
				Console.get_Error().WriteLine("Error: invalid expression");
			}
		}
		
    }    
}
