public class WordFinderTests
{
	[Fact]
	public void Find_BasicSearch_ReturnsFoundWords()
	{
		// Arrange
		var matrix = new List<string>
		{
			"hello",
			"world",
			"apple",
			"peace"
		};
		var wordStream = new List<string> { "hello", "apple", "peace", "world" };
		var finder = new WordFinder(matrix);

		// Act
		var result = finder.Find(wordStream).ToList();

		// Assert
		Assert.Equal(["hello", "apple", "peace", "world"], result);
	}

	[Fact]
	public void Constructor_MatrixExceeds64x64_ThrowsArgumentException()
	{
		// Arrange
		var oversizedMatrix = Enumerable.Repeat("a".PadLeft(65, 'a'), 65);

		// Act & Assert
		Assert.Throws<ArgumentException>(() => new WordFinder(oversizedMatrix));
	}

	[Fact]
	public void Find_NoWordsFound_ReturnsEmpty()
	{
		// Arrange
		var matrix = new List<string>
		{
			"dog",
			"cat",
			"bird",
			"fish"
		};
		var wordStream = new List<string> { "lion", "tiger", "elephant" };
		var finder = new WordFinder(matrix);

		// Act
		var result = finder.Find(wordStream);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Find_WordRepeatedMultipleTimes_ReturnsCorrectOrder()
	{
		// Arrange
		var matrix = new List<string>
		{
			"appleapple",
			"appleapple",
			"bananaapple"
		};
		var wordStream = new List<string> { "apple", "banana" };
		var finder = new WordFinder(matrix);

		// Act
		var result = finder.Find(wordStream).ToList();

		// Assert
		Assert.Equal(["apple", "banana"], result);
	}

	[Fact]
	public void Find_MoreThan10WordsInMatrix_ReturnsTop10MostFrequent()
	{
		// Arrange
		var matrix = new List<string>
		{
			"appleappleapple",
			"bananaapplegrape",
			"grapeorangeapple",
			"applegrapefruit",
			"fruitbananaapple"
		};
		var wordStream = new List<string>
		{
			"apple", "banana", "grape", "orange", "fruit", "mango", "kiwi",
			"pineapple", "peach", "lemon", "melon", "berry"
		};
		var finder = new WordFinder(matrix);

		// Act
		var result = finder.Find(wordStream).ToList();

		// Assert
		Assert.Equal(10, result.Count);
		Assert.Contains("apple", result);
		Assert.Contains("banana", result);
	}
}
