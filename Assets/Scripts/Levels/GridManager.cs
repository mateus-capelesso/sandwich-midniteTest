using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using InputManagement;
using Nodes;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public abstract class GridManager : MonoBehaviour
    {
        public const int GridSizeX = 4;
        public const int GridSizeY = 4;
        public const float NodeHeight = 0.1f;

        public CameraMovement cameraMovement;

        public const float NodeSizeX = 1f;
        public const float NodeSizeY = 1f;

        protected NodeContext LastSelectedNodeContext;
        protected bool GameIsOver;
        protected bool BlockInputsWhileTween;

        protected List<NodeContext> Grid;
        public UnityEvent onSwipeSuccessful;

        protected virtual void Start()
        {
            LevelManager.OnNewLevelStart += ClearAndInstantiateGrid;
            InputManager.OnSwipeDetected += OnSwipe;
        }

        public abstract void InstantiateGrid();

        protected virtual void MoveStack(NodeContext selectedNode, NodeContext targetNode, Direction direction)
        {
            onSwipeSuccessful?.Invoke();
        
            BlockInputsWhileTween = true;

            selectedNode.Interactable = false;
            selectedNode.parentNode = targetNode;
            targetNode.childrenNodes.Add(selectedNode);
            
            Grid.Find(n => n == selectedNode).position = targetNode.position;
            selectedNode.assignedNodeObject.transform.SetParent(targetNode.assignedNodeObject.transform);
            LastSelectedNodeContext = selectedNode;
        }

        // Gets the parent object of the stack, and check if the movement is available for its position.
        private void OnSwipe(Direction swipeDirection, GameObject selectedNode)
        {
            var selectedContext = selectedNode.GetComponent<NodeContext>();
            var selectedStack = selectedContext.GetParent;

            if (GameIsOver || BlockInputsWhileTween || !selectedStack.Interactable || !selectedNode.CompareTag("Nodes"))
            {
                TweenRotateStackFake(selectedStack, swipeDirection);
                return;
            }

            var validDirections = GetValidSurroundings(selectedStack);
            if (validDirections.Contains(swipeDirection))
                MoveStack(selectedStack, selectedStack.surroundingNodes[(int) swipeDirection], swipeDirection);
            else
                TweenRotateStackFake(selectedStack, swipeDirection);

        }

        // Analyse and add surrounding data to node context
        protected List<NodeContext> GetSurroundingContexts(Vector2 position)
        {
            var top = GetContextFromPosition(new Vector2(position.x, position.y - 1));
            var right = GetContextFromPosition(new Vector2(position.x - 1, position.y));
            var bottom = GetContextFromPosition(new Vector2(position.x, position.y + 1));
            var left = GetContextFromPosition(new Vector2(position.x + 1, position.y));
            return new List<NodeContext>{top, right, bottom, left};
        }
        
        // It returns null if can't find node from position
        protected NodeContext GetContextFromPosition(Vector2 position)
        {
            return Grid.FirstOrDefault(context => context.position == position);
        }
        
        protected void AssignSurroundingsToContext()
        {
            foreach (var nodeContext in Grid)
            {
                var nodePosition = nodeContext.position;
                nodeContext.surroundingNodes = GetSurroundingContexts(nodePosition);
            }
        }

        protected Vector3 CalculateNodePosition(Vector2 gridPosition)
        {
            return new Vector3(NodeSizeX * gridPosition.x, 0, NodeSizeY * gridPosition.y);
        }

        protected List<Direction> GetValidSurroundings(NodeContext ingredient)
        {
            var availableDirections = new List<Direction>();
            foreach (var surrounding in ingredient.surroundingNodes)
            {
                if (surrounding != null && surrounding.Interactable)
                    availableDirections.Add((Direction) ingredient.surroundingNodes.IndexOf(surrounding));
            }

            return availableDirections;
        }

        // Translates direction to a Vector3, for movement and rotation purposes.
        protected Vector3 GetVectorFromDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                    return Vector3.left;
                case Direction.Right:
                    return Vector3.forward;
                case Direction.Bottom:
                    return Vector3.right;
                case Direction.Left:
                    return Vector3.back;
                default:
                    return Vector3.zero;
            }
        }

        protected float GetNodeHeight(int childrenCount)
        {
            var height = NodeHeight;
            height += childrenCount * NodeHeight;

            return height;
        }

        private void TweenRotateStackFake(NodeContext stack, Direction direction)
        {
            var strenght = GetVectorFromDirection(direction);
            var end = new Vector3(Mathf.Abs(strenght.x), Mathf.Abs(strenght.y), Mathf.Abs(strenght.z));
            
            stack.transform.DOShakeRotation(0.25f, end * 20f, 5)
                .SetEase(Ease.InOutBack);
        }

        protected void TweenMoveStackIntoAnother(Transform stack, Vector2 targetStackPosition, float totalHeight, Action onComplete)
        {
            // Tween movement with bezier path, from actual, to midpoint, to targetPosition, when tween is completed, enable new movement from swipe
            var targetPosition = new Vector3(
                targetStackPosition.x,
                totalHeight, 
                targetStackPosition.y);

            var actualPosition = stack.position;
            var midPosition = new Vector3(actualPosition.x + (targetPosition.x - actualPosition.x) / 2, totalHeight + 1f,
                actualPosition.z + (targetPosition.z - actualPosition.z) / 2);

            stack.DOPath(new[] {midPosition, targetPosition}, 0.3f, PathType.CatmullRom).SetEase(Ease.OutSine).
                OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        protected void TweenRotateStackIntoAnother(NodeContext context, Direction direction)
        {
            var rotate = GetVectorFromDirection(direction) * 180;
            context.assignedNodeObject.transform.DORotate(
                context.assignedNodeObject.transform.rotation.eulerAngles + rotate,
                0.25f).SetEase(Ease.OutSine);
        }
        
        private void ClearAndInstantiateGrid(int level)
        {
            if (Grid != null && Grid.Count > 0)
                ClearGrid();
            InstantiateGrid();
        }

        private void ClearGrid()
        {
            foreach (var node in Grid)
            {
                Destroy(node.gameObject);
            }

            Grid.Clear();

            LastSelectedNodeContext = null;
            GameIsOver = false;
            BlockInputsWhileTween = false;
        }
    }
}