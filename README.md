# BinaryHeap_cs

Implementation of min-max binary heap using C#. Arrays are used to represent the tree structure where parent_i has children at indices 2i and 2i+1. In order to minimize the cost of memory re-allocation for fixed sized arrays, the List<T> class is used for it's optimized array resizing properties. 

When initializing a binary heap object the default type is min-binary heap. Otherwise it has to be stated using BinaryHeapType.MAX. Furthermore the leaves of the tree can be of any type with one constaint that they are comparable between each other (so has to implement the IComparable interface).

