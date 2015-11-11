// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;
using System.IO;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public /* override */ bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
        //public /* override */ Node apply (Node args)
        //{
       //     return new StringLit("Error: BuiltIn.apply not yet implemented");
    	//}
		
		public override Node apply(Node args)
		{
			int num = Util.expLength(args);
			bool flag = num > 2;
			if (flag)
			{
				Console.Error.WriteLine("Error: wrong number of arguments");
			}
			bool flag2 = num == 0; // length 0 arguments
			Node result;
			if (flag2)
			{
				result = this.apply0();
			}
			else //1 or 2 argument lengths
			{
				bool flag3 = num == 1; // length 1 arguments
				if (flag3)
				{
					result = this.apply1(args.getCar());
				}
				else
				{
					result = this.apply2(args.getCar(), args.getCdr().getCar());
				}
			}
			return result;
		}

        //if arg length = 0
		private Node apply0()
		{
			string name = this.symbol.getName();
			bool flag = name.Equals("read");
			Node result;
			if (flag)
			{
				Parse.Scanner s = new Parse.Scanner(Console.In);
				Parse.Parser parser = new Parse.Parser(s, new TreeBuilder());
				Node node = (Node)parser.parseExp();
				bool flag2 = node != null;
				if (flag2)
				{
					result = node;
				}
				else
				{
					result = new Ident("end-of-file");
				}
			}//end outer if
			else
			{
				bool flag3 = name.Equals("newline");
				if (flag3)
				{
					Console.WriteLine();
					result = Unspecific.getInstance();
				}
				else
				{
					bool flag4 = name.Equals("interaction-environment");
					if (flag4)
					{
						result = Scheme4101.env;
					}
					else
					{
						Console.Error.WriteLine("Error: wrong number of arguments");
						result = Nil.getInstance();
					}
				}
			}//end outer else
			return result;
		}

        //if arg's length = 1
		private Node apply1(Node arg1)
		{
			string name = this.symbol.getName();
			bool flag = name.Equals("car");
			Node result;
			if (flag)
			{
				result = arg1.getCar();
            }//end if
            else
			{
				bool flag2 = name.Equals("cdr");
				if (flag2)
				{
					result = arg1.getCdr();
                }//end if
                else
				{
					bool flag3 = name.Equals("number?");
					if (flag3)
					{
						result = BoolLit.getInstance(arg1.isNumber());
                    }//end if
                    else
					{
						bool flag4 = name.Equals("symbol?");
						if (flag4)
						{
							result = BoolLit.getInstance(arg1.isSymbol());
                        }//end if
                        else
						{
							bool flag5 = name.Equals("null?");
							if (flag5)
							{
								result = BoolLit.getInstance(arg1.isNull());
                            }//end if
                            else
							{
								bool flag6 = name.Equals("pair?");
								if (flag6)
								{
									result = BoolLit.getInstance(arg1.isPair());
								}//end if
								else
								{
									bool flag7 = name.Equals("procedure?");
									if (flag7)
									{
										result = BoolLit.getInstance(arg1.isProcedure());
									}//end if
									else
									{
										bool flag8 = name.Equals("write");// i am not sure if im handling this correctly
                                        if (flag8)
										{
											arg1.print(-1);
											result = Unspecific.getInstance();
										}//end if
										else
										{
											bool flag9 = name.Equals("display"); // i am not sure if im handling this correctly
											if (flag9)
											{
												StringLit.printDoubleQuotes = false;
												arg1.print(-1);
												StringLit.printDoubleQuotes = true;
												result = Unspecific.getInstance();
											}//end if
											else
											{
												bool flag10 = name.Equals("load");
												if (flag10)
												{
													bool flag11 = !arg1.isString();
													if (flag11)
													{
														Console.Error.WriteLine("Error: wrong type of argument");
														result = Nil.getInstance();
													}
													else
													{
														string stringVal = arg1.getStringVal();
														try
														{
                                                            //not sure why it has to be Parse.Scanner to call this
															Parse.Scanner s = new Parse.Scanner(File.OpenText(stringVal));
															TreeBuilder b = new TreeBuilder();
															Parse.Parser parser = new Parse.Parser(s, b);
															for (Node node = (Node)parser.parseExp(); node != null; node = (Node)parser.parseExp())
															{
																node.eval(Scheme4101.env);
															}
														}
														catch (SystemException)
														{
															Console.Error.WriteLine("Could not find file " + stringVal);
														}
														result = Unspecific.getInstance();
													}
												}//end if
												else
												{
													Console.Error.WriteLine("Error: wrong number of arguments");
													result = Nil.getInstance();
												}
											}
										}
									}
								}
							}
						}//end inner sub sub sub else
					}//end inner sub sub else
				}//end inner sub else
			}//end outer else
			return result;
		}

		private Node apply2(Node arg1, Node arg2)
		{
			string name = this.symbol.getName();
			bool flag = name.Equals("eq?");
			Node result;
			if (flag)
			{
				bool flag2 = arg1.isSymbol() && arg2.isSymbol();
				if (flag2)
				{
					string name2 = arg1.getName();
					string name3 = arg2.getName();
					result = BoolLit.getInstance(name2.Equals(name3));
				}
				else
				{
					result = BoolLit.getInstance(arg1 == arg2);
				}
			}
			else
			{
				bool flag3 = name.Equals("cons");
				if (flag3)
				{
					result = new Cons(arg1, arg2);
				}
				else
				{
					bool flag4 = name.Equals("set-car!");
					if (flag4)
					{
						arg1.setCar(arg2);
						result = Unspecific.getInstance();
					}
					else
					{
						bool flag5 = name.Equals("set-cdr!");
						if (flag5)
						{
							arg1.setCdr(arg2);
							result = Unspecific.getInstance();
						}
						else
						{
							bool flag6 = name.Equals("eval");
							if (flag6)
							{
								bool flag7 = arg2.isEnvironment();
								if (flag7)
								{
									result = arg1.eval((Environment)arg2);
								}
								else
								{
									Console.Error.WriteLine("Error: wrong type of argument");
									result = Nil.getInstance();
								}
							}
							else
							{
								bool flag8 = name.Equals("apply");
								if (flag8)
								{
									result = arg1.apply(arg2);
								}
								else
								{
									bool flag9 = name[0] == 'b' && name.Length == 2;
									if (flag9)
									{
										bool flag10 = arg1.isNumber() && arg2.isNumber();
										if (flag10)
										{
											result = this.apply2(arg1.getIntVal(), arg2.getIntVal());
										}
										else
										{
											Console.Error.WriteLine("Error: invalid arguments");
											result = Nil.getInstance();
										}
									}
									else
									{
										Console.Error.WriteLine("Error: wrong number of arguments");
										result = Nil.getInstance();
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		//Function for evaluating binary arithmetic operations
		private Node apply2(int arg1, int arg2)
		{
			string name = this.symbol.getName();
			bool flag = name.Equals("b+");
			Node result;
			if (flag)
			{
				result = new IntLit(arg1 + arg2);
			}
			else
			{
				bool flag2 = name.Equals("b-");
				if (flag2)
				{
					result = new IntLit(arg1 - arg2);
				}
				else
				{
					bool flag3 = name.Equals("b*");
					if (flag3)
					{
						result = new IntLit(arg1 * arg2);
					}
					else
					{
						bool flag4 = name.Equals("b/");
						if (flag4)
						{
							result = new IntLit(arg1 / arg2);
						}
						else
						{
							bool flag5 = name.Equals("b=");
							if (flag5)
							{
								result = BoolLit.getInstance(arg1 == arg2);
							}
							else
							{
								bool flag6 = name.Equals("b<");
								if (flag6)
								{
									result = BoolLit.getInstance(arg1 < arg2);
								}
								else
								{
									Console.Error.WriteLine("Error: unknown built-in function");
									result = Nil.getInstance();
								}
							}
						}
					}
				}
			}
			return result;
		}		
    }    
}

