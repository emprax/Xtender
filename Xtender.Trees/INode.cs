using System;
using System.Collections.Generic;
using Xtender.Trees.Builders;

namespace Xtender.Trees
{
    /// <summary>
    /// The value-based node interface for nodes of a tree.
    /// </summary>
    /// <typeparam name="TValue">The type of the value the node stores.</typeparam>
    public interface INode<out TValue> : INode
    {
        /// <summary>
        /// Getting the value that is stored in the node.
        /// </summary>
        TValue Value { get; }
    }

    /// <summary>
    /// The basic node interface for nodes of a tree.
    /// </summary>
    public interface INode : IAccepter, IEnumerable<INode>
    {
        /// <summary>
        /// Getting the identification of the node.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the parent node of the this node.
        /// When this property returns null, then this indicates that this node is a root-node.
        /// </summary>
        INode Parent { get; }

        /// <summary>
        /// The child-nodes of this node, functioning as branches sprouting from this node into new paths.
        /// </summary>
        IReadOnlyCollection<INode> Children { get; }

        /// <summary>
        /// Method to add a node to the children-collection within this node.
        /// </summary>
        /// <param name="node">The node that is added to the children-collection.</param>
        void Add(INode node);

        /// <summary>
        /// Method to add multiple nodes to the children-collection within this node.
        /// </summary>
        /// <param name="nodes">The nodes that are added to the children-collection.</param>
        void AddRange(params INode[] nodes);

        /// <summary>
        /// Method to add a node based on the value that the node will encapsulate.
        /// </summary>
        /// <typeparam name="TValue">The type of the value for in the to-be created node.</typeparam>
        /// <param name="value">The value for in the to-be created node.</param>
        /// <returns>Returns the resulting node with the encapsulated value.</returns>
        INode<TValue> AddValue<TValue>(TValue value);

        /// <summary>
        /// Method to add a node based on the value that the node will encapsulate. Custom id can be assigned to the to-be created node.
        /// </summary>
        /// <typeparam name="TValue">The type of the value for in the to-be created node.</typeparam>
        /// <param name="id">Identity for the newly created node.</param>
        /// <param name="value">The value for in the to-be created node.</param>
        /// <returns>Returns the resulting node with the encapsulated value.</returns>
        INode<TValue> AddValue<TValue>(string id, TValue value);

        /// <summary>
        /// Method to add nodes through the builder approach.
        /// </summary>
        /// <param name="builder">The builder that can be used to fluently add new nodes.</param>
        void Add(Action<INodeBuilder> builder);

        /// <summary>
        /// Method to remove a particular node.
        /// </summary>
        /// <param name="node">The node that has to be removed.</param>
        /// <returns>Indication to whether the deletion of the node was successful.</returns>
        bool Remove(INode node);

        /// <summary>
        /// Method to remove a particular node identified by its id.
        /// </summary>
        /// <param name="id">Id of the node that has to be removed.</param>
        /// <returns>Indication to whether the deletion of the node was successful.</returns>
        bool Remove(string id);
    }
}