public class NetflowGraph : GraphInterface{
    internal class Node{
        public int vertex{get;set;}
        public Node? next{get;set;}
        public int weight;

        public Node(int vertex,int weight){
            this.vertex = vertex;
            this.weight = weight;
            this.next = null;
        }
}
    bool[] visited;
    Node?[] capacity;
    Node?[] flow;
    Node?[] residual;
    int source,sink;
    int[] parentList;
    int maxFlow;

    public NetflowGraph(int numVertices,int source, int sink){
        this.source = source;
        this.sink = sink;
        capacity = new Node[numVertices];
        flow = new Node[numVertices];
        residual = new Node[numVertices];
        visited = new bool[numVertices];
        parentList = new int[numVertices];
        maxFlow = 0;
        for(int i = 0; i < numVertices; i++){
            capacity[i] = null;
            flow[i] = null;
            residual[i] = null;
            visited[i] = false;
            parentList[i] = -1;
        }
    }

    public bool containsEdge(int start, int target){
        if(start < 0 || target < 0){
            return false;
        }
        Node? current = capacity[start];

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

    public void addEdge(int start, int target,int weight){
        if(start < 0 || target < 0){
            return;
        }

        if(this.containsEdge(start, target)){
            return;
        }

        //directed graph
        addEdgeHelper(capacity,start,target,weight);
        addEdgeHelper(flow,start,target,0);
        addEdgeHelper(residual,start,target,weight);
        addEdgeHelper(residual,target,start,0);

    }

    private void addEdgeHelper(Node?[] ar, int start, int target,int weight){
        Node? currentVertex = ar[start];
        if(currentVertex == null){
            ar[start] = new Node(target,weight);
        }
        else{
            ar[start] = new Node(target,weight);
            ar[start].next = currentVertex;
        }
    }

    public void removeEdge(int start, int target){
        if(start < 0 || target < 0){
            return;
        }
        if(!containsEdge(start,target)){
            return;
        }

        removeEdgeHelper(capacity,start, target);
        removeEdgeHelper(flow,start,target);
        removeEdgeHelper(residual,start,target);
        removeEdgeHelper(residual,target,start);
    }

    private void removeEdgeHelper(Node?[] ar, int start, int target){
        Node? currentVertex = ar[start];
        if(currentVertex == null){
            return;
        }

        if(currentVertex.next == null){
           ar[start] = null;
        }
        else{
            while(currentVertex.next.vertex != target){
                currentVertex = currentVertex.next;
            }
            currentVertex.next = currentVertex.next.next;
        }
    }

    private void displayGraph(StreamWriter fs, Node?[] ar){
        int j = 0;
        Node? current;
        foreach(Node? i in ar){
            fs.Write($"Vertex {j}:");
            current = i;
            while(current != null){
                if(current.weight > 0){
                    fs.Write($"{current.vertex}({current.weight})");
                }
                current = current.next;
            }
            j++;
            fs.WriteLine();
            fs.Flush();
        }
    }

    public void displayGraph(StreamWriter fs){
        fs.WriteLine("Capacity Graph:");
        displayGraph(fs,capacity);
        fs.WriteLine("\nFlow Graph:");
        displayGraph(fs,flow);
        fs.WriteLine("\nResidual Graph");
        displayGraph(fs,residual);
    }

    private void resetVisited(){
        for(int i = 0; i < this.visited.Length; i++){
            this.visited[i] = false;
        }
    }

    private void resetParent(){
        for(int i = 0; i < this.parentList.Length; i++){
            this.parentList[i] = -1;
        }
    }

    public void dfs(int start/*,StreamWriter fs*/){
        visited[start] = true;
        //fs.WriteLine(start);
        Node? walker = this.residual[start];
        while(walker != null){
            if(!visited[walker.vertex]){
                parentList[walker.vertex] = start;
                dfs(walker.vertex/*,fs*/);
            }
            walker = walker.next;
        }
    }

    public void bfs(int start/*,StreamWriter? fs*/){
        Queue<int> layers = new Queue<int>();
        layers.Enqueue(start);
        this.visited[start] = true;
        while(layers.Count != 0 && parentList[sink] < 0 ){
            int currentVertex = layers.Dequeue();
            //fs.WriteLine(currentVertex);
            Node? currentNeighbor = this.residual[currentVertex];
            while(currentNeighbor != null){
                if(!visited[currentNeighbor.vertex] && currentNeighbor.weight > 0){
                    visited[currentNeighbor.vertex] = true;
                    parentList[currentNeighbor.vertex] = currentVertex;
                    layers.Enqueue(currentNeighbor.vertex);
                }
                currentNeighbor = currentNeighbor.next;
            }
        }
    }

    private bool getPath(){
        this.resetVisited();
        this.resetParent();
        this.bfs(source/*,null*/);
        return this.parentList[sink] != -1;
    }

    private int getBottleneck(){
        int min = int.MaxValue;
        int index = sink;
        while(parentList[index] > -1){
            Node? walker = this.residual[parentList[index]];
            while(walker != null){
                if(walker.vertex == index){
                    break;
                }
                walker = walker.next;
            }
            min = Math.Min(walker.weight, min);
            index = parentList[index];
        }
        return min;
    }

    private Node? getEdge(int start, int end, Node?[] ar){
        Node? walker = ar[start];
        while(walker != null){
            if(walker.vertex == end){
                break;
            }
            walker = walker.next;
        }
        return walker;
    }

    public void fordFulkerson(StreamWriter fs){
        while(getPath()){
            int index = sink;
            int bottleneck = getBottleneck();

            while(parentList[index] > -1){
                Node? residualEdge = getEdge(parentList[index], index, residual);
                Node? oppositeResidualEdge = getEdge(index, parentList[index], residual);
                Node? flowEdge;

                if(this.containsEdge(parentList[index], index)){
                    flowEdge = getEdge(parentList[index], index, flow);
                    flowEdge.weight += bottleneck;
                }
                else{
                    flowEdge = getEdge(index, parentList[index], flow);
                    flowEdge.weight -= bottleneck;
                }
                residualEdge.weight -= bottleneck;
                oppositeResidualEdge.weight += bottleneck;
                index = parentList[index];
            }
            fs.WriteLine("--------------------------------------------");
            fs.WriteLine(bottleneck);
            displayGraph(fs);
            maxFlow += bottleneck;
        }
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