using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Core.Graphics
{
    public class SceneGraph
    {
        public List<ILeafNode> Nodes { get; private set; }
        public SceneGraph()
        {
            Nodes = new List<ILeafNode>();
        }

        public void Render()
        {
            foreach (var node in Nodes)
            {
                node.Render();
            }
        }

        public void Clear()
        {
            Nodes.Clear();
        }
    }

    public interface IGroupNode
    {
        
    }

    public interface ILeafNode
    {
        void Render();
    }
}
