var maxN13Code = "0CCCCCCCCCCCC";

Console.WriteLine($"Nice numbers count: {FindNiceCountWithSumGrouping(maxN13Code)}");
//Console.WriteLine($"Nice numbers count: {FindNiceCountWithEnumeration(maxN13Code)}");

static decimal FindNiceCountWithEnumeration(string maxN13Code)
{
	var maxN13 = N13.Build(maxN13Code);
	var maxN10 = maxN13.ToDecimal();
	var n10s = maxN10.Range().ToArray();
	var n13s = n10s.Select(n10 => n10.ToN13(maxN13.Digits.Length)).ToArray();
	var niceN13s = n13s.Where(n13 => n13.IsNice()).ToArray();
	return n13s.Count(n13 => n13.IsNice());
}
static decimal FindNiceCountWithSumGrouping(string maxN13Code)
{
	var maxN13 = N13.Build(maxN13Code);
	var rightPartMaxN13 = maxN13.GetRightPart();
	var leftPartMaxN13 = maxN13.GetLeftPart();
	
	var rightPartMaxN10 = rightPartMaxN13.ToDecimal();
	var rightPartN10s = rightPartMaxN10.Range().ToArray();
	var rightPartN13s = rightPartN10s
		.Select(n10 => n10.ToN13(rightPartMaxN13.Digits.Length))
		.ToArray();
	var maxSum = leftPartMaxN13.GetDigitSum();
	var sumCounts = new (decimal rightPartSumCount, decimal leftPartSumCount)[maxSum + 1];
	foreach (var rightPartN13 in rightPartN13s)
	{
		var sum = rightPartN13.GetDigitSum();
		if (sum <= maxSum)
		{
			sumCounts[sum].rightPartSumCount++;
		}
	}
	
	var leftPartMaxN10 = leftPartMaxN13.ToDecimal();
	var leftPartN10s = leftPartMaxN10.Range().ToArray();
	var leftPartN13s = leftPartN10s
		.Select(n10 => n10.ToN13(leftPartMaxN13.Digits.Length))
		.ToArray();
	foreach (var leftPartN13 in leftPartN13s)
	{
		var sum = leftPartN13.GetDigitSum();
		sumCounts[sum].leftPartSumCount++;
	}

	var leftRightPartNiceCombinationCounts = sumCounts.Sum(
		sum => sum.rightPartSumCount * sum.leftPartSumCount);
	var middleDigitCount = maxN13.Digits.Length - rightPartMaxN13.Digits.Length * 2;
	var middleDigitMultiplyer = (int)Math.Pow(D13Extensions.Dimension, middleDigitCount);
	return leftRightPartNiceCombinationCounts * middleDigitMultiplyer;
}

public static class D13Extensions
{
	public const int Dimension = 13;
	public static string Print(this D13 d13) => string.Join("", d13.ToString().Skip(1));
	public static int ToInt(this D13 d13) => (int)d13;
	public static int GetSum(this IEnumerable<D13> digits) => digits.Select(d => d.ToInt()).Sum();
}
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