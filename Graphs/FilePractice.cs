public class FilePractice{
    public static void Main(){
        StreamWriter fs = new StreamWriter(File.Open("./graphinfo.txt", FileMode.Create));
        Graph g = new Graph(6);
        g.addEdge(0,2);
        g.addEdge(0,3);
        //g.addEdge(2,3);
        //g.addEdge(0,3);
        g.addEdge(0,4);
        g.addEdge(1,2);
        g.addEdge(1,3);
        g.addEdge(1,4);
        g.addEdge(1,5);
        //g.addEdge(5,4);
        //g.addEdge(0,1);
        g.displayGraph(fs);
        //fs.WriteLine(g.hasCycle(0));
        g.bfsAll(fs);
        fs.WriteLine(g.checkBipartite());
        fs.Close();
    }
}