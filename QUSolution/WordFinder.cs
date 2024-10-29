using System.Text;

public class WordFinder
{
	private readonly List<string> _rows = [];
	private readonly List<string> _columns = [];

	public WordFinder(IEnumerable<string> matrix)
	{
		this._rows = matrix.ToList();

		if (this._rows.Count > 64 || this._rows.Any(row => row.Length > 64))
			throw new ArgumentException("the matrix should not exceed 64x64.");

		for (int i = 0; i < _rows[0].Length; i++)
		{
			var column = new StringBuilder(_rows.Count);

			for (int j = 0; j < _rows.Count; j++)
				column.Append(_rows[j][i]);

			_columns.Add(column.ToString());
		}
	}

	public IEnumerable<string> Find(IEnumerable<string> wordStream)
	{
		var wordFrequency = wordStream
			.Distinct()
			.ToDictionary(
				word => word,
				word => _rows.Sum(row => row.Split(word).Length - 1) +
						_columns.Sum(column => column.Split(word).Length - 1)
			);

		return wordFrequency
			.Where(kv => kv.Value > 0)
			.OrderByDescending(kv => kv.Value)
			.Take(10)
			.Select(kv => kv.Key);
	}
}
