﻿// LeetCode doesn't make use of null-state analysis
#nullable disable

using System.Collections.Generic;

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
     * 144. Binary Tree Preorder Traversal
     * https://leetcode.com/problems/binary-tree-preorder-traversal/
     * 
     * Recursive solution, check below for iterative.
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
    public IList<int> PreorderTraversal_Iterative(TreeNode root)
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
}