namespace Pathfinding
{
    public class Program
    {
        public static int mapSizeX, mapSizeY;

        public static Node[][] map;

        public static void Main(string[] args)
        {
            mapSizeX = 40;
            mapSizeY = 40;

            for (var x = 0; x < mapSizeX; x++)
            {
                for (var y = 0; y < mapSizeY; y++)
                {
                    map[x][y] = new Node(x, y);
                }
            }
        }
    }
}
