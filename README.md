# Xtender
A segmented visitor library to solidify the visitor pattern.

[TOC]

### Background

Xtender, obviously named after the ability to extend, is a library developed to solidify the [Visitor design pattern](https://en.wikipedia.org/wiki/Visitor_pattern) which is used to separate an algorithm from an object structure or simply dynamically add a new algorithm to such an object.

Where the visitor pattern consists mostly of a single class containing the *Visit* methods for every concrete type in the object structure, the segmented visitor pattern separate this class into segments where each segment defines a Visit method for a dedicated concrete type.

![Visitor](docs/Visitor.png)

What this adds to the already consistent visitor pattern are the [SOLID](https://en.wikipedia.org/wiki/SOLID) principles, which most notably are:

- The *Single-responsibility principle*, as every segments handles an operation for a single concrete type.
- The *Open-closed principle*, as when the object structure is extended with more concrete types, then the visitor structure is simply extended to add a new segment to the collection instead of modifying a class.
- The *Liskov substitution principle*, as the abstractions of the segments are simple and generic enough that the abstraction works for every type of segment that is added. The segment abstraction does not contain more methods or data than what every segment has in common with one-another.
- The *Interface segregation principle*, as the interface of the segments does only contain what is necessary. More behaviors are separated into multiple different abstractions when needed.
- The *Dependency inversion principle*, as the library components are all depending on abstractions instead of concrete implementations.

#### V1

The first version that was considered when implementing the Xtender library was the use of the Decorator Pattern. Better suited is the Chain-of-responsibility Pattern, because the Decorator is more in line with a pipeline, where every segment is executed until the leading object is reached, as where the Chain-of-responsibility Pattern determines whether a segment, as handler, is suited to do the job.

![Xtender-V1](docs/Xtender-V1.png)

All together a client is used that holds a reference to the root-segment in the chain and executes the operation process by simply calling the entry-point on the root-segment and then traverses through the chain until it finds the suitable segment, where this detection is achieved by simply doing a type check and casts the object to the specific concrete implementation it is specified to be. For a simple chain this is no problem of course, but when adding more segments to the chain, then it could grow quite fast. The complexity of this solution is of O(n).

#### V2

A second version has been established to solve the aforementioned disadvantage in efficiency. The main difference with this version and the previous one is that the handlers/segments are stored in a dictionary instead of chaining them together. 

![Xtender-V2](docs/Xtender-V2.png)

The dictionary, as map-like structure, is a well-known collective data-structure that can save a value on a location that is linked to a key, where the key can be of any data-type and calculates a hash-value for that key, which indicates its index in memory. The search functionality of the dictionary is known to be of complexity O(1). Implementing a dictionary as a registry for storing the different segments greatly increases search-efficiency by reducing complexity.



### Coding Guide

This section emphasizes the important components of the library on the basis of some coding examples. We first start with the definition of some of the components in regard to their purpose and location within an application.

#### Accepter

The accepter is the object that can be visited/extended. It most likely would be an object that is part of a composite. The base of the composite should define that the concrete implementations would each define an Accept(...) method that accepts the extender. The reason for this not to be abstracted away is that every concrete implementation has to provide itself to the extender so the extender can determine the right implementation it should be working on.

```c#
public abstract Component : IAccepter
{
    public abstract Task Accept<TState>(IExtender<TState> extender);
}

public class Item : Component
{
    public string Context { get; set; }
    
    public override Task Accept<TState>(IExtender<TState> extender) => extender.Extent(this);
}

public class Composite : Component
{
    public IList<Component> Components { get; set; }
    
    public override Task Accept<TState>(IExtender<TState> extender) => extender.Extent(this);
}
```

Here the composite-pattern-component implements the IAccepter interface and provide the Accept(...) method regarding the interface as an abstract method, so the implementations (Item, Composite) can implement the method.

#### Extensions

The extensions are the segments in that each handle a concrete implementation regarding a composite.

```c#
public class ItemExtension : Extension<string, Component, Item>
{
    public ItemExtension(IExtender<Component, string> extender) : base(extender) { }

    protected override Task Extent(Item context)
    {
        System.Console.WriteLine("Entered ItemExtension");
        if (base.extender.State is null)
        {
            base.extender.State = "Encountered an Item regarding Component.";
        }
        else
        {
            base.extender.State += "Encountered an Item regarding Component.";
        }

        return Task.CompletedTask;
    }
}
```

Here the ItemExtension is an example of an extension that is responsible for processing the Item implementations regarding the aforementioned composite.

#### Construction

To make life as a developer easier, the library comes with a ServiceCollection extension that can be used to easily implement the extender components and by building up the Extender client with its segments/extensions.

```c#
var services = new ServiceCollection()
    .AddXtender<Component, string>((builder, provider) =>
    {
        return builder
            .Attach(extender => new ItemExtension(extender))
            .Attach(extender => new CompositeExtension(extender))
            .Build();
    })
    .BuildServiceProvider();
```

Here both the aforementioned ItemExtension and the CompositeExtension are being linked by the builder object and the extender can be created by simply calling Build(). The extensions themselves are being constructed this way to ensure that they perserve update-to-date dependencies (dependencies with a short lifetime and/or specific temporary data should be retrieved in update-to-date form and should not be inserted while disposed). The IServiceProvider can be used next to the builder to determine the extension dependencies. 

The client itself has already been defined within the library and carries the extensions as well as the visitor-state. This state type is being determined by the AddXtender<TAccepter, TState>(...) method, where the TState serves this purpose. The state is used to carry specific data that would, in the old situation, have been preserved by the Visitor Pattern its own visitor class and could be updated when visiting the accepting objects. So the state in this implementation provides its take on that regard.



### Which Problems to solve

Possible cases to use this library:

1. Using a more dynamic version of the Visitor Pattern that conforms to the SOLID design principles.
2. When needed to traverse a tree-structure/composition where the amount of concrete component implementations could change.
3. Recycle algorithmic components and compose a client with a custom set of combinations of these components.
4. Using it as an intermediate layer in between the services and the domain models, where the domain models could be composed into a compositional structure. For that it is no longer a far stretch to be open minded for the idea of using the composite pattern, because the negative effects of not being able to directly access the discrete implementation of the model is mostly removed.

Be aware that even though this solution is quite performant with the key-value data structure, the standard Visitor Pattern is still a good fit for more direct and easy to solve problems.