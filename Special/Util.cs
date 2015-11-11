using System;

namespace Tree
{
	/**
	* Helper class to obtain Node expression data. Length and map function to evaluate expressions
	*/
	public class Util
	{
		//obtain experession length of a statement
		public static int expLength(Node exp)
		{
			if (exp.isNull())
				return 0;
			
			if (!exp.isPair())
				return -1;
			
			int num = Util.expLength(exp.getCdr());
			if (num == -1)
				return -1;
			
			return num + 1;
		}

		//mapping element for evaluation, i think it's similar when you are trying to map some exp to a certain lambda
		//Ex) (map (lambda (x) (* -1 x)) '(1 2 3 4))
		//reference http://courses.cs.washington.edu/courses/cse341/02sp/scheme/apply-eval.html
		public static Node mapEval(Node exp, Environment env)
		{
			if (exp.isNull())
				return Nil.getInstance();
			return new Cons(exp.getCar().eval(env), Util.mapEval(exp.getCdr(), env));
		}

		//function to get the car value of the current environment & the rest of exp (cdr)
		public static Node begin(Node exp, Environment env)
		{
			Node result = exp.getCar().eval(env);
            //Console.WriteLine("This is result: " + result);
			Node cdr = exp.getCdr();
            //Console.WriteLine("This is cd:  "+ cdr);
			if (cdr.isNull())
			{
				return result;
			}
			return Util.begin(cdr, env);
		}
	}
}
