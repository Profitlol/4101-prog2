using System;

namespace Tree
{
    public class Unspecific : Node
    {
        private static Unspecific instance = new Unspecific();

        private Unspecific()
        {
        }

        public static Unspecific getInstance()
        {
            return Unspecific.instance;
        }

        public override void print(int n)
        {
            int num;
            for (int i = 0; i < n; i = num + 1)
            {
                Console.Write(' ');
                num = i;
            }
            Console.Write("#{Unspecific}");
            bool flag = n >= 0;
            if (flag)
            {
                Console.WriteLine();
            }
        }
    }
}