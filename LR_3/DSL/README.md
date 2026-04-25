<h1>DSL (Data Structure Library)</h1>

<h2>Classes and methods which need to be implemented on the second layer for proper graphing</h2>

<h3>Types (Enum)</h3>

An enum of all possible DataStructure generation types.<br>
Used by Generator<> and derivatives.

<h3>Benchmarks (struct)</h3>

A struct that represents the results of running a benchmarking test on a DataStructure using an algorithm.<br>
Returned by Algorithm, used by AlgorithmPlot.

<h3>Generator (class)</h3>

A class responcible for the generation of DataStructures.<br>
Uses Types to determine creation types of DataStructure.<br>
Needs to implement `Generate(Type type, int size)` itself.

<h2>Classes which are tested and requirements for them</h2>

<h3>Algorithm</h3>

An algorithm, contained in an instance.<br>
Can be anonymous and complexity-less, although some graphing functionalities will stop working if that is enabled.
Must be initialized with a `Func<DataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)>`.

<h3>DataStructure</h3>

Data structure on which the algorithm is applied.<br>
Can be any class which Algorithm accepts, but DSL is intended for benchmarking complex algorithms on data structures, not for simple operations or simple classes.

<h2>DSL Usage</h2>

<h3>AlgorithmPlot</h3>

Inherits from `ScottPlot.Plot`
Has several methods for plotting benchmarks.
Should be initialized by specifying the boilerplate types created on the second layer.

Example for plotting sorting algorithms: `AlgorithmPlot<int[], ArrayBenchmarks, ArrayTypes, SortingBenchmarks>`. The returned class will be able to perform graphing methods with algorithms.

`using` usage recommended for shorter type syntax. Example: `using SortingPlot = AlgorithmPlot<int[], ArrayBenchmarks, ArrayTypes, SortingBenchmarks>;`.