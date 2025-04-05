using System.Runtime.CompilerServices;

namespace Algorithms
{
    public class Algorithm
    {
        public static String? name;
        public static String? description;
        public virtual String Content { get; set; } = "";
        public virtual String Pattern { get; set; } = "";


        public virtual String PreProcess(){return "No preprocessing Required";}
        public virtual int[] PreProcess(String content) { return new int[content.Length]; }
        
        public override string ToString()
        {
            return name + ": " + base.ToString() + "\n" + "Description: " + description;
        }
        public Algorithm() {}
    }

    sealed class NaiveAlgorithm : Algorithm
    {
        // Returns starting indexes of all substrings that match the pattern
       public int[] Detect()
        {
            List<int> detectedIndexes = new List<int>();
            for (int i=0; i<Content.Length - Pattern.Length; i++)
            { 
                int j;
                for (j=0; j<Pattern.Length; j++)
                {
                    if (Content[i+j] != Pattern[j]) { break; }
                }
                if (j==Pattern.Length)
                {
                    detectedIndexes.Add(i);
                }
            }
            return detectedIndexes.ToArray();
        }
        static NaiveAlgorithm()
        {
            name = "Naive Algorithm";
            description = "Time Complexity is O(m*n).Suitable for small text";
        }
        public NaiveAlgorithm(String content, String pattern){
            this.Content = content;
            this.Pattern = pattern;
        }
    }

    sealed class KMP : Algorithm
    {
        public override int[] PreProcess(String content)
        {
            return new int[content.Length];
        }
        static KMP()
        {
            name = "Knuth-Morris-Pratt Algorithm";
            description = "Time Complexity is O(m+n)";
        }
    }
}