using System;
using System.Collections.Generic;
using System.Text;

namespace WeightedGraphs
{
    class PriorityHeap<T> where T : IComparable<T>
    {
        T[] array;
        int Count;
        public PriorityHeap(int capacity = 10)
        {
            array = new T[capacity];
            Count = 0;
        }
        int FindParentIndex(int childIndex)
        {
            int parentNodeIndex = (childIndex - 1) / 2;
            return parentNodeIndex;
        }
        int FindLeftChild(int parentIndex)
        {
            int leftChildIndex = (2 * parentIndex) + 1;
            return leftChildIndex;
        }
        int FindRightChild(int parentIndex)
        {
            int RightChildIndex = (2 * parentIndex) + 2;
            return RightChildIndex;
        }
        public void Insert(T value)
        {
            array[Count] = value;
            HeapifyUp();
            if (Count > array.Length)
            {
                Resize();
            }
            Count++;
        }
        private void HeapifyUp()
        {
            if (Count == 0) return;
            int current = Count;
            int compareValueIndex = FindParentIndex(current);
            while (array[compareValueIndex].CompareTo(array[current]) > 0 && compareValueIndex >= 0)
            {
                T temp = array[current];
                array[current] = array[compareValueIndex];
                array[compareValueIndex] = temp;
                current = compareValueIndex;
                compareValueIndex = FindParentIndex(current);
            }
        }
        public T Pop()
        {
            T temp = array[0];
            array[0] = array[Count];
            HeapifyDown();
            return temp;
        }
        private void HeapifyDown()
        {
            int currentIndex = 0;
            int RightChildIndex = FindRightChild(currentIndex);
            int LeftChildIndex = FindLeftChild(currentIndex);
            while (currentIndex < array.Length)
            {
                if (LeftChildIndex > array.Length)
                {
                    break;
                }
                else if (RightChildIndex > array.Length)
                {
                    if (array[currentIndex].CompareTo(array[LeftChildIndex]) > 0)
                    {
                        Swap(LeftChildIndex, currentIndex);
                        currentIndex = LeftChildIndex;
                    }
                }
                else if (array[LeftChildIndex].CompareTo(array[RightChildIndex]) < 0)
                {
                    Swap(LeftChildIndex, currentIndex);
                    currentIndex = LeftChildIndex;
                }
                else
                {
                    Swap(RightChildIndex, currentIndex);
                    currentIndex = RightChildIndex;
                }
                RightChildIndex = FindRightChild(currentIndex);
                LeftChildIndex = FindLeftChild(currentIndex);
            }
        }
        private void Swap(int childIndex, int currentIndex)
        {
            T temp = array[currentIndex];
            array[currentIndex] = array[childIndex];
            array[childIndex] = temp;
        }
        private void Resize()
        {
            T[] resizeArray = new T[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                resizeArray[i] = array[i];
            }
            array = resizeArray;
        }
    }
}
