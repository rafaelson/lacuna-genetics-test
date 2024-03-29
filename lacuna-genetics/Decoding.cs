﻿namespace lacuna_genetics
{
    public class Decoding
    {
        public static string Base64ToString(string base64Str)
        {
            return BitsToString(ByteToBits(Base64ToByte(base64Str)));
        }

        private static byte[] Base64ToByte(string base64Str)
        {
            return Convert.FromBase64String(base64Str);
        }

        private static string ByteToBits(byte[] hex)
        {
            string bits = "";
            string temp;

            foreach (byte b in hex)
            {
                temp = Convert.ToString(b, toBase: 2);
                
                if (temp.Length != 8)
                {
                    int diff = 8 - temp.Length;
                    var zeroes = String.Concat(Enumerable.Repeat('0', diff));
                    temp = $"{zeroes}{temp}";
                }

                bits += temp;
            }

            return bits;
        }

        private static string BitsToString(string bits)
        {
            const string A = "00";
            const string C = "01";
            const string T = "11";
            const string G = "10";
            string strand = "";

            for (int i = 0; i < bits.Length; i += 2)
            {
                switch (bits.Substring(i, 2))
                {
                    case A:
                        strand += 'A';
                        break;
                    case C:
                        strand += 'C';
                        break;
                    case T:
                        strand += 'T';
                        break;
                    case G:
                        strand += 'G';
                        break;
                }
            }
            return strand;
        }

    }
}
