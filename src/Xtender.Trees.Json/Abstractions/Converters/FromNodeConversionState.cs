using System.Collections.Generic;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public class FromNodeConversionState<TTransferObject> where TTransferObject : class
{
    public FromNodeConversionState(TypeKeyProvider provider) => this.Provider = provider;

    public ICollection<TTransferObject> Results { get; } = [];

    public TypeKeyProvider Provider { get; }
}
