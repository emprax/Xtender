# Xtender
A segmented visitor library to solidify the visitor pattern.



#### Background

Xtender, obviously named after the ability to extend, is a library developed to solidify the [Visitor design pattern](https://en.wikipedia.org/wiki/Visitor_pattern) which is used to separate an algorithm from an object structure or simply dynamically add a new algorithm to such an object.

Where the visitor pattern consists mostly of a single class containing the *Visit* methods for every concrete type in the object structure, the segmented visitor pattern separate this class into segments where each segment defines a Visit method for a dedicated concrete type.

What this adds to the already consistent visitor pattern are the [SOLID](https://en.wikipedia.org/wiki/SOLID) principles, which most notably are:

- The *Single-responsibility principle*, as every segments handles an operation for a single concrete type.
- The *Open-closed principle*, as when the object structure is extended with more concrete types, then the visitor structure is simply extended to add a new segment to the collection instead of modifying a class.
- The *Liskov substitution principle*, as the abstractions of the segments are simple and generic enough that the abstraction works for every type of segment that is added. The segment abstraction does not contain more methods or data than what every segment has in common with one-another.
- The *Interface segregation principle*, as the interface of the segments does only contain what is necessary. More behaviors are separated into multiple different abstractions when needed.
- The *Dependency inversion principle*, as the library components are all depending on abstractions instead of concrete implementations.

#### V1

The first version that was considered when implementing the Xtender library was the use of the Decorator Pattern. Better suited is the Chain-of-responsibility Pattern, because the Decorator is more in line with a pipeline, where every segment is executed until the leading object is reached, as where the Chain-of-responsibility Pattern determines whether a segment, as handler, is suited to do the job.

All together a client is used that holds a reference to the root-segment in the chain and executes the operation process by simply calling the entry-point on the root-segment and then traverses through the chain until it finds the suitable segment, where this detection is achieved by simply doing a type check and casts the object to the specific concrete implementation it is specified to be. For a simple chain this is no problem of course, but when adding more segments to the chain, then it could grow quite fast. The complexity of this solution is of O(n).

#### V2

A second version has been established to solve the aforementioned disadvantage in efficiency. The main difference with this version and the previous one is that the handlers/segments are stored in a dictionary instead of chaining them together. 

The dictionary, as map-like structure, is a well-known collective data-structure that can save a value on a location that is linked to a key, where the key can be of any data-type and calculates a hash-value for that key, which indicates its index in memory. The search functionality of the dictionary is known to be of complexity O(1). Implementing a dictionary as a registry for storing the different segments greatly increases search-efficiency by reducing complexity.