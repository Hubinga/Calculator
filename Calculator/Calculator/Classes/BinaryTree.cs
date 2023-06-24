using Calculator.Models;

namespace Calculator.Classes
{

	public class Node
	{
		public Node(string value)
		{
			Value = value;
		}

		public string Value { get; private set; } = "";
		public Node? Left { get; private set; } = null;
		public Node? Right { get; private set; } = null;

		public void Add(string newValue)
		{
			if(Left == null)
			{
				Left = new Node(newValue);
			}
			else if(Right == null)
			{
				Right = new Node(newValue);
			}
			else
			{
				
				//dont add dublicate values
				if(Left.Value == newValue || Right.Value == newValue) 
				{
					return;
				}

				if(Left.Value.Contains(newValue))
				{
					Left.Add(newValue);
				}
				else if(Right.Value.Contains(newValue))
				{
					Right.Add(newValue);
				}
			}
		}
	}

	public class BinaryTree
	{
		public Node? Root { get; private set; } = null;

		public void PrintLevelOrder()
		{
			int heigth = CalculateHeight(Root);

			for (int i = 1; i <= heigth; i++)
			{
				PrintCurrentLevel(Root, i);
                Console.WriteLine();
            }
		}

		private void PrintCurrentLevel(Node root, int level)
		{
			if (root == null) 
			{
				return;
			}
			if (level == 1)
			{
                Console.Write(root.Value + " ");
            }
			else if (level > 1)
			{
				PrintCurrentLevel(root.Left, level - 1);
				PrintCurrentLevel(root.Right, level - 1);
			}
		}

		public void PrintAllCodes()
		{
			PrintCodes(Root, "");
		}

		private void PrintCodes(Node root, string str)
		{
			if (root == null)
				return;

			if (root.Value != "")
				Console.WriteLine(root.Value + ": " + str);

			PrintCodes(root.Left, str + "0");
			PrintCodes(root.Right, str + "1");
		}

		public void GetEncodings(List<EncodingDataModel> encodingDataModels)
		{
			for (int i = 0; i < encodingDataModels.Count; i++)
			{
				string encoding = GetEncodingForValue(Root, encodingDataModels[i].Value);
				encodingDataModels[i].Encoding = encoding;
                Console.WriteLine("enoding for {0}: {1}", encodingDataModels[i].Value, encoding);
            }
		}

		private string GetEncodingForValue(Node root, string value)
		{
			if (root == null)
			{
				return "";
			}
			else
			{
				if (root.Value == value)
				{
					return "";
				}

				if (root.Left != null && root.Left.Value.Contains(value))
				{
					return "0" + GetEncodingForValue(root.Left, value);
				}
				else if (root.Right != null && root.Right.Value.Contains(value))
				{
					return "1" + GetEncodingForValue(root.Right, value);
				}
				else
				{
					return "";
				}
			}
		}

		public int CalculateHeight(Node root)
		{
			if(root == null)
			{
				return 0;
			}
			else
			{
				//calculate height of left and right subtree
				int left = CalculateHeight(root.Left);	
				int right = CalculateHeight(root.Right);
				
				if(left < right)
				{
					return right + 1;
				}
				else
				{
					return left + 1;
				}
			}
		}

		public void Add(string value)
		{
			if (Root == null)
			{
				Root = new Node(value);
			}
			else
			{
				Root.Add(value);
			}
		}
	}
}
