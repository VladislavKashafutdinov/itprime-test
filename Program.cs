var maxN13Code = "0CCCCCCCCCCCC";

Console.WriteLine($"Nice numbers count: {FindNiceCountWithSumGrouping(maxN13Code)}");
//Console.WriteLine($"Nice numbers count: {FindNiceCountWithEnumeration(maxN13Code)}");

/// <summary>
/// Поиск количества красивых чисел путем группировки правой и левой частей всех чисел по сумме цифр
/// </summary>
static decimal FindNiceCountWithSumGrouping(string maxN13Code)
{
	var maxN13 = N13.Build(maxN13Code);
	var rightPartMaxN13 = maxN13.GetRightPart();
	var leftPartMaxN13 = maxN13.GetLeftPart();
	var maxSum = leftPartMaxN13.GetDigitSum();
	var sumCounts = new (decimal rightPartSumCount, decimal leftPartSumCount)[maxSum + 1];

	// Группировка возможных вариантов правых частей по сумме цифр и расчет количества этих групп
	var rightPartN13s = rightPartMaxN13.ToDecimal().Range()
		.Select(n10 => n10.ToN13(rightPartMaxN13.Digits.Length))
		.ToArray();
	foreach (var sum in rightPartN13s.Select(n13 => n13.GetDigitSum()))
	{
		if (sum < sumCounts.Length)
		{
			sumCounts[sum].rightPartSumCount++;
		}
	}

	// Группировка возможных вариантов левых частей по сумме цифр и расчет количества этих групп
	var leftPartN13s = leftPartMaxN13.ToDecimal().Range()
		.Select(n10 => n10.ToN13(leftPartMaxN13.Digits.Length))
		.ToArray();
	foreach (var sum in leftPartN13s.Select(n13 => n13.GetDigitSum()))
	{
		sumCounts[sum].leftPartSumCount++;
	}

	// Комбинаций количеств правых и левых частей, дающих одинаковую сумму цифр
	var leftRightPartNiceCombinationCounts = sumCounts.Sum(
		sum => sum.rightPartSumCount * sum.leftPartSumCount);
	
	// Если размер числа нечетный (н-р, 13), то цифра в середине числа не будет играть роли,
	// является число красивым или нет.
	// Значит, в этом случае, количество красивых чисел будет равно
	// количеству красивых комбинаций левых и правых частей умножить на 13
	// (количество возможных вариантов цифры в середине числа)
	var middleDigitCount = maxN13.Digits.Length - rightPartMaxN13.Digits.Length * 2;
	var middleDigitMultiplyer = (int)Math.Pow(D13Extensions.Dimension, middleDigitCount);
	return leftRightPartNiceCombinationCounts * middleDigitMultiplyer;
}

/// <summary>
/// Метод перебора всех чисел. Используется для проверки
/// </summary>
static decimal FindNiceCountWithEnumeration(string maxN13Code)
{
	var maxN13 = N13.Build(maxN13Code);
	var maxN10 = maxN13.ToDecimal();
	var n10s = maxN10.Range().ToArray();
	var n13s = n10s.Select(n10 => n10.ToN13(maxN13.Digits.Length)).ToArray();
	var niceN13s = n13s.Where(n13 => n13.IsNice()).ToArray();
	return n13s.Count(n13 => n13.IsNice());
}
