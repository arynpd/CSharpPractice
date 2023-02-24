public class Graph : GraphInterface{
    private class Node{
        public int vertex{get;set;}
        public Node? next{get;set;}

        public Node(int vertex){
            this.vertex = vertex;
            this.next = null;
        }
}
    bool[] visited;
    Node?[] adjacencyList;

    public Graph(int numVertices){
        adjacencyList = new Node[numVertices];
        visited = new bool[numVertices];
        for(int i = 0; i < numVertices; i++){
            adjacencyList[i] = null; 
            visited[i] = false;
        }
    }

    public bool containsEdge(int source, int target){
        if(source < 0 || target < 0){
            return false;
        }
        Node? current = adjacencyList[source];

        if(current == null){
            Console.WriteLine(current);
            return false;
        }

        while(current != null){
            if(current.vertex == target){
                return true;
            }
            current = current.next;
        }
        return false;
    }

    public void addEdge(int source, int target){
        if(source < 0 || target < 0){
            return;
        }

        if(this.containsEdge(source, target)){
            return;
        }

        //undirected graph
        addEdgeHelper(source,target);
        addEdgeHelper(target,source);

    }

    private void addEdgeHelper(int source, int target){
        Node? currentVertex = this.adjacencyList[source];
        if(currentVertex == null){
            currentVertex = new Node(target);
        }
        else{
            while(currentVertex.next != null){
                currentVertex = currentVertex.next;
            }
            currentVertex.next = new Node(target);
        }
    }

    public void removeEdge(int source, int target){
        if(source < 0 || target < 0){
            return;
        }
        if(!containsEdge(source,target)){
            return;
        }

        removeEdgeHelper(source, target);
        removeEdgeHelper(source,target);
    }

    private void removeEdgeHelper(int source, int target){
        Node? currentVertex = this.adjacencyList[source];
        if(currentVertex == null){
            return;
        }

        if(currentVertex.next == null){
            currentVertex = null;
        }
        else{
            while(currentVertex.next != null){
                if(currentVertex.next.vertex == target)
                    break;
                currentVertex = currentVertex.next;
            }
            currentVertex.next = currentVertex.next.next;
        }
    }

    public void displayGraph(){
        int j = 0;
        Node? current;
        foreach(Node? i in adjacencyList){
            Console.Write($"Vertex {j}:");
            current = i;
            while(current != null){
                Console.Write($"{current.vertex}");
                current = current.next;
            }
            j++;
            Console.WriteLine();
        }
    }

    public static void Main(){
        Graph g = new Graph(5);
        g.addEdge(0,1);
        Console.WriteLine(g.containsEdge(0,1));
       // g.displayGraph();
    }


}
