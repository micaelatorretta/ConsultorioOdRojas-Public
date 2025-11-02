using System;

namespace Test.Utils
{
    public class RandomGenerator
    {
        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }

        public int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max); // max es exclusivo
        }
    }
}
