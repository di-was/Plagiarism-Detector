namespace Algorithms
{
    public class Algorithm
    {
        protected static String name;
        protected static String description;
        public virtual String Content { get; set; }
        public virtual String Pattern { get; set; }


        public virtual void PreProcess(String content=""){Console.WriteLine("No preprocessing Required");}
        public Algorithm() {}
    }

    public class NaiveAlgorithm : Algorithm
    {
        static NaiveAlgorithm()
        {
            name = "Naive Algorithm";
            description = "Time Complexity is O(m*n).Suitable for small text";
        }
        public NaiveAlgorithm(String content, String pattern){
            this.Content = content;
            this.Pattern = pattern;
        }
        public override string ToString()
        {
            return name + ": " + base.ToString() + "\n" + "Description: " + description;
        }
    }
}