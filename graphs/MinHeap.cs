public class MinHeap<T> : PriorityQueue<T> where T : IComparable<T>{
    private T[] minHeap;
    private int size;
    private const int DEFAULT_SIZE = 10;


    public MinHeap(int initialSize){
        if(initialSize > 1_000_000){
            throw new ArgumentException("The initial size is too large. Input an initial size <= 1,000,000");
        }
        this.minHeap = new T[initialSize];
        this.size = 0;
    }

    public MinHeap(){
        this.minHeap = new T[DEFAULT_SIZE];
        this.size = 0;
    }

    public MinHeap(T[] ar){
        this.minHeap = ar;
        this.size = ar.Length;
    }

    public void clear(){
        this.minHeap = new T[DEFAULT_SIZE];
        this.size = 0;
    }

    public int getSize(){
        return this.size;
    }

    public T getMin(){
        return this.minHeap[0];
    }

    private void expand(){
        if(this.size >= minHeap.Length){
            T[] temp = this.minHeap;
            int newLength = (int)(2*minHeap.Length);
            this.minHeap = new T[newLength];
            for(int i = 0; i < this.size; i++){
                this.minHeap[i] = temp[i];
            }
        }
    }

    private void shrink(){
        if(this.size <= (int)((1.0/3)*minHeap.Length)){
            T[] temp = this.minHeap;
            int newLength = (int)(1.0/2.0 * this.minHeap.Length);
            this.minHeap = new T[newLength];
            for(int i = 0; i < this.size; i++){
                this.minHeap[i] = temp[i];
            }
        }
    }

    private void swap(int index1, int index2){
        T temp = this.minHeap[index1];
        this.minHeap[index1] = this.minHeap[index2];
        this.minHeap[index2] = temp;
    }

    private void rehepificationUp(){
        int lastIndex = this.size;
        int parent = (lastIndex-1)/2;
        while(parent >= 0 && (this.minHeap[lastIndex].CompareTo(this.minHeap[parent]) < 0)){
            swap(parent,lastIndex);
            lastIndex = parent;
            parent = (lastIndex-1)/2;
        }
        
    }

    private void rehepificationDown(){
        int lastIndex = this.size-1;
        int parent = 0;
        int leftChild = 2*parent + 1;
        int rightChild = 2*parent + 2;
        int minChild;
        if(rightChild <= lastIndex && this.minHeap[rightChild].CompareTo(this.minHeap[leftChild]) < 0){
                minChild = rightChild;
        }
        else{
            minChild = leftChild;
        }
        while(leftChild <= lastIndex && this.minHeap[minChild].CompareTo(this.minHeap[parent]) < 0){
            swap(minChild,parent);
            parent = minChild;
            leftChild = 2*parent+1;
            rightChild = 2*parent+2;
            if(rightChild <= lastIndex && this.minHeap[rightChild].CompareTo(this.minHeap[leftChild]) < 0){
                minChild = rightChild;
            }
            else{
                minChild = leftChild;
            }
        }
        

    }
    
    public void insert(T value){
        expand();
        int lastIndex = this.size;
        this.minHeap[lastIndex] = value;
        rehepificationUp();
        this.size++;
    }

    public T removeMin(){
        if(this.size <= 0){
            throw new InvalidOperationException("The Heap is empty");
        }
        T removedElement = default(T);
            int lastIndex = this.size-1;
            removedElement = this.minHeap[0];
            this.minHeap[0] = this.minHeap[lastIndex];
            size--;
            rehepificationDown();
        return removedElement;
    }

    public bool isEmpty(){
        return this.size <= 0;
    }

    public void display(){
        while(!this.isEmpty()){
            Console.Write($"{this.removeMin()} ");
        }
    }

}
namespace Driver{
using System.Diagnostics;
public class MinHeapDriver{

    private static void swap(int[] ar, int index1, int index2){
        int temp = ar[index1];
        ar[index1] = ar[index2];
        ar[index2] = temp;
    }

    public static void heapify(int[] ar, int root){
        int lastIndex = ar.Length-1;
        int parent = root;
        int leftChild = 2*parent + 1;
        int rightChild = 2*parent + 2;
        int minChild;
        bool done = false;
        while(leftChild <= lastIndex && !done){
            minChild = leftChild;
            if(rightChild <= lastIndex && ar[rightChild].CompareTo(ar[leftChild]) < 0){
                minChild = rightChild;
            }
            if(ar[minChild].CompareTo(ar[parent]) < 0){
                swap(ar,minChild,parent);
                parent = minChild;
                leftChild = 2*parent+1;
                rightChild = 2*parent+2;
            }
            else{
                done = true;
            }
        }
    }
    public static void Main(){
        Stopwatch st = new Stopwatch();

        st.Start();
        int[] ar = new int[10];
        for(int i = 10; i > 0; i--){
            ar[10-i] = i;
        }
        for(int i = ar.Length-1; i > -1; i--){
            heapify(ar,i);
        }

        PriorityQueue<int> pq = new MinHeap<int>(ar);
        pq.display();
        st.Stop();
        Console.WriteLine($"Time elapsed: {st.ElapsedMilliseconds} ms");
        

    }
}
}