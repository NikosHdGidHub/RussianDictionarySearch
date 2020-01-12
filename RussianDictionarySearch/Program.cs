using Lib;
using Lib.ConsoleLib;
using System;
using System.Linq;
using System.Text;
using TreeLib;

namespace RussianDictionarySearch
{
	internal class Program
	{
		private const string path = "dictionary1.txt";
		private static readonly IConsoleModel model = ConsoleModel.GetView();

		private static void Main(string[] args)
		{
			Console.Title = "Словарь русского языка";
			Console.CursorSize = 44;
			Console.WriteLine("Добро пожаловать в словарь русского языка");
			model.PrintMessage += mes => Console.WriteLine(mes); ;

			var DictionaryRow = FFunc.ReadTextFileRows(path);
			var Tree = new TreeWords<int>();
			for (int i = 0; i < DictionaryRow.Count; i++)
			{
				//DictionaryRow[i] = DictionaryRow[i].Substring(8);
				Tree.Add(DictionaryRow[i].Substring(8), i);
			}
			
			var flag = true;
			var str = "";
			Console.WriteLine("Введите символы");
			while (flag)
			{

				char key = Console.ReadKey().KeyChar;
				if (key == 'e') break;
				if (key == '`' || key == '\r')
				{
					if (Tree.Contains(str))
					{
						Console.WriteLine();
						Console.WriteLine("Найдено: " + str);
					}
					str = "";
					Console.WriteLine("Введите символы");
					continue;
				}
				if (key == '\b')
				{
					if (str.Length <= 1)
						str = "";
					else
						str = str.Substring(0, str.Length - 1);
				}
				else
					str += key.ToString().ToLower();
				var pos = Console.CursorLeft;
				Console.Write("                                      ");
				Console.CursorLeft = pos + 2;
				

				var resultSearth = Tree.SearchByPrefix(str);
				var first = resultSearth?.FirstOrDefault() ?? " - ";
				var last = "";
				if (resultSearth != null && resultSearth.Count > 1)
				{
					last = " || " + resultSearth.Last();
				}

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(first + last);
				if (Tree.Contains(str))
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(" <- Такое слово есть");
				}
				Console.ResetColor();
				Console.CursorLeft = pos;
			}

			model.EndProgramm();
		}
		//private static void CheckWords
		private static void SearchWords(char[] charArr, TreeWords<int> dict)
		{
			//a,b,c,d,e
			var len = (byte)charArr.Length;
			var strB = new StringBuilder(len);

			var maxCounter = (ulong)Math.Pow(10, len);

			for (ulong i = 0; i < maxCounter;)
			{
				if ((byte)(i % 10) == len - 1)
				{
					i += 11 - (ulong)len;
					continue;
				}
				//        1
				//a,a,a,a,b
				i++;
			}

			void check(string str, StringBuilder stringBuilder)
			{

			}
		}
	}
}
