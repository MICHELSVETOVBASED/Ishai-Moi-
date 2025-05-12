using System.Net;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.IO;

class Program{
    static void Main(string[] args){
        
    }

    public class MainClass
    {    
        private static readonly string[] filmGenres =
        {
            "Детектив", "Комедия", "Триллер",
            "Научная фантастика", "Документальный",
            "Ужасы", "Фэнтези", "Боевик",
            "Мультфильм", "Приключения",
            "Криминал", "Мюзикл","Проверьте корректность ввода"
        };

        public static void PrintGenre(){

            Console.WriteLine(filmGenres[int.TryParse(Console.ReadLine(), out int result) && result <12 ? result : 12]);
        }
    }
}
