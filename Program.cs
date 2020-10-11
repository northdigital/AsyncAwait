using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
  class Program
  {

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
      var task = Task.Run(() =>
      {
        Thread.Sleep(3000);  
      });

      task.Wait();

      return task;

      // OR
      //return Task.CompletedTask;
    }

    #endregion

    #region async Task return nothing and must await

    public static async Task f5()
    {
      string a = string.Empty;

      if (a == "b")
        return;

      await Task.Delay(11);
    }

    #endregion

    static async Task Main(string[] args)
    {
      var i1 = await f1();
      var i2 = await f2();
      await f4();
      await f5();

      Console.WriteLine($"-> {i1} {i2}");
    }
  }
}
