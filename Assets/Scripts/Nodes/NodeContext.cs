using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nodes
{
    public class NodeContext : MonoBehaviour
    {
        public bool Interactable { get; set; }
        
        public List<NodeContext> surroundingNodes = new List<NodeContext>(4);
        [HideInInspector] public List<NodeContext> childrenNodes;
        [HideInInspector] public NodeContext parentNode;
        
        [Header("Node Properties")]
        public GameObject assignedNodeObject;
        public NodeContent content;
        public Vector2 position;

        public NodeContext GetParent
        {
            get
            {
                var nodeContext = this;
                while (nodeContext.parentNode != null)
                {
                    nodeContext = nodeContext.parentNode;
                }

                return nodeContext;
            }
        }

        public int ChildrenCount
        {
            get
            {
                return childrenNodes.Count + childrenNodes.Sum(context => context.ChildrenCount);
            }
        }

    }
}