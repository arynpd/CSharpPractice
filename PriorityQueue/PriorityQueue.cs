public interface PriorityQueue<T> where T : IComparable<T>{
    public void insert(T value);
    public T removeMin();
    public T getMin();
    public int getSize();
    public void clear();
    public void display();
    public bool isEmpty();

}