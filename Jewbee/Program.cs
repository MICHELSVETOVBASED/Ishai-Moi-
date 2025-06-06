using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Runtime;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace Jewbee;

class Program{
    public static void Main(){
        Console.WriteLine(Solution.RobotWithString("bddax`")); //bdevfziy
    }



    public class Solution {
        public static string RobotWithString(string s){
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            var consonants = new HashSet<char> { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm',
                'n', 'p', 'q', 'r', 's', 't', 'v', 'x' };
            var ss = s.ToList();
            var t = new Stack<char>();
            var p = "";
            
            var i = 0;
            for (; i < ss.Count; i++){
                t.Push(ss[i]);
                if (ss.Count > 1  && t.Peek() > ss.Skip(i + 1).DefaultIfEmpty(char.MaxValue).Min()){
                    ss.RemoveAt(i--);
                }
                else{
                    if (t.Peek() <= ss.Skip(i+1).DefaultIfEmpty(char.MaxValue).Min()){
                        p += t.Pop();
                        ss.RemoveAt(i--);
                        for (var l = 0; l < t.Count; l++){
                            if (t.Peek() <= ss.DefaultIfEmpty(char.MaxValue).Min())
                                p += t.Pop();
                        }
                        continue;
                    }
                    if (ss.Count != 1) continue;
                    ss.RemoveAt(i);
                    return p += RevertPass(new string(t.Reverse().ToArray()));
                    
                }
            
                
            }
            if(t.Count != 0)
                return p += RevertPass(new string(t.Reverse().ToArray()));
            return p;
        }

        private static string RevertPass(string t){
            var fluxedT = "";
            for (var i = t.Length-1; i >= 0; i--){
                fluxedT += t[i];
            }

            return fluxedT;
        }
        public static string ClearDigits(string s){
            for (var i = s.Length - 1; i >= 0; i--){
                if (char.IsDigit(s[i])){
                    for (var j = i; j >= 0; j--){
                        if (!char.IsDigit(s[j])){
                            s = s.Remove(j, 1);
                            if (i - 1 >= 0)
                                i--;
                            break;
                        }
                    }

                    if (char.IsDigit(s[0]) && s.Length == 1)
                        s = "";
                    if(s!="")
                        s = s.Remove(i, 1);
                }

            }
            return s;
        }
        public static int NumberOfSteps(int num){
            var count = 0;
            if (num == 0) return 0;
            while (num != 0){
                if (num % 2 == 0)
                    num /= 2;
                else
                    num--;
                count++;
            }
            return count;
        }
        public static string LongestCommonPrefix(string[] strs){
            var dict = new List<IDictionary<List<char>, int>>(); 
            List<IList<char>> result = new List<IList<char>>();
            for (var i = 0 ; i < strs.Length; i++){
                var list = new List<char>();          
                for (var j = 0; j < strs.Min(inn => inn.Length); j++){//to the littlest word
                    var firstMatch = strs[i][j];
                    for (var ii = 0; ii < strs.Length-1; ii++){
                        if (firstMatch != strs[ii+1][j] || i==ii+1 ){
                            continue;
                        }
                        list.Add(firstMatch);
                        break;
                    }
                }
                if (list.Count != 0){
                    result.Add(list);
                }

            }

            return string.Join("", result.MaxBy(list => list.Count) ?? new List<char>());
        }

        static char StartPassWords(string[] strs,char c){
            for (var i = 0; i < strs.Length; i++){
                
            }

            return 'o';
        }
        public static IList<IList<int>> Generate(int numRows){
            if (numRows <1 || numRows >30)
                throw new Exception("more than 1 less than 30");
            
            var resultList = new List<IList<int>>(numRows);
            resultList.Add([1]);
            
            for (var i = 2; i <= numRows && numRows != 1; i++){
                var list = new List<int>(new int[i]);
                var previousList = resultList[i-2];
                HalfFilling(ref list, previousList);
                resultList.Add(list);
            }
            Console.WriteLine(String.Join(", ", resultList[numRows-1].ToArray()));
            return resultList;
        }

        private static void HalfFilling(ref List<int> list, IList<int>previousList){
            list[0] = 1;
            list[^1] = 1;
            for (var i = 1; i <= list.Count-2; i++){
                list[i] = previousList[i - 1] + previousList[i];
            }
        }
    }
       
