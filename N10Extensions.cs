
//Console.WriteLine($"Nice numbers count: {FindNiceCountWithEnumeration(maxN13Code)}");

/// <summary>
/// Метод перебора всех чисел для проверки
/// </summary>

/// <summary>
/// Поиск количества красивых чисел
/// </summary>
public static class N10Extensions
{
	public static D13 ToD13(this int i) => (D13)i;
	public static N13 ToN13(this decimal number, int size)
	{
		var rest = number;
		var digits = new D13[size];
		for (var i = size-1; i >= 0; i--)
		{
			var d10 = (int)(rest % D13Extensions.Dimension);
			digits[i] = d10.ToD13();
			rest = (rest - d10) / 13;
			rest = Math.Floor(rest);
		}
		return new N13(digits);
	}
	public static decimal Pow(this decimal number, int power)
	{
		decimal result = 1;
		for (int i = 1; i <= power; i++)
		{
			result *= number;
		}
		return result;
	}
	public static IEnumerable<decimal> Range(this decimal number)
	{
		for (decimal i = 0; i <= number; i++)
		{
			yield return i;
		}
	}
}
