using System.Collections.Generic;

namespace Xtender.Trees.Abstractions.Converters;

public class FromNodeConversionState<TTransferObject> where TTransferObject : class
{
    public ICollection<TTransferObject> Results { get; } = [];
}
