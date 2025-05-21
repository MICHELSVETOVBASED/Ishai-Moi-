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
        
    }
    public static int CountOccurences(string input, string searchPattern){
        if ((new string[]{ input, searchPattern }.Any(x => x == null))){
            throw new ArgumentException("Value cannot be null");
        }
        return 0;
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
        NoobCodersUser user = null;
        try{
            user = dbContext.CreateUser(name, email);
        }
        catch (SqlException ex){
            if (ex.Message == "Cannot insert duplicated value"){
                throw new InvalidOperationException("The user already added");
            }
            throw; // пробрасываем другие исключения выше
        }
        catch (NullReferenceException ex){
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