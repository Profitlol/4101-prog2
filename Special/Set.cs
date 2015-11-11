// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }
		
		public override Node eval(Node exp, Environment env){
            int num = Util.expLength(exp);
            //(set-cdr! (assq ’x (car env)) (list 15)), need length 3
            bool flag = num != 3;
            Node instance;
            if (flag)
            {
                Console.Error.WriteLine("Error: invalid expression");
                instance = Nil.getInstance();
            }
            else
            {
                Node car = exp.getCdr().getCar();
                //Console.WriteLine(car);
                Node car2 = exp.getCdr().getCdr().getCar();
                //Console.Writeline(car2)
                env.assign(car, car2.eval(env));
                instance = Unspecific.getInstance();
            }
            return instance;
        }
		
    }
}

