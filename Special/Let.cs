// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }
		
		//added from here and down, possible errors 
		private static int define(Node bind, Environment env, Environment lenv)
		{
			if (bind.isNull())
			{
				return 0;
			}
			Node car = bind.getCar();
			if (Util.expLength(car) != 2)
			{
				return -1;
			}
			Node car2 = car.getCar(); // (car x)
			Node value = car.getCdr().getCar().eval(env); //(eval (cadr x) env)
			lenv.define(car2, value); // (list (car x) (eval (cadr x) env)))
			return Let.define(bind.getCdr(), env, lenv);
		}

		public override Node eval(Node exp, Environment env)
		{
			int num = Util.expLength(exp);
			if (num < 3)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			Node car = exp.getCdr().getCar();
			Node cdr = exp.getCdr().getCdr();
			num = Util.expLength(car);
			if (num < 1)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			Environment environment = new Environment(env);
			if (Let.define(car, env, environment) < 0)
			{
				Console.Error.WriteLine("Error: invalid expression");
				return Nil.getInstance();
			}
			return Util.begin(cdr, environment);
		}
		
    }
	
	
}


