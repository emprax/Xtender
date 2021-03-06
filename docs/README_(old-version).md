# <img src=".\Xtender-logo.png" style="zoom:33%;" /> Xtender

![Nuget](https://img.shields.io/nuget/v/Xtender?color=green&style=plastic)

NuGet package pages:
- [Xtender](https://www.nuget.org/packages/Xtender/)
- [Xtender.DependencyInjection](https://www.nuget.org/packages/Xtender.DependencyInjection/)

## Introduction

A library regarding a segmented visitor to solidify the visitor pattern. The background will explain the origin of the idea and the problem in regards to the SOLID design principles and further sections will explain how the approach works.

Specific use-cases can be found at the end of this document within the section [**Which Problems to solve**](#which-problems-to-solve) and than specifically within the subsection [**Specific UseCases**](#specific-usecases).

## Table of contents

- [Xtender](#xtender)
  * [Background](#background)
    + [V1](#v1)
    + [V2](#v2)
  * [Coding Guide](#coding-guide)
    + [Accepter](#accepter)
    + [Extensions](#extensions)
    + [Construction](#construction)
  * [Which Problems to solve](#which-problems-to-solve)
    * [Specific UseCases](#specific-usecases)

<small><i><a href='http://ecotrust-canada.github.io/markdown-toc/'>Table of contents generated with markdown-toc</a></i></small>

## Background

Xtender, obviously named after the ability to extend, is a library developed to solidify (conform to SOLID) the [Visitor design pattern](https://en.wikipedia.org/wiki/Visitor_pattern) which is used to separate an algorithm from an object structure or simply flexibly adding a new algorithm to such an object.

Where the visitor pattern consists mostly of a single class containing the *Visit* methods for every concrete type in the object structure, the segmented visitor pattern separate this class into segments. Each segment defines a Visit method for a dedicated concrete type.

![Visitor](Visitor.png)

What this adds to the already consistent visitor pattern are the [SOLID](https://en.wikipedia.org/wiki/SOLID) principles, which most notably are:

- The *Single-responsibility principle*, as every segment handles an operation for a single concrete type.
- The *Open-closed principle*, to the point that when the object structure is extended with more concrete types, then the visitor structure is simply extended to add a new segment to the collection instead of modifying a class.
- The *Liskov substitution principle*, as the abstractions of the segments are simple and generic enough that the abstraction works for every type of segment that is added to the structure. The segment abstraction does not contain more methods or data than what every segment has in common with one-another.
- The *Interface segregation principle*, to the point that the interface of the segments does only contain what is necessary. More behaviors are separated into multiple different abstractions when needed.
- The *Dependency inversion principle*, as the library components are all depending on abstractions instead of concrete implementations.

### V1

The first version that was considered when implementing the Xtender library was the use of the Decorator Pattern. Better suited is the Chain-of-responsibility Pattern, because the Decorator is more in line with a pipeline, where every segment is executed until the leading object is reached, as where the Chain-of-responsibility Pattern determines whether a segment, as handler, is suited to do the job.

![Xtender-V1](Xtender-V1.png)

All together a client is used that holds a reference to the root-segment in the chain and executes the operation process by simply calling the entry-point on the root-segment and then traverses through the chain until it finds the suitable segment. This detection is achieved by simply doing a type check and casts the object to the specific concrete implementation it is specified to be. For a simple chain this is no problem of course, but when adding more segments to the chain, it could grow quite fast. The complexity of this solution is of O(n).

### V2

A second version has been established to solve the aforementioned disadvantage in efficiency. The main difference with this version and the previous one is that the handlers/segments are stored in a dictionary instead of chaining them together. 

![Xtender-V2](Xtender-V2.png)

The dictionary, as map-like structure implemented with a hash-map, is a well-known collective data-structure that can save a value on a location that is linked to a key. The key can be of any data-type and calculates a hash-value for that key, which indicates its index in memory. The search functionality of the dictionary is known to be of complexity O(1). Implementing a dictionary as a registry for storing the different segments greatly increases search-efficiency by reducing complexity.



## Coding Guide

This section emphasizes the important components of the library on the basis of some coding examples. We first start with the definition of some of the components in regard to their purpose and location within an application. Be aware that the V2 version is a bit different.

### Accepter

The accepter is the object that can be visited/extended. In most cases this would be an object that is part of a composite structure. The base of the composite should define that the concrete implementations would each define an Accept(...) method that accepts the extender. The reason for this not being abstracted away is that every concrete implementation has to provide itself to the extender, so that the extender can determine the right implementation it should be working on. **NOTE:** This example is shown in the cleanest way possible. It depicts an example regarding the use of the **V2** version as the IExtender only requires to have the state being known, while the **V1** version would also like to know the base-component object.

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

Here the composite-pattern component implements the IAccepter interface and provides the Accept(...) method regarding the interface as an abstract method, so the implementations (Item, Composite) can implement the method. 

### Extensions

The extensions are the segments in that each handles a concrete implementation regarding a composite.

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

Here the ItemExtension is an example of an extension that is responsible for processing the Item implementations regarding the aforementioned composite from the previous section.

In **V2**:

```c#
public class ItemExtension : IExtension<Item>
{
    private IExtender<string> extender;

    public ItemExtension(IExtender<string> extender) => this.extender = extender;

    public Task Extent(Item context)
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

This version is a bit cleaner and easier to use. Furthermore, the Extension abstract-class is no longer needed but it is still provided within the library for developers who insist on using it.

### Construction

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

Here both the aforementioned ItemExtension and the CompositeExtension are linked to the extender by the builder object and the extender can be created by simply calling Build(). The extensions themselves are constructed this way to ensure that they preserve update-to-date dependencies (dependencies with a short lifetime and/or specific temporary data should be retrieved in update-to-date form and should not be inserted while disposed). The IServiceProvider (marked as *provider* in the example) can be used next to the builder to determine the extension dependencies. 

The extender itself has already been defined within the library and carries the extensions as well as the visitor-state. This state type is determined by the AddXtender<TAccepter, TState>(...) method, where the TState serves this purpose. The state is used to carry specific data that would, in the old situation, have been preserved by the Visitor Pattern its own visitor class and could be updated when visiting the accepting objects. So the state in this implementation provides its take on that regard.

```C#
// An example composition.
var composition = new Composite
{
    Components = new List<Component>
    {
        new Item(),
        new Item(),
        new Composite() 
        {
            new Item(),
            new Item(),
            new Item()
        }
    }
};

// Getting the extender from the ServiceProvider from the DI.
var extender = services.GetRequiredService<IExtender<Component, string>>();  // V1 version. V2 does not have the Component generic

// The extension process here as adding new operations.
await extender.Extent(composition);
```

The **V2** version is nearly identical to the V1, but does not provide the base-component with a generic parameter, what makes it easier to be more dynamic.

## Which Problems to solve

Possible cases for which this library can be used:

- Using a more dynamic version of the Visitor Pattern that conforms more to the SOLID design principles.

- When needed to traverse a tree-structure/composition where the amount of concrete component implementations could change.

- Recycle algorithmic components and compose a client (*extender*) with a custom set of combinations of these components.

- Extensive validation purposes.

- Using it as an intermediate layer in between the services and the domain models, where the domain models could be composed into a compositional structure. For that it is no longer a far stretch to be open minded for the idea of using the composite pattern, because the negative effects of not being able to directly access the discrete implementation of the model is mostly removed.

Be aware that even though this solution is quite performant with the key-value data structure, the standard Visitor Pattern is still a good fit for more direct and easy to solve problems.

### Specific UseCases

A possible use-case for this algorithm would be something like a school-system or an health-insurance system. 

- The school-system could for example house many new registered schools which signed an alignment with your school-administration application. Now consider that a lot of different schools have a lot of different layouts in, i.e., their learning paths and subject ordering to name a few instances. At that point it would be quite obvious to use the composite pattern to map all the different possible components to define a varying constructible school-layout modelling system so that every school registration can be established to the heart-desire of the school itself. 

  Now it would be quite difficult to apply rules directly on the composite structure, so the visitor pattern would be an obvious choice to achieve accessibility. Now with multiple schools being attached at different times and schools that are already registered having differentiating demands in their structures could be problematic to maintain with a simple visitor. That has to do with a lot of changes that have to be applied, while this could also bring a lot of new problems. So a nice solution would be to use the Xtender library to solve those problems as well.

- The health-insurance system would take quite a similar approach as the school-system. Now instead of school-layouts this would be the something like the layout of different policies or customizable declarations/contracts in regards to treatments for patients that can be more fine-grained and flexible. Consider for moment that those structures would be quite extensive as multiple hundreds of different combinations are possible. So again to apply rules, operations, validations or whatever to the structure, it would be quite useful to apply the Xtender library for this specific use-case.
