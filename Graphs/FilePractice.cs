public class FilePractice{
    public static void Main(){
        StreamWriter fs = new StreamWriter(File.Open("./graphinfo.txt", FileMode.Create));
        Graph g = new Graph(5);
        g.addEdge(0,1);
        g.addEdge(1,2);
        //g.addEdge(2,3);
        //g.addEdge(0,3);
        g.addEdge(2,3);
        g.addEdge(3,1);
        g.addEdge(1,4);
        g.addEdge(3,4);
        g.displayGraph(fs);
        fs.WriteLine(g.hasCycle(0));
        g.bfs(0,fs);
        fs.Close();
    }
}