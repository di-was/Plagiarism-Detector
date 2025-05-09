using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Algorithms
{
    public class Algorithm
    {
        public static string? name;
        public static string? description;
        public virtual string Content { get; set; } = "";
        public virtual string Pattern { get; set; } = "";


        public virtual int[] PreProcess() { return new int[1]; }
        public virtual int[] Detect() { return new int[0]; }
        
        public override string ToString()
        {
            return name + ": " + base.ToString() + "\n" + "Description: " + description;
        }
      
    }

    sealed class NaiveAlgorithm : Algorithm
    {
        // Returns starting indexes of all substrings that match the pattern
       public override int[] Detect()
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
        public NaiveAlgorithm(string content, string pattern){
            this.Content = content;
            this.Pattern = pattern;
        }
    }

    sealed class KMP : Algorithm
    {
        public override int[] PreProcess()
        {
            // Computes the Longest Suffix that is also Prefix table
        
            int[] LongestPrefixSuffix = new int[Pattern.Length];
            int i = 1, j = 0;
            while (i < Pattern.Length)
            {
                if (Pattern[i] == Pattern[j])
                {
                    j++;
                    LongestPrefixSuffix[i] = j;
                    i++;
                }
                else if (j != 0)
                {
                    j = LongestPrefixSuffix[j - 1];

                } else
                {
                    LongestPrefixSuffix[i] = 0;
                    i++;
                }
            }
            return LongestPrefixSuffix;
        }

        public override int[] Detect()
        {
            int[] lps = PreProcess();
            int i = 0, j = 0;
            List<int> detectedIndexes = new List<int>();
            while (i < Content.Length)
            {
                if (Pattern[j] == Content[i])
                {
                    i++;
                    j++;
                }
                
                if (j == Pattern.Length)
                {
                    detectedIndexes.Add(i-j);
                    j = lps[j - 1];
                    
                } else
                {
                    if (i < Content.Length && Pattern[j] != Content[i])
                    {
                        if (j != 0)
                        {
                            j = lps[j - 1];
                        } else
                        {
                            i++;
                            
                        }
                    } 
                }
            }
            return detectedIndexes.ToArray();
        }
        static KMP()
        {
            name = "Knuth-Morris-Pratt Algorithm";
            description = "Time Complexity is O(m+n).";
        }
        public KMP(string content, string pattern)
        {
            this.Content = content;
            this.Pattern = pattern;
        } 
    }

    sealed class RabinKarp : Algorithm
    {
        public static int p = 31; // small prime number
        public static int m = (int)Math.Pow(10, 9) + 9; // large prime number
       static RabinKarp()
        {
        if (!RabinKarp.IsPrime(m))
            {
                throw new ArgumentException("Invalid static parameter");
            }
        
        }

        public static bool IsPrime(int m)
        {
            if (m <= 1) { return false;}
            if (m == 2) { return true; }
            int upperBound = (int)Math.Sqrt(m);
            for (int i=2; i<upperBound; i++)
            {
                if (m%i == 0)
                {
                    return false;
                }
            }
            return false;
        }
        public static int CalculateHash(string s)
        {
            int hashValue=0;
            int asciiValue;
            int power = 1;

            for (int i=0; i<s.Length; i++)
            {
                asciiValue = s[i] - 'a';
                hashValue += (hashValue + asciiValue * power) % m;
                power = (power * p) % m;

            }
            return hashValue;
        }

        public override int[] PreProcess()
        {
            int[] hashList = new int[Content.Length];
            for (int i=0; i<=Content.Length - Pattern.Length; i++) {
                hashList[i] = (int)RabinKarp.CalculateHash(Content.Substring(i,Pattern.Length));
            }
            return hashList;
        }
        public override int[] Detect()
        {
            var detectedIndexes = new List<int>();
            int[] hashList = this.PreProcess();
            int patternHashValue = RabinKarp.CalculateHash(Pattern);
            for (int i = 0; i < hashList.Length; i++)
            {
                if (hashList[i] == patternHashValue)
                {
                    detectedIndexes.Add(i);
                }
            }
            return detectedIndexes.ToArray();
        }

        static  RabinKarp()
        {
            name = "Rabin-Karp Algorithm";
            description = "Best case scenario : O(m+n), worst case scenario if suboptima hash function : O(mn)";
        }
        public RabinKarp(string content, string pattern)
        {
            this.Content = content;
            this.Pattern = pattern;
        }
    }

    sealed class BoyerMoore : Algorithm
    {
        static BoyerMoore()
        {
            name = "Boyer-Moore Algorithm";
            description = "Best case : O(n/m) \n Average case : O(n) \n Worst case : O(n*m)";
        }

        public BoyerMoore(string content, string pattern)
        {
            this.Content = content;
            this.Pattern = pattern;
        }
    }
}
