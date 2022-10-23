namespace lacuna_genetics
{
    public class Encoding
    {
        public static string StringToBase64(string strand)
        {
            return ByteToBase64(BitsToByte(StringToBits(strand)));
        }

        private static string StringToBits(string strand)
        {
            const string A = "00";
            const string C = "01";
            const string T = "11";
            const string G = "10";
            string final = "";

            foreach (char c in strand)
            {
                switch (c)
                {
                    case 'A':
                        final += A;
                        break;
                    case 'C':
                        final += C;
                        break;
                    case 'T':
                        final += T;
                        break;
                    case 'G':
                        final += G;
                        break;

                }
            }

            return final;
        }
        private static byte[] BitsToByte(string bits)
        {
            Queue<byte> queue = new Queue<byte>();

            for (int i = 0; i < bits.Length; i += 8)
            {
                queue.Enqueue(Convert.ToByte(bits.Substring(i, 8), fromBase: 2));
            }

            return queue.ToArray();
        }

        private static string ByteToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
