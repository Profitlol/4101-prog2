// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;
        public static bool printDoubleQuotes = true;

        public StringLit(string s)
        {
            stringVal = s;
        }

        public override void print(int n)
        {
            bool flag = StringLit.printDoubleQuotes;
            //handling "" situation
            if (flag) {
                Printer.printStringLit(n, stringVal);
            }
            //print normally
            else
            {
                int num;
                for (int i = 0; i < n; i = num + 1)
                {
                    Console.Write(' ');
                    num = i;
                }
                Console.Write(this.stringVal);
                bool flag2 = n >= 0;
                if (flag2)
                {
                    Console.WriteLine();
                }
            }
        }

        public override bool isString()
        {
            return true;
        }
		
		//added this and down
		public override Node eval(Environment env)
		{
			return this;
		}
		
		public override string getStringVal()
		{
			return this.stringVal;
		}
    }
}

