using System.Windows.Forms;

namespace OctofyLib
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
