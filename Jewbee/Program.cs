using System.Net;

class Program{
    static void Main(string[] args){
        Process();
    }
    public static void Process()
    {
        try{
            NoobCoders.ProcessData();
        }
        catch (InvalidOperationException e){
            Console.WriteLine(e);
        }
    }
}

class NoobCoders{
    public static int ProcessData(){
        Queue<int> numbers = new Queue<int>();
        return numbers.Dequeue();

    }
}
