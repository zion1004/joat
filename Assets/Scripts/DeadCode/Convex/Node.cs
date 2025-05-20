namespace ConcaveHull
{
    public class Node
    {
        public int id;
        public float x;
        public float y;
        public double cos; // Used for middlepoint calculations
        public Node(float x, float y, int id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }
    }
}