using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public NodeContext parentNode;
        
        [Header("Node Properties")]
        public GameObject assignedNodeObject;
        public int content;
        public Vector2 position;

        public NodeContext GetParent
        {
            get
            {
                var node = this;
                while (node.parentNode != null)
                {
                    node = node.parentNode;
                }

                return node;
            }
        }

        public void AddChildren(NodeContext child)
        {
            if (!childrenNodes.Contains(child))
            {
                childrenNodes.Add(child);
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