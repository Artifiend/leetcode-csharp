// LeetCode doesn't make use of null-state analysis
#nullable disable

using System.Collections.Generic;
using System.Numerics;

// A common binary tree definition
public class TreeNode
{
    public int val;
    public TreeNode left, right;
    
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    private static void Main()
    {
        // Testing area
        
        
        
        // Intentionally left blank
    }
    
    /*
     * 94. Binary Tree Inorder Traversal
     * https://leetcode.com/problems/binary-tree-inorder-traversal/
     */
    public IList<int> InorderTraversal(TreeNode root, IList<int> result = null)
    {
        if(result is null) {
            // Initialize result
            result = new List<int>();
        }
        
        if(root is null) {
            return result;
        }
        
        InorderTraversal(root.left, result);
        result.Add(root.val);
        InorderTraversal(root.right, result);
        
        return result;
    }
    
    /*
     * 100. Same Tree
     * https://leetcode.com/problems/same-tree/
     */
    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        if(p is null || q is null) {
            return p == q;
        }
        
        return
            p.val == q.val &&
            IsSameTree(p.left, q.left) &&
            IsSameTree(p.right, q.right);
    }
    
    /*
     * 101. Symmetric Tree
     * https://leetcode.com/problems/symmetric-tree/
     */
    public bool IsSymmetric(TreeNode root)
    {
        return
            root is null ||
            Helper(root.left, root.right);
        
        bool Helper(TreeNode p, TreeNode q)
        {
            if(p is null || q is null) {
                return p == q;
            }
            
            return
                p.val == q.val &&
                Helper(p.left, q.right) &&
                Helper(p.right, q.left);
        }
    }
    
    /*
     * 102. Binary Tree Level Order Traversal
     * https://leetcode.com/problems/binary-tree-level-order-traversal/
     * 
     * Recursive, see below for iterative.
     */
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var result = new List<IList<int>>();
        Helper(root, 0);
        return result;
        
        void Helper(TreeNode node, int level)
        {
            if(node is null) {
                return;
            }
            
            if(result.Count <= level) {
                result.Add(new List<int>());
            }
            
            result[level].Add(node.val);
            
            level++;
            Helper(node.left, level);
            Helper(node.right, level);
        }
    }
    // Iterative 102.
    public IList<IList<int>> LevelOrder_iterative(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var result = new List<IList<int>>();
        
        if(root is null) {
            return result;
        }
        
        queue.Enqueue(root);
        
        while(queue.Count > 0)
        {
            var levelList = new List<int>();
            var nextQueue = new Queue<TreeNode>();
            
            while(queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                levelList.Add(node.val);
                
                if(node.left != null) {
                    nextQueue.Enqueue(node.left);
                }
                
                if(node.right != null) {
                    nextQueue.Enqueue(node.right);
                }
            }
            
            result.Add(levelList);
            queue = nextQueue;
        }
        
        return result;
    }
    
    /*
     * 111. Minimum Depth of Binary Tree
     * https://leetcode.com/problems/minimum-depth-of-binary-tree/
     */
    public int MinDepth(TreeNode root)
    {
        if(root is null) {
            return 0;
        }
        
        return Helper(root, 1);
        
        int Helper(TreeNode node, int level)
        {
            if(node.left is null && node.right is null) {
                return level;
            }
            
            level++;
            return Math.Min(
                node.left is null ? int.MaxValue : Helper(node.left, level),
                node.right is null ? int.MaxValue : Helper(node.right, level)
            );
        }
    }
    
    /*
     * 144. Binary Tree Preorder Traversal
     * https://leetcode.com/problems/binary-tree-preorder-traversal/
     * 
     * Recursive, see below for iterative.
     */
    public IList<int> PreorderTraversal(TreeNode root, IList<int> result = null)
    {
        if(result is null) {
            // Initialize result
            result = new List<int>();
        }
        
        if(root is null) {
            return result;
        }
        
        result.Add(root.val);
        PreorderTraversal(root.left, result);
        PreorderTraversal(root.right, result);
        
        return result;
    }
    // Iterative 144.
    public IList<int> PreorderTraversal_iterative(TreeNode root)
    {
        var result = new List<int>();
        var stack = new Stack<TreeNode>();
        
        // Dummy
        stack.Push(null);
        
        TreeNode tree = root;
        
        while(tree != null)
        {
            result.Add(tree.val);
            
            if(tree.right != null) {
                stack.Push(tree.right);
            }
            
            tree = tree.left ?? stack.Pop();
        }
        
        return result;
    }
    
    /*
     * 145. Binary Tree Postorder Traversal
     * https://leetcode.com/problems/binary-tree-postorder-traversal/
     */
    public IList<int> PostorderTraversal(TreeNode root, IList<int> result = null)
    {
        if(result is null) {
            // Initialize result
            result = new List<int>();
        }
        
        if(root is null) {
            return result;
        }
        
        PostorderTraversal(root.left, result);
        PostorderTraversal(root.right, result);
        result.Add(root.val);
        
        return result;
    }
    
    /*
     * 226. Invert Binary Tree
     * https://leetcode.com/problems/invert-binary-tree/
     */
    public TreeNode InvertTree(TreeNode root)
    {
        if(root is null) {
            return null;
        }
        
        // Swap child nodes
        (root.left, root.right) = (root.right, root.left);
        
        // Go invert children
        InvertTree(root.left);
        InvertTree(root.right);
        
        return root;
    }
    // BTW, did you notice that order of swapping
    //  and inverting child nodes isn't important?
    
    /*
     * 617. Merge Two Binary Trees
     * https://leetcode.com/problems/merge-two-binary-trees/
     */
    public TreeNode MergeTrees(TreeNode first, TreeNode second)
    {
        if(first is null || second is null) {
            return first ?? second;
        }
        
        first.val += second.val;
        first.left = MergeTrees(first.left, second.left);
        first.right = MergeTrees(first.right, second.right);
        
        return first;
    }
    
    /*
     * 637. Average of Levels in Binary Tree
     * https://leetcode.com/problems/average-of-levels-in-binary-tree/
     * 
     * NOTE: Using long instead of BigInteger will count as completed on LeetCode,
     *       but wouldn't be safe due to overflow on wider trees.
     * 
     * Recursive, see below for iterative.
     */
    public IList<double> AverageOfLevels(TreeNode root)
    {
        var sum = new List<BigInteger>();
        var length = new List<int>();
        Helper(root, 0);
        return sum.Zip(length, (lvlSum, lvlLen) => ((double) lvlSum) / lvlLen).ToList();
        
        void Helper(TreeNode node, int level)
        {
            if(node is null) {
                return;
            }
            
            if(sum.Count <= level) {
                // A new level
                sum.Add(node.val);
                length.Add(1);
            } else {
                sum[level] += node.val;
                length[level]++;
            }
            
            level++;
            Helper(node.left, level);
            Helper(node.right, level);
        }
    }
    // Alternative 637.
    // Obsolete due to absence of built-in rationals in .NET.
    //
    // NOTE: Will count as completed on LeetCode,
    //       but floating-point roundoff error will bite on wider trees.
    public IList<double> AverageOfLevels_alternative(TreeNode root)
    {
        var result = new List<double>();
        var nodesInLevel = new List<int>();
        Helper(root, 0);
        return result;
        
        void Helper(TreeNode node, int level)
        {
            if(node is null) {
                return;
            }
            
            if(result.Count <= level) {
                // A new level
                result.Add((double) node.val);
                nodesInLevel.Add(1);
            } else {
                nodesInLevel[level]++;
                // Using a slight variation of:
                // https://en.wikipedia.org/wiki/Moving_average#Cumulative_average
                result[level] -= (result[level] - node.val) *
                    Math.ReciprocalEstimate(nodesInLevel[level]);
            }
            
            level++;
            Helper(node.left, level);
            Helper(node.right, level);
        }
    }
    // Iterative 637.
    public IList<double> AverageOfLevels_iterative(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var result = new List<double>();
        
        if(root is null) {
            return result;
        }
        
        queue.Enqueue(root);
        
        while(queue.Count > 0)
        {
            int count = 0;
            var sum = new BigInteger(0);
            var nextQueue = new Queue<TreeNode>();
            
            while(queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                sum += node.val;
                count++;
                
                if(node.left != null) {
                    nextQueue.Enqueue(node.left);
                }
                
                if(node.right != null) {
                    nextQueue.Enqueue(node.right);
                }
            }
            
            result.Add(((double) sum) / count);
            queue = nextQueue;
        }
        
        return result;
    }
}