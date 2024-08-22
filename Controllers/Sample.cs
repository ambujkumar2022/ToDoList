namespace FullStackPr_ToDoList.Controllers
{
	delegate void Calculator(int x, int y);
	public class Sample
	{
		public static void Add(int a, int b)
		{
			Console.WriteLine(a + b);
		}
		public static void Mult(int a ,int b)
		{
			Console.WriteLine (a * b);
		}
	}
}
