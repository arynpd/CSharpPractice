public interface GraphInterface{
    public void addEdge(int source, int target,int weight);
    public void removeEdge(int source, int target);
    public bool containsEdge(int source, int target);
}