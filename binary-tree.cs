﻿// LeetCode doesn't make use of null-state analysis
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
            CheckSymmetry(root.left, root.right);
        
        bool CheckSymmetry(TreeNode p, TreeNode q)
        {
            if(p is null || q is null) {
                return p == q;
            }
            
            return
                p.val == q.val &&
                CheckSymmetry(p.left, q.right) &&
                CheckSymmetry(p.right, q.left);
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
        Traverse(root, 0);
        return result;
        
        void Traverse(TreeNode node, int level)
        {
            if(node is null) {
                return;
            }
            
            if(result.Count <= level) {
                result.Add(new List<int>());
            }
            
            result[level].Add(node.val);
            
            level++;
            Traverse(node.left, level);
            Traverse(node.right, level);
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
     * 104. Maximum Depth of Binary Tree
     * https://leetcode.com/problems/maximum-depth-of-binary-tree/
     */
    public int MaxDepth(TreeNode root)
    {
        if(root is null) {
            return 0;
        }
        
        return 1 + Math.Max(
            MaxDepth(root.left),
            MaxDepth(root.right)
        );
    }
    
    /*
     * 110. Balanced Binary Tree
     * https://leetcode.com/problems/balanced-binary-tree/
     */
    public bool IsBalanced(TreeNode root)
    {
        if(root is null) {
            return true;
        }
        
        return
            Math.Abs(Height(root.left) - Height(root.right)) <= 1 &&
            IsBalanced(root.left) &&
            IsBalanced(root.right);
        
        int Height(TreeNode root)
        {
            if(root is null) {
                return 0;
            }
            
            return 1 + Math.Max(
                Height(root.left),
                Height(root.right)
            );
        }
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
        
        int left = MinDepth(root.left);
        int right = MinDepth(root.right);
        
        return (left * right) == 0
            ? 1 + Math.Max(left, right)
            : 1 + Math.Min(left, right);
    }
    
    /*
     * 112. Path Sum
     * https://leetcode.com/problems/path-sum/
     */
    public bool HasPathSum(TreeNode root, int targetSum, int sum = 0)
    {
        if(root is null) {
            return false;
        }
        
        sum += root.val;
        
        if(root.left is null && root.right is null) {
            return sum == targetSum;
        } else {
            return
                HasPathSum(root.left, targetSum, sum) ||
                HasPathSum(root.right, targetSum, sum);
        }
    }
    
    /*
     * 113. Path Sum II
     * https://leetcode.com/problems/path-sum-ii/
     */
    public IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        var result = new List<IList<int>>();
        Helper(root, new List<int>());
        return result;
        
        void Helper(TreeNode root, IList<int> currentPath)
        {
            if(root is null) {
                return;
            }

            currentPath.Add(root.val);

            if(root.left is null && root.right is null) {
                if(currentPath.Sum() == targetSum) {
                    result.Add(currentPath);
                }
            } else {
                // clone on the left child, in-place on the right
                Helper(root.left, new List<int>(currentPath));
                Helper(root.right, currentPath);
            }
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
     * 199. Binary Tree Right Side View
     * https://leetcode.com/problems/binary-tree-right-side-view/
     */
    public IList<int> RightSideView(TreeNode root)
    {
        var result = new List<int>();
        Helper(root, 0);
        return result;
        
        void Helper(TreeNode root, int level)
        {
            if(root is null) {
                return;
            }
            
            if(result.Count <= level) {
                result.Add(root.val);
            }
            
            ++level;
            Helper(root.right, level);
            Helper(root.left, level);
        }
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
        
        (root.left, root.right) = (root.right, root.left);
        
        InvertTree(root.left);
        InvertTree(root.right);
        
        return root;
    }
    // BTW, did you notice that order of swapping
    //  and inverting child nodes isn't important?
    
    /*
     * 257. Binary Tree Paths
     * https://leetcode.com/problems/binary-tree-paths/
     */
    public IList<string> BinaryTreePaths(TreeNode root)
    {
        var paths = new List<IList<int>>();
        FindPaths(root, new List<int>());
        
        return
            paths.Select(path =>
                 String.Join("->", path.Select(val => val.ToString()))
             ).ToList();
        
        void FindPaths(TreeNode root, IList<int> currentPath)
        {
            if(root is null) {
                return;
            }

            currentPath.Add(root.val);

            if(root.left is null && root.right is null) {
                paths.Add(currentPath);
            } else {
                // Clone on the left child, in-place on the right
                FindPaths(root.left, new List<int>(currentPath));
                FindPaths(root.right, currentPath);
            }
        }
    }
    
    /*
     * 331. Verify Preorder Serialization of a Binary Tree
     * https://leetcode.com/problems/verify-preorder-serialization-of-a-binary-tree/
     */
    public bool IsValidSerialization(string preorder)
    {
        string[] nodes = preorder.Split(',');
        int count = 1;
        
        for(int i = 0; i < nodes.Length; ++i)
        {
            if(count == 0) {
                return false;
            }
            
            if(nodes[i] == "#") {
                --count;
            } else {
                ++count;
            }
        }
        
        return count == 0;
    }
    
    /*
     * 404. Sum of Left Leaves
     * https://leetcode.com/problems/sum-of-left-leaves/
     */
    public int SumOfLeftLeaves(TreeNode root, bool isLeft = false)
    {
        if(root is null) {
            return 0;
        }
        
        if(root.left is null && root.right is null) {
            return isLeft ? root.val : 0;
        }
        
        return
            SumOfLeftLeaves(root.left, true) +
            SumOfLeftLeaves(root.right, false);
    }
    
    /*
     * 508. Most Frequent Subtree Sum
     * https://leetcode.com/problems/most-frequent-subtree-sum/
     */
    public int[] FindFrequentTreeSum(TreeNode root)
    {
        var sumsFreq = new Dictionary<int, int>();
        Helper(root);
        int maxCount = sumsFreq.Values.Max();
        return
            sumsFreq.Where(d => d.Value == maxCount)
            .Select(d => d.Key).ToArray();
        
        int Helper(TreeNode root)
        {
            if(root is null) {
                return 0;
            }
            
            int sum =
                root.val +
                Helper(root.left) +
                Helper(root.right);
            
            sumsFreq[sum] = CollectionExtensions.GetValueOrDefault(sumsFreq, sum) + 1;
            return sum;
        }
    }
    
    /*
     * 513. Find Bottom Left Tree Value
     * https://leetcode.com/problems/find-bottom-left-tree-value/
     */
    public int FindBottomLeftValue(TreeNode root)
    {
        int maxLevel = -1;
        int result = 0;
        Helper(root, 0);
        return result;
        
        void Helper(TreeNode root, int level)
        {
            if(root is null) {
                return;
            }
            
            if(level > maxLevel) {
                result = root.val;
                maxLevel = level;
            }
            
            ++level;
            Helper(root.left, level);
            Helper(root.right, level);
        }
    }
    
    /*
     * 543. Diameter of Binary Tree
     * https://leetcode.com/problems/diameter-of-binary-tree/
     */
    public int DiameterOfBinaryTree(TreeNode root)
    {
        if(root is null) {
            return 0;
        }
        
        return Math.Max(
            MaxDepth(root.left) + MaxDepth(root.right),
            Math.Max(
                DiameterOfBinaryTree(root.left),
                DiameterOfBinaryTree(root.right)
            )
        );
        
        // Reusing 104
        int MaxDepth(TreeNode root)
        {
            if(root is null) {
                return 0;
            }
            
            return 1 + Math.Max(
                Probe(root.left),
                Probe(root.right)
            );
        }
    }
    
    /*
     * 563. Binary Tree Tilt
     * https://leetcode.com/problems/binary-tree-tilt/
     */
    public int FindTilt(TreeNode root)
    {
        if(root is null) {
            return 0;
        }
        
        int tilt = Math.Abs(
            SumOfTree(root.left) -
            SumOfTree(root.right)
        );
        
        return
            tilt +
            FindTilt(root.left) +
            FindTilt(root.right);
        
        int SumOfTree(TreeNode root)
        {
            if(root is null) {
                return 0;
            }
            
            return
                root.val +
                SumOfTree(root.left) +
                SumOfTree(root.right);
        }
    }
    
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
    
    /*
     * 654. Maximum Binary Tree
     * https://leetcode.com/problems/maximum-binary-tree/
     */
    public TreeNode ConstructMaximumBinaryTree(int[] nums)
    {
        if(nums.Length == 0) {
            return null;
        }
        
        int maxIdx = Array.IndexOf(nums, nums.Max());
        
        var segment = new ArraySegment<int>(nums);
        
        var left = ConstructMaximumBinaryTree(
            segment.Slice(0, maxIdx).ToArray()
        );
        
        var right = ConstructMaximumBinaryTree(
            segment.Slice(maxIdx + 1, nums.Length - 1 - maxIdx).ToArray()
        );
        
        return new TreeNode(nums[maxIdx], left, right);
    }
}
