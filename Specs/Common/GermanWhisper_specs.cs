namespace Specs.Common.GermanWhisper_specs;

public class Restricts
{
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    public void paths(int length)
    {
        var occur = new Digits[length];
        var q = new Queue<string>(["1", "2", "3", "4", "6", "7", "8", "9"]);

        for (var l = 1; l < length; l++)
        {
            var count = q.Count;

            for (var i = 0; i < count; i++)
            {
                var path = q.Dequeue();
                var done = Parse(path);
                var last = path[^1] - '0';

                var nexts = ~Digits.Between(last - 4, last + 4);
                nexts ^= done;

                foreach (var next in nexts)
                {
                    q.Enqueue($"{path}{next}");
                }
            }
        }

        foreach (var path in q)
        {
            for (var i = 0; i < path.Length; i++)
            {
                occur[i] |= path[i] - '0';
            }
        }

        for (var i = 0; i < length; i++)
        {
            Console.WriteLine($"{i}: {occur[i]}");
        }

        Assert.Inconclusive();

        static Digits Parse(string s)
        {
            var digits = Digits.None;
            foreach (var c in s)
                digits |= c - '0';

            return digits;
        }
    }
}