    public static void PrintNumber(){
        string? line = Console.ReadLine(); // ввод числа в десятеричной системе 
        int x = int.Parse(line);

        var result = new List<object>();
        
        if (x > 16){
            var division = x % 16;
            x /= 16;
            result.Add(division);
            
            for (;;){
                
                division = x%16;
                x /= 16;
                result.Add(division);
                if (x<16){
                    result.Add(x);
                    break;
                }
            }
        }
        else{
            result.Add(x);
        }

        for (var i = 0; i < result.Count; i++){
            switch (result[i]){
                case(10):
                    result[i] = "a";
                    break;
                case(11):
                    result[i] = "b";
                    break;
                case(12):
                    result[i] = "c";
                    break;
                case(13):
                    result[i] = "d";
                    break;
                case(14):
                    result[i] = "e";
                    break;
                case(15):
                    result[i] = "f";
                    break;
                default:
                    continue;
            }
        }

        Console.WriteLine(String.Join("", result.Select(y => y.ToString()).Reverse()));
    }
    

    
    
    

#region CountOccurences    

    

    
    public static int CountOccurences(string input, string searchPattern){
        /*if ((new string[]{ input, searchPattern }.Any(x => x == null))){
            throw new ArgumentException("Value cannot be null");//
        }*/
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        ArgumentNullException.ThrowIfNull(searchPattern, nameof(searchPattern));
        if (new string[]{ input, searchPattern }.Any(x => x == "")){
            throw new ArgumentException("Value cannot be empty");
        }
        if (searchPattern.Length > input.Length)
            throw new InvalidOperationException("Search pattern can not be longer than input");


        int result = 0;
        for (int i = 0; i < input.Length; i++){
            for (int j = 0; j < searchPattern.Length && j < input.Length && i < input.Length; j++){
                if (input[i] == searchPattern[j]){
                    if (i < input.Length && searchPattern.Length >= 2 && input[i] == searchPattern[^1]){
                        if (searchPattern[^1] != searchPattern[^2]){
                            result++;
                            for (int ii = i; ii < input.Length; ii++){
                                for (int jj = 0;
                                     jj < searchPattern.Length && jj < input.Length && ii < input.Length;
                                     j++){
                                    if (input[ii] == searchPattern[jj]){
                                        ii++;
                                        if (input[ii] == searchPattern[^1] && ii < input.Length){
                                            throw new InvalidOperationException(
                                                "Value should not be intersected with itself");
                                        }
                                    }
                                    else{
                                        break;
                                    }
                                }
                            }
                        }
                        else{
                            Label:
                            i++;
                            j++;
                            if (input.ElementAtOrDefault(i) == searchPattern.ElementAtOrDefault(j)
                                && input.ElementAtOrDefault(i + 1) == searchPattern.ElementAtOrDefault(j + 1)){
                                result++;
                            }
                            else{
                                goto Label;
                            }
                        }
                    }
                    else{
                        i++;
                    }
                }
            }
        }


        return result;
    }
#endregion    
    public static NoobCoders CreateUser1(string name, string email){
        // доработайте этот метод, добавив валидацию входных данных,
        // обработку и логирование ошибок
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(email);
        var log = new string[]{ name, email };
        if (log.Contains("")){
            throw new ArgumentException("Value cannot be empty");
        }

        if (!email.Contains('@')){
            throw new ArgumentException("Email should contain '@'");
        }

        var dbContext = NoobCodersDatabase.CreateContext();
        NoobCoders user;
        try{
            user = dbContext.CreateUser(name, email);
        }
        catch (SqlException ex){
            if (ex.Message == "Cannot insert duplicated value"){
                throw new InvalidOperationException("The user already added");
            }

            throw; // пробрасываем другие исключения выше
        }
        catch (NullReferenceException){
            Console.WriteLine("Internal error occurred. Retrying...");
            try{
                user = dbContext.CreateUser(name, email);
            }
            catch{
                throw; // если вторая попытка тоже не удалась, пробрасываем исключение
            }
        }
        catch (UnauthorizedAccessException ex){
            Console.WriteLine(ex.Message);
            throw;
        }
        finally{
            dbContext.Dispose();
        }

        return user;
    }
}



public class NoobCoders{
    public static async Task LoadDataAsync(CancellationToken token){
        
        try{
            var message = await GetMessageAsync(token);
            Console.WriteLine(message);
        }
        catch (OperationCanceledException e) when (e.CancellationToken == token){
            Console.WriteLine(e.Message);
            throw;
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
        }
        
    }
    private static Task<string> GetMessageAsync(CancellationToken token){
        return Task.FromResult("Hi, noobcoders! ;)");
    }

    public static async Task<NoobCoders> CreateUser(string userName, string userEmail)
        {
            var user = new NoobCoders(userName, userEmail);
            var createdUser = await NoobCoders.AddUser(user);
            return createdUser;
        }
    public NoobCoders(string name, string email){
        
    }

    public NoobCoders(){
    }

    public static async Task<NoobCoders> AddUser(NoobCoders user){
        await Task.Delay(2000);
        return await Task<NoobCoders>.FromResult(user);
    }

    public static Task<string> GetMessageAsync(){
        return Task.FromResult("hi, noobcoders! =)");
    }

    public static async Task LoadDataAsync(){
        var message = await GetMessageAsync();
        Console.WriteLine(message);
    }
}

public class NoobCodersDatabase{
    public static DbContext CreateContext(){
        return new DbContext();
    }

    public class DbContext : IDisposable{
        public NoobCoders CreateUser(string name, string email){
            return new NoobCoders();
        }

        

        public void Dispose(){
            // Реализация Dispose//
        }
    }
}