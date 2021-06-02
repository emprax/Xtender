using System.Collections.Generic;
using System.Linq;

namespace Xtender.Trees.Extensions
{
    /// <summary>
    /// Node-extensions.
    /// </summary>
    public static class NodeExtensions
    {
        /// <summary>
        /// Gets nodes in a pre-ordered enumeration
        /// </summary>
        /// <param name="node">The node from which the the pre-ordering is achieved.</param>
        /// <returns>Enumeration of pre-ordered nodes.</returns>
        public static IEnumerable<INode> PreOrder(this INode node)
        {
            yield return node;
            foreach (var child in node?.Children ?? Enumerable.Empty<INode>())
            {
                foreach (var n in child.PreOrder())
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// Gets nodes in a post-ordered enumeration
        /// </summary>
        /// <param name="node">The node from which the the post-ordering is achieved.</param>
        /// <returns>Enumeration of post-ordered nodes.</returns>
        public static IEnumerable<INode> PostOrder(this INode node)
        {
            foreach (var child in node?.Children ?? Enumerable.Empty<INode>())
            {
                foreach (var n in child.PostOrder())
                {
                    yield return n;
                }
            }

            yield return node;
        }
    }
}