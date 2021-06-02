using System.Collections.Generic;
using System.Linq;

namespace Xtender.Trees.Extensions
{
    /// <summary>
    /// Extensions for the tree data structure.
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// Enumeration method to get nodes in a pre-ordered fashion.
        /// </summary>
        /// <param name="tree">The tree of interest.</param>
        /// <returns>The enumeration of nodes in pre-ordered fashion.</returns>
        public static IEnumerable<INode> PreOrder(this ITree tree) => tree?.Root?.PreOrder() ?? Enumerable.Empty<INode>();

        /// <summary>
        /// Enumeration method to get nodes in a post-ordered fashion.
        /// </summary>
        /// <param name="tree">The tree of interest.</param>
        /// <returns>The enumeration of nodes in post-ordered fashion.</returns>
        public static IEnumerable<INode> PostOrder(this ITree tree) => tree?.Root?.PostOrder() ?? Enumerable.Empty<INode>();
    }
}