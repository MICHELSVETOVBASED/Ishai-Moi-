using System.Net;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.IO;
using System.Runtime.InteropServices;


namespace Jewbee;
class Program{
    static void Main(string[] args){
        
    }

    public static NoobCodersUser CreateUser(string name, string email)
    {
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
        var user = dbContext.CreateUser(name, email);
        return user;
    }
    
}

public class NoobCodersUser{
}
public class NoobCodersDatabase{
    public static  DbContext CreateContext(){
        return new DbContext();
    }

    public class DbContext{
        public NoobCodersUser CreateUser(string name,string email){
            return new NoobCodersUser();
        }
    }
}