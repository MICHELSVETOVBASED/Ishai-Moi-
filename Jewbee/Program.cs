using System.Net;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

class Program{
    static void Main(string[] args){
        Process();
    }
    public static void Process()
    {
        try{
            NoobCoders.ProcessData();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (InvalidOperationException e) when (e.Message.Contains("This error should not be re-thrown"))
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            ExceptionDispatchInfo.Capture(e).Throw();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ExceptionDispatchInfo.Capture(e).Throw();
        }
    }
}

class NoobCoders{
    public static int ProcessData(){
        Queue<int> numbers = new Queue<int>();
        return numbers.Dequeue();
    }
}
