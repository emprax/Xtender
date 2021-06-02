using System.Collections.Generic;

namespace Xtender.Trees
{
    /// <summary>
    /// The interface for the tree structure.
    /// </summary>
    public interface ITree : IAccepter, IEnumerable<INode>
    {
        /// <summary>
        /// Root-node of the tree.
        /// </summary>
        INode Root { get; set; }

        /// <summary>
        /// Method to get a node from the tree by its id.
        /// </summary>
        /// <param name="id">The identification of the node that is requested.</param>
        /// <returns>The found result, otherwise it will be null.</returns>
        INode GetNode(string id);

        /// <summary>
        /// Method to insert a node by value as child on a certain parent node identified by the parent-id.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="parentId">The identity of the node where the to-be created node with its value has to be attached to.</param>
        /// <param name="value">The value for the to-be created node.</param>
        /// <returns>The created node with its value.</returns>
        INode<TValue> InsertValue<TValue>(string parentId, TValue value);

        /// <summary>
        /// Method to insert a node by value as child on a certain parent node identified by the parent-id.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="parentId">The identity of the node where the to-be created node with its value has to be attached to.</param>
        /// <param name="id">The id that is given to the to-be created node.</param>
        /// <param name="value">The value for the to-be created node.</param>
        /// <returns>The created node with its value.</returns>
        INode<TValue> InsertValue<TValue>(string parentId, string id, TValue value);

        /// <summary>
        /// Method to insert multiple nodes as children on a certain parent node identified by the parent-id.
        /// </summary>
        /// <param name="parentId">The identity of the parent node where the inserted nodes have to be attached to.</param>
        /// <param name="nodes">The inserted nodes.</param>
        /// <returns>Indication whether the operation was successful.</returns>
        bool InsertRange(string parentId, params INode[] nodes);

        /// <summary>
        /// Method to insert a node as child on a certain parent node identified by the parent-id.
        /// </summary>
        /// <param name="parentId">The identity of the parent node where the inserted node has to be attached to.</param>
        /// <param name="node">The inserted node.</param>
        /// <returns>Indication whether the operation was successful.</returns>
        bool Insert(string parentId, INode node);

        /// <summary>
        /// Method to remove a specific node, identified by its id.
        /// </summary>
        /// <param name="id">The identity of the to-be deleted node.</param>
        /// <returns>Indication whether the operation was successful.</returns>
        bool Remove(string id);
    }
}