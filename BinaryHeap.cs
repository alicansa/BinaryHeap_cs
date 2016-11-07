// Binary heap implementation using arrays
// 
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace Dijkstra.Utils{
    public enum BinaryHeapType {MIN,MAX};
    public class BinaryHeap<T> : IEnumerable<T>, ICollection<T> where T : IComparable
    {
      
        public List<T> Nodes {get;private set;} //nodes of the binary heap
        public BinaryHeapType Type {get;set;}
        public BinaryHeap(){
            Type = BinaryHeapType.MIN;
            Nodes = new List<T>();
        }
        public BinaryHeap(BinaryHeapType type){
            Type = type;
            Nodes = new List<T>();
        }
        public BinaryHeap(T[] nodes){
            Type = BinaryHeapType.MIN;
            Nodes = new List<T>();
            foreach (T node in nodes){
                this.Add(node);
            }
        }
        public BinaryHeap(T[] nodes, BinaryHeapType type){
            Type = type;
            Nodes = new List<T>();
            foreach (T node in nodes){
                this.Add(node);
            }
        }
        public int Count
        {
            get
            {
                return this.Nodes.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)Nodes.GetEnumerator();
        }
        public void Add(T item)
        {
            //add the element to the bottom of the heap
            this.Nodes.Add(item);
            //heapify up
            this.HeapifyUp(this.Count-1);
        }
        public void Clear()
        {
            Nodes.Clear();
        }
        public bool Contains(T item)
        {

            return Nodes.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            Nodes.CopyTo(array,arrayIndex);
        }
        public bool Remove(T item)
        {
            int itemIndex = Nodes.IndexOf(item);
            if (itemIndex >= 0){
                //swap the element with the farthest right element at the lowest level
                Swap(itemIndex,this.Count-1);
                Nodes.Remove(item);
                if (itemIndex == 0){ //if root just heapifydown
                    this.HeapifyDown(itemIndex);
                }else{
                    switch (this.Type){
                        case BinaryHeapType.MIN:
                            if (this[itemIndex].CompareTo(this[(itemIndex-1)/2]) < 0) //itemIndex doesn't change -> compare it to parent and choose heapify type
                                HeapifyUp(itemIndex);
                            else
                                HeapifyDown(itemIndex);
                            break;
                        case BinaryHeapType.MAX:
                            if (this[itemIndex].CompareTo(this[(itemIndex-1)/2]) < 0) //itemIndex doesn't change -> compare it to parent and choose heapify type
                                HeapifyDown(itemIndex);
                            else
                                HeapifyUp(itemIndex);
                            break;
                    }
                    
                }
                return true;
            }else return false;
  
        }

        public T this[int i] 
        {
            get
            {
                return Nodes[i];
            }
            set
            {
                Nodes[i] = value;
            }
        }
        private void HeapifyUp(int childIndex){
            //compare the added element with its parent; if they are in correct order stop
            //if not swap the element with its parent and return to the previous step
            int parentIndex = (childIndex-1)/2; //floored because its integer
            T child; // the added item
            T parent;
            bool done=false;
            while(childIndex > 0 && !done){
                child = this[childIndex]; // the added item
                parent = this[parentIndex];

                switch (this.Type){
                    case BinaryHeapType.MAX:
                        if (parent.CompareTo(child) < 0) Swap(childIndex,parentIndex);
                        else {done = true;}
                        break;
                    case BinaryHeapType.MIN:
                        if (parent.CompareTo(child) > 0) Swap(childIndex,parentIndex);
                        else {done = true;}
                        break;
                }

                childIndex = parentIndex;
                parentIndex = (childIndex-1)/2;
            }
        }
        private void HeapifyDown(int parentIndex){
            //compare the new root with its children; if they are in the correct order stop
            //If not, swap the element with one of its children and return to the previous step.

            int rChildIndex = 2*parentIndex+2;
            int lChildIndex = 2*parentIndex+1;

            T rChild;
            T lChild;
            T parent;
            bool done=false;
            while(parentIndex < this.Count && !done){
                parent = this[parentIndex];

                switch (this.Type){
                    case BinaryHeapType.MAX:
                         if (rChildIndex < this.Count){
                            rChild = this[rChildIndex];
                            lChild = this[lChildIndex];
                            //compare left and right child
                            if (rChild.CompareTo(lChild) < 0){
                                if (parent.CompareTo(lChild) < 0) {
                                    Swap(lChildIndex,parentIndex);
                                    parentIndex = lChildIndex;
                                }
                                
                            }else{
                                if (parent.CompareTo(rChild) < 0) {
                                    Swap(rChildIndex,parentIndex);
                                    parentIndex = rChildIndex;
                                }
                            }
                        }
                        else if (lChildIndex < this.Count){
                            lChild = this[lChildIndex];
                            if (parent.CompareTo(lChild) < 0) {
                                Swap(lChildIndex,parentIndex);
                                parentIndex = rChildIndex;
                                break;
                            }
                        }
                        done = true;
                        break;

                    case BinaryHeapType.MIN:
                        if (rChildIndex < this.Count){
                            rChild = this[rChildIndex];
                            lChild = this[lChildIndex];
                            //compare left and right child
                            if (rChild.CompareTo(lChild) > 0){
                                if (parent.CompareTo(lChild) > 0) {
                                    Swap(lChildIndex,parentIndex);
                                    parentIndex = lChildIndex;
                                    break;
                                }
                                
                            }else{
                                if (parent.CompareTo(rChild) > 0) {
                                    Swap(rChildIndex,parentIndex);
                                    parentIndex = rChildIndex;
                                    break;
                                }
                            }
                        }
                        else if (lChildIndex < this.Count){
                            lChild = this[lChildIndex];
                            if (parent.CompareTo(lChild) > 0) {
                                Swap(lChildIndex,parentIndex);
                                parentIndex = rChildIndex;
                                break;
                            }
                        }
                        done = true;
                        break;
                }

                rChildIndex = 2*parentIndex+2;
                lChildIndex = 2*parentIndex+1;
            }
        }

        private void Swap(int index1,int index2){
            T temp = this[index2];
            this[index2] = this[index1];
            this[index1] = temp;
        }
    }
}
