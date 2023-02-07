namespace Pc.Configurator
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Please uncomment one from the provided inputs and run the program in order to test several cases in which the program works.

            /*Negative path - 3 components testing */

            //var input = new Queue<string>(new string[] { "12900K, 11700K, CR8GB", "12900K, ASX670E, GS8GB", "11700K, ASUSZ690, KS16GB" });           
            //var input = new Queue<string>(new string[] { "12900K, MSIZ690, CR8GB", "12900K, ASX670E, GS8GB", "11700K, ASUSZ690, KS16GB" });

            /*Happy path - 3 components */

            var input = new Queue<string>(new string[] { "12900K, MSIZ690, SM32GB", "12900K, ASUSZ690, KS16GB", "7950X, MSIX670E, SM32GB" });

            /*Less than 3 components */

            //var input = new Queue<string>(new string[] { "12900K", "11700K", "ASUSZ690", "ASX670E, GS8GB", "KS16GB" , "ASUSZ690, KS16GB", "11700K, ASUSZ690"});          
           // var input = new Queue<string>(new string[] { "12900K, 11700K", "GS8GB, KS16GB", "ASUSZ690, ASX670E" });
            
            PcConfigurator.Configure(input);
            
        }
    }
}