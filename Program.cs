using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
  class Program
  {
    // whatever is a Task is awaitable
    // whatever is async it is waiting for something, if it is also a Task it is also awaitable
    // we cannot await async void functions and these run syncronously
    // public static async Task f5() is awaitable
    // public static async void f5() is not

    #region async Task<int> must return an int value

    public static async Task<int> f1()
    {
      await Task.Delay(5);

      return 123;
    }

    #endregion

    #region Task<int> must return a Task<int> calling Task.FromResult<int>() or a function that returns Task<int>

    public static Task<int> f2()
    {
      return Task.FromResult<int>(123);
    }

    public static Task<int> f3()
    {
      return f2();
    }

    #endregion

    #region Task must return a Task or Task.CompletedTask or similal
    
    public static Task f4()
    {
      return Task.Run(() =>
      {
        Thread.Sleep(3000);  
      });
    }

    #endregion

    #region async Task return nothing and must await

    public static async Task f5()
    {
      string a = string.Empty;

      if (a == "b")
        return;

      await Task.Delay(3000);
    }

    public static async void f6()
    {
      string a = string.Empty;

      if (a == "b")
        return;

      await Task.Delay(3000); // εδώ παίζει το await αλλά η f6 δεν είναι awaitable
      Console.WriteLine("I am here!");
    }

    #endregion

    static async Task Main(string[] args)
    {
      var i1 = await f1();
      var i2 = await f2();
      await f4();
      await f5();
      f6();

      Console.WriteLine($"-> {i1} {i2}");
      Console.ReadLine(); // χωρίς αυτό το λεκτικό "I am here!" δεν θα εμφανιστεί
    }
  }
}
