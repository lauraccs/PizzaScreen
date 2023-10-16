using Newtonsoft.Json;
using PizzaCodeTest.Models;
/*
 * Applicant: Laura Santana
 * Position: Back-end Engineer (.NET)
 * 
 */
namespace PizzaCodeTest;
class Program
{
    static void Main(string[] args)
    {
        string path = System.AppContext.BaseDirectory;
        path = path.Replace("\\bin\\Debug\\net6.0", "").Replace("/bin/Debug/net6.0", "");
        string dataPath = path + "Data/Data.json";

        List<EmployeePizzaPreference> employeePizzaPreferences;
        using (StreamReader r = new StreamReader(dataPath))
        {
            string json = string.Empty;
            json = r.ReadToEnd();
            employeePizzaPreferences = JsonConvert.DeserializeObject<List<EmployeePizzaPreference>>(json);
        }

        Console.WriteLine("Which department has the largest number of employees who like Pineapple on their pizzas?");
        var deptOfPineappleLovers = employeePizzaPreferences.Where(x => x.Toppings.Contains("Pineapple")).Select(y => y).ToList().GroupBy(z => z.Department).First().Key;
        Console.WriteLine(deptOfPineappleLovers);
        Console.WriteLine();

        Console.WriteLine("Which department prefers Peperoni and Onions?");
        var deptOfPepperoniOnionLovers = employeePizzaPreferences.Where(x => x.Toppings.Contains("Pepperoni") && x.Toppings.Contains("Onions")).Select(y => y).ToList().GroupBy(z => z.Department).First().Key;
        Console.WriteLine(deptOfPepperoniOnionLovers);
        Console.WriteLine();

        Console.WriteLine("How many people prefer Anchovies?");
        var anchovieLoversCount = employeePizzaPreferences.Where(x => x.Toppings.Contains("Anchovies")).Select(y => y).ToList().GroupBy(z => z.Name).Count();
        Console.WriteLine(anchovieLoversCount);
        Console.WriteLine();

        Console.WriteLine("How many pizzas would you need to order to feed the Engineering department, assuming a pizza feeds 4 people? Ignore personal preferences.");
        var pizzasNeededToFeedEngDept = Math.Round(Convert.ToDouble(employeePizzaPreferences.Where(x => x.Department.Equals("Engineering")).Count()) / 4, 0);
        Console.WriteLine(pizzasNeededToFeedEngDept);
        Console.WriteLine();

        Console.WriteLine("Which pizza topping combination is the most popular in each department and how many employees prefer it?");
        var departments = employeePizzaPreferences.Select(x => x.Department).Distinct().ToList();
        foreach (var dept in departments)
        {
            var mostLovedCombo = employeePizzaPreferences.Where(x => x.Department.Equals(dept)).GroupBy(y => y.Toppings);
            Console.WriteLine(dept + ": " + mostLovedCombo.First().Key[0] + " + " + mostLovedCombo.First().Key[1] + " -- preferred by " + mostLovedCombo.Count() + " employees");
        }

    }
}

