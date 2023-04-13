using System.Collections.Generic;
public class DisjointSet{
    int[] disjointSet;
    Dictionary<string,int> names;
    string[] keys;

    public DisjointSet(string[] ar){
        this.disjointSet = new int[ar.Length];
        this.names = new Dictionary<string,int>();
        this.keys = new string[ar.Length];
        for(int i = 0; i < ar.Length;i++){
            this.names.Add(ar[i], i);
            this.keys[i] = ar[i];
            this.disjointSet[i] = i;
        }
    }

    public string find(string set1){
        int set1Index = this.names[set1];
        int parentSetIndex = this.disjointSet[set1Index];
        return this.keys[parentSetIndex]; 
    }

    public void union(string set1, string set2){
        int set1Index = this.names[set1];
        int set2Index = this.names[set2];
        for(int i = 0; i < disjointSet.Length; i++){
            if(disjointSet[i] == disjointSet[set1Index]){
                disjointSet[i] = disjointSet[set2Index];
            }
        }
    }

    public void displaySets(){
        for(int i = 0; i < disjointSet.Length; i++){
            Console.Write($"Set {i}: ");
            for(int j = 0; j < disjointSet.Length; j++){
                if(disjointSet[j] == i){
                    Console.Write($"{keys[j]}");
                }
            }
            Console.WriteLine();
        }
    }

    public static void Main(){
        string[] names = {"A", "B", "C", "D", "E"};
        DisjointSet ds = new DisjointSet(names);
        ds.displaySets();
        Console.WriteLine();
        ds.union("A","B");
        ds.union("C","B");
        Console.WriteLine(ds.find("C"));
        ds.displaySets();

    }
}