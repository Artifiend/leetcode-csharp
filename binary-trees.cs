// LeetCode doesn't make use of null checks
#nullable disable

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
    // BTW, did you notice that an order of swapping
    //  and inverting child nodes isn't important?
}