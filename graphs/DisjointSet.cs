public class DisjointSet{
    int[] disjointSet;
    string[] names;

    public DisjointSet(string[] ar){
        this.disjointSet = new int[ar.Length];
        this.names = new string[ar.Length];
        for(int i = 0; i < ar.Length;i++){
            this.names[i] = ar[i];
            this.disjointSet[i] = i;
        }
    }

    public bool find(int set1, int set2){
        return disjointSet[set1] == disjointSet[set2];
    }

    public void union(int set1, int set2){
        for(int i = 0; i < disjointSet.Length; i++){
            if(disjointSet[i] == disjointSet[set2]){
                disjointSet[i] = disjointSet[set1];
            }
        }
    }

    public void displaySets(){
        for(int i = 0; i < disjointSet.Length; i++){
            for(int j = 0; j < disjointSet.Length; j++){
                if(disjointSet[j] == disjointSet[i]){
                    Console.WriteLine($"Set {i}: {names[j]}");
                }
            }
        }
    }

    public static void Main(){
        string[] names = {"A", "B", "C", "D", "E"};
        DisjointSet ds = new DisjointSet(names);
        ds.displaySets();

        ds.union(0,1);
        ds.union(2,1);
        ds.find(0,2);
        ds.displaySets();

    }
}