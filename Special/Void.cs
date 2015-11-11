using System;

namespace Tree
{
	/**
	* Class for an "empty" expression. A blank space
	*/
	public class Void : Node
	{
		private static Void instance = new Void();

		private Void()
		{
		}

		public static Void getInstance()
		{
			return Void.instance;
		}

		public override void print(int n)
		{
		}
	}
}