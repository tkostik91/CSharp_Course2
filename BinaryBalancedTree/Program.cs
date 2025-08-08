using System.Diagnostics;
using System.Security.Cryptography;

namespace BinrayBalancedTree
{
    public class TreeNode
    {

        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

        public int Weight { get; set; }

    }


    internal class Program
    {
        static Random rnd = new Random();
        static long total;

        public static void CreateRandomTree(TreeNode node, int level)
        {
            node.Left = new TreeNode();
            node.Right = new TreeNode();
            node.Weight = rnd.Next(100);
            total += node.Weight;
            level--;
            if (level == 0)
            {
                node.Left.Weight = rnd.Next(100);
                node.Right.Weight = rnd.Next(100);
                total += node.Left.Weight;
                total += node.Right.Weight;
                return;
            }
            CreateRandomTree(node.Left, level);
            CreateRandomTree(node.Right, level);
        }


        public static long weightTree(TreeNode root)
        {
            return
                (long)root.Weight +
                (root.Left != null ? weightTree(root.Left) : 0) +
                (root.Right != null ? weightTree(root.Right) : 0);

        }

        static async Task <long> weightTreeAsync(TreeNode root, int level = 8)
        {
            if(level <=0)return weightTree(root);
            int newLevel = level - 1;

            Task<long> t1 = null, t2 = null;
            if(root.Left != null)
                t1 = Task.Run(()=> weightTreeAsync(root.Left, newLevel));

            if(root.Right != null)
                t2 = Task.Run(()=> weightTreeAsync(root.Right, newLevel));

            return
                (long)root.Weight +
                (root.Left != null ? await t1 : 0) +
                (root.Right != null ? await t2 : 0);

        }

        static void Main(string[] args)
        {
            int treeLevel = 25; // 2^(n+1)-1

            Console.WriteLine($"Starting tree creation with depth {treeLevel}...");
            TreeNode root = new TreeNode();
            CreateRandomTree(root, treeLevel);
            Console.WriteLine($"Tree created with total weight: {total}");


            Stopwatch t1 = new Stopwatch();
            t1.Start();
            // long r1 = weightTree(root);
            long r1 = weightTreeAsync(root).Result;
            t1.Stop();
            Console.WriteLine($"Multi weight: {r1} Time {t1.ElapsedMilliseconds}");

            Stopwatch t2 = new Stopwatch();
            t2.Start();
            r1 = weightTree(root);
            // long r1 = weightTreeAsync(root).Result;
            t2.Stop();
            Console.WriteLine($"Single weight: {r1} Time {t2.ElapsedMilliseconds}");

        }
    }
}