using System.Windows.Forms;

namespace OctofyExp
{
    class FakeChildNode : TreeNode
    {
        public FakeChildNode(TreeNode parent)
            : base()
        {
            parent.Nodes.Add(this);
        }
    }
}
