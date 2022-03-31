
//Console.WriteLine($"Nice numbers count: {FindNiceCountWithEnumeration(maxN13Code)}");

/// <summary>
/// Метод перебора всех чисел для проверки
/// </summary>

/// <summary>
/// Поиск количества красивых чисел
/// </summary>
public struct N13
{
	public D13[] Digits { get; }

	public N13(IEnumerable<D13> digits) : this(digits.ToArray()) { }

	public N13(params D13[] digits)
	{
		Digits = digits;
	}

	public decimal ToDecimal()
	{
		var length = Digits.Length;
		return Digits
			.Select((d, i) => d.ToInt() * ((decimal)D13Extensions.Dimension).Pow(length - i - 1))
			.Sum();
	}
	public bool IsNice() => IsNice(Digits.Length / 2);
	public bool IsNice(int partSize) => IsNice(GetLeftPart(partSize), GetRightPart(partSize));
	public int GetDigitSum() => Digits.GetSum();
	public N13 GetLeftPart() => GetLeftPart(Digits.Length / 2);
	public N13 GetLeftPart(int size) => new(Digits.Take(size));
	public N13 GetRightPart() => GetRightPart(Digits.Length / 2);
	public N13 GetRightPart(int size) => new(Digits.Skip(Digits.Length - size));
	public override string ToString() => string.Join("", Digits.Select(d => d.Print()));
	public static bool IsNice(N13 left, N13 right) => left.GetDigitSum() == right.GetDigitSum();
	public static N13 Build(string digits)
	{
		return new(digits.Select(d => Enum.Parse<D13>($"D{d.ToString().ToUpper()}")));
	}
}