namespace lacuna_genetics
{
    public class Checking
    {
        public static bool Gene(string strand, string gene)
        {
            int halfGene = gene.Length / 2;
            
            for (int i = 0; i < halfGene; i++)
            {
                var subGene = gene.Substring(i, halfGene + 1);
                for (int j = 0; j <= strand.Length - (halfGene + 1); j++)
                {
                    var subStrand = strand.Substring(j, halfGene + 1);
                    if (subStrand == subGene) return true;
                }
            }

            return false;

        }
    }
}
