namespace Algorithms
{
    public class Algorithm
    {
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public Algorithm() {
            Name = "New Algorithm";
            Description = "No Description";
        }
    }

    public class NaiveAlgorithm : Algorithm
    {
        public NaiveAlgorithm()
        {
            Name = "Naive Algorithm";
            Description = "Time Complexity : O(m*n). Suitable for small text";
        }
    }
}