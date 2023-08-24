using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public static class SOExtension
	{
		public static IEnumerable<TreeNode> FlattenTree(this TreeView tv)
		{
			return tv.Nodes.FlattenTree();
		}

		public static IEnumerable<TreeNode> FlattenTree(this TreeNodeCollection coll)
		{
			return coll.Cast<TreeNode>().Concat(coll.Cast<TreeNode>().SelectMany((TreeNode x) => x.Nodes.FlattenTree()));
		}
	}
}
