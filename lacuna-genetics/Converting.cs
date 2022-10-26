namespace lacuna_genetics
{
    public class Converting
    {
        public static string ComplementaryToTemplate(string strand)
        {
            string templateStrand = "";
            
            foreach(char c in strand)
            {
                switch (c)
                {
                    case 'A':
                        templateStrand += 'T';
                        break;
                    
                    case 'C':
                        templateStrand += 'G';
                        break;
                    
                    case 'G':
                        templateStrand += 'C';
                        break;

                    case 'T':
                        templateStrand += 'A';
                        break;
                }
            }

            return templateStrand;
        }
    }
}
