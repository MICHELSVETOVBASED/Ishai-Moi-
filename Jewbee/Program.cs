using System.Net;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace Jewbee;
class Program{
    static void Main(string[] args){
        Console.WriteLine(CountOccurences("abbcdgeabb", "abb"));

    }
    public static int CountOccurences(string input, string searchPattern){
        /*if ((new string[]{ input, searchPattern }.Any(x => x == null))){
            throw new ArgumentException("Value cannot be null");//
        }*/
        try{
            ArgumentNullException.ThrowIfNull(input, nameof(input));
            ArgumentNullException.ThrowIfNull(searchPattern, nameof(searchPattern));
        }
        catch (ArgumentNullException){
            throw new ArgumentNullException("Value cannot be null");
        }

        if (new string[]{ input, searchPattern }.Any(x => x == "")){
            throw new ArgumentException("Value cannot be empty");
        }

        if (searchPattern.Length > input.Length)
            throw new InvalidOperationException("Search pattern can not be longer than input");
        
        
        int result = 0;
        for (int i = 0; i < input.Length; i++){
            for (int j = 0; j < searchPattern.Length && j < input.Length && i <input.Length; j++){
                if (input[i] == searchPattern[j]){
                    i++;
                    if (i<input.Length && searchPattern.Length >= 2 && input[i] == searchPattern[^1] && searchPattern[^1] != searchPattern[^2]){
                        result++;
                        for (int ii = i; ii < input.Length; ii++){
                            for (int jj = 0; jj < searchPattern.Length && jj < input.Length && ii < input.Length; j++){
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
                }
            }
        }


        return result;
        
    }

    public static NoobCodersUser CreateUser(string name, string email){
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
        NoobCodersUser user;
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
            try {
                user = dbContext.CreateUser(name, email);
            }
            catch {
                throw; // если вторая попытка тоже не удалась, пробрасываем исключение
            }
        }
        catch (UnauthorizedAccessException ex){
            Console.WriteLine(ex.Message);
            throw;//modmod
        }
        finally{
            dbContext.Dispose();
        }
        return user;
    }//ddddd
}

public class NoobCodersUser{
}

public class NoobCodersDatabase{
    public static DbContext CreateContext(){
        return new DbContext();
    }

    public class DbContext : IDisposable{
        public NoobCodersUser CreateUser(string name,string email){
            return new NoobCodersUser();
        }

        public void Dispose(){
            // Реализация Dispose
        }
    }
}