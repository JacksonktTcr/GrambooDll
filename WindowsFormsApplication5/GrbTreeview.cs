using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REGAL.Classes
{
    class GrbTreeview : TreeView
    {
       public GrbTreeview()
        {
           
        }

       public long DoubleclickTicks = 0;
       public object DoubleclickTicksLock = new object();
       private List<TreeNode> trcol  ;

       protected override void WndProc(ref Message m)
       {
           // Suppress WM_LBUTTONDBLCLK
           if (m.Msg == 0x203)
           {
               long ticksdifference;
               lock (DoubleclickTicksLock)
               {
                   ticksdifference = DateTime.Now.Ticks - DoubleclickTicks;
               }
               if (ticksdifference < TimeSpan.TicksPerSecond)
                   m.Result = IntPtr.Zero;
               else
                   base.WndProc(ref m);
           }
           else base.WndProc(ref m);
       }



       private List<TreeNode> PrintRecursive(TreeNode treeNode)
       {

           
           // Print each node recursively.
           foreach (TreeNode tn in treeNode.Nodes)
           {
               trcol.Add(tn);
               PrintRecursive(tn);
           }
           return trcol;
       }

       // Call the procedure using the TreeView.
       public List<TreeNode> GetAllNodes()
       {
           trcol = new List<TreeNode>();
           trcol.Clear();
           TreeNodeCollection nodes = this.Nodes;
           foreach (TreeNode n in nodes)
           {
               trcol.Add(n);
               PrintRecursive(n);
           }

           return trcol;
       }
    }
}