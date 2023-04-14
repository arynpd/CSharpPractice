public class FilePractice{
    public static void Main(){
        StreamWriter fs = new StreamWriter(File.Open("./graphinfo.txt", FileMode.Create));
        //StreamWriter fs = new StreamWriter(Console.OpenStandardOutput());
        NetflowGraph g = new NetflowGraph(8,0,7);
        g.addEdge(0,1,10);
        g.addEdge(0,2,5);
        g.addEdge(0,3,15);
        g.addEdge(1,2,4);
        g.addEdge(1,4,9);
        g.addEdge(1,5,15);
        g.addEdge(2,3,4);
        g.addEdge(2,5,8);
        g.addEdge(3,6,30);
        g.addEdge(4,5,15);
        g.addEdge(4,7,10);
        g.addEdge(5,7,10);
        g.addEdge(5,6,15);
        g.addEdge(6,2,6);
        g.addEdge(6,7,10);
        
        /*g.addEdge(0,1,1);
        g.addEdge(0,2,1);
        g.addEdge(3,2,1);
        g.addEdge(4,1,1);
        g.addEdge(4,5,1);
        g.addEdge(4,6,1);
        g.addEdge(7,2,1);
        g.addEdge(7,8,1);
        g.addEdge(9,8,1);
        g.addEdge(10,0,1);
        g.addEdge(10,3,1);
        g.addEdge(10,4,1);
        g.addEdge(10,7,1);
        g.addEdge(10,9,1);
        g.addEdge(1,11,1);
        g.addEdge(2,11,1);
        g.addEdge(5,11,1);
        g.addEdge(6,11,1);
        g.addEdge(8,11,1);*/

        
        g.displayGraph(fs);
        g.fordFulkerson(fs);
        fs.WriteLine("\n-------------------------------After FordFulkerson--------------------------\n");
        g.displayGraph(fs);
        fs.Close();
    }
}