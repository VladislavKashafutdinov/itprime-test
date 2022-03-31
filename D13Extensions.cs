
//Console.WriteLine($"Nice numbers count: {FindNiceCountWithEnumeration(maxN13Code)}");

/// <summary>
/// Метод перебора всех чисел для проверки
/// </summary>

/// <summary>
/// Поиск количества красивых чисел
/// </summary>
public static class D13Extensions
{
	public const int Dimension = 13;
	public static string Print(this D13 d13) => string.Join("", d13.ToString().Skip(1));
	public static int ToInt(this D13 d13) => (int)d13;
	public static int GetSum(this IEnumerable<D13> digits) => digits.Select(d => d.ToInt()).Sum();
}
