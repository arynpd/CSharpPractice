using System.Collections.Generic;
public class Graph : GraphInterface{
    internal class Node{
        public int vertex{get;set;}
        public Node? next{get;set;}

        public Node(int vertex){
            this.vertex = vertex;
            this.next = null;
        }
}
    bool[] visited;
    Node?[] adjacencyList;
    int[] parentList;
    bool[] color;

    public Graph(int numVertices){
        adjacencyList = new Node[numVertices];
        visited = new bool[numVertices];
        parentList = new int[numVertices];
        color = new bool[numVertices];
        for(int i = 0; i < numVertices; i++){
            adjacencyList[i] = null; 
            visited[i] = false;
            parentList[i] = -1;
            color[i] = false;
        }
    }

    public bool containsEdge(int source, int target){
        if(source < 0 || target < 0){
            return false;
        }
        Node? current = adjacencyList[source];

        if(current == null){
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

    public void addEdge(int source, int target, int weight=1){
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
            this.adjacencyList[source] = new Node(target);
        }
        else{
            this.adjacencyList[source] = new Node(target);
            this.adjacencyList[source].next = currentVertex;
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
        removeEdgeHelper(target,source);
    }

    private void removeEdgeHelper(int source, int target){
        Node? currentVertex = this.adjacencyList[source];
        if(currentVertex == null){
            return;
        }

        if(currentVertex.next == null){
            this.adjacencyList[source] = null;
        }
        else{
            while(currentVertex.next.vertex != target){
                currentVertex = currentVertex.next;
            }
            currentVertex.next = currentVertex.next.next;
        }
    }

    public void displayGraph(StreamWriter fs){
        int j = 0;
        Node? current;
        foreach(Node? i in adjacencyList){
            fs.Write($"Vertex {j}:");
            current = i;
            while(current != null){
                fs.Write($"{current.vertex}");
                current = current.next;
            }
            j++;
            fs.WriteLine();
        }
    }

    private void resetVisited(){
        for(int i = 0; i < this.visited.Length; i++){
            this.visited[i] = false;
        }
    }

    private bool _dfs(int source, bool hasCycle){
        visited[source] = true;
        Node? walker = this.adjacencyList[source];
        while(walker != null && !hasCycle){
            if(!visited[walker.vertex]){
                parentList[walker.vertex] = source;
                hasCycle = _dfs(walker.vertex, hasCycle);
            }
            else if(parentList[source] != walker.vertex && visited[walker.vertex]){
                hasCycle = true;
            }
            walker = walker.next;
        }
        return hasCycle;
    }

    public bool hasCycle(int source){
        if(this.adjacencyList[source] == null){
            return false;
        }
        else{
            this.resetVisited();
            return this._dfs(source, false);
        }
    }

    public void bfs(int source, StreamWriter fs){
        Queue<int> layers = new Queue<int>();
        int layerCounter = 0;
        layers.Enqueue(source);
        this.visited[source] = true;
        this.color[source] = true;
        while(layers.Count != 0){
            int currentVertex = layers.Dequeue();
            fs.WriteLine(currentVertex);
            layerCounter++;
            Node? currentNeighbor = this.adjacencyList[currentVertex];
            while(currentNeighbor != null){
                if(!visited[currentNeighbor.vertex]){
                    visited[currentNeighbor.vertex] = true;
                    if(layerCounter%2==0)
                        color[currentNeighbor.vertex] = true;
                    else
                        color[currentNeighbor.vertex] = false; 
                    layers.Enqueue(currentNeighbor.vertex);
                }
                currentNeighbor = currentNeighbor.next;
            }
        }
    }

    public void bfsAll(StreamWriter fs){
        this.resetVisited();
        for(int i = 0; i < this.visited.Length; i++){
            if(this.visited[i] == false){
                fs.WriteLine($"New source: {i}");
                bfs(i,fs);
            }
        }
    }

    public bool checkBipartite(){
        bool currentVertexColor = false;
        Node? current;
        for(int i = 0; i < this.adjacencyList.Length; i++){
            currentVertexColor = this.color[i];
            current = this.adjacencyList[i];
            while(current!=null){
                if(this.color[current.vertex] == currentVertexColor){
                    return false;
                }
                current = current.next;
            }
        }
        return true;
    }
    /*public static void Main(){
        Graph g = new Graph(5);
        g.addEdge(0,1);
        g.addEdge(1,2);
        //g.addEdge(2,3);
        //g.addEdge(0,3);
        g.addEdge(2,3);
        g.addEdge(3,1);
        g.addEdge(1,4);
        g.addEdge(3,4);
        g.displayGraph();
        Console.WriteLine(g.hasCycle(0));
    }*/


}
