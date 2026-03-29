using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("1. Sync + Sequential");
        Console.WriteLine("Total time ≈ A + B");
        Console.WriteLine("One operation starts only after the previous one finishes.");
        Console.WriteLine("==================================================");

        var sw = Stopwatch.StartNew();

        var s1 = BlockingA();
        var s2 = BlockingB();

        Console.WriteLine($"{s1}, {s2}");
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine();

        Console.WriteLine("==================================================");
        Console.WriteLine("2. Async + Sequential");
        Console.WriteLine("Total time ≈ A + B");
        Console.WriteLine("Non-blocking waiting, but still NOT concurrent.");
        Console.WriteLine("B starts only after A has fully completed.");
        Console.WriteLine("==================================================");

        sw.Restart();

        var a1 = await AsyncA();
        var b1 = await AsyncB();

        Console.WriteLine($"{a1}, {b1}");
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine();

        Console.WriteLine("==================================================");
        Console.WriteLine("3. Async + Concurrent");
        Console.WriteLine("Total time ≈ longest running operation = max(A, B)");
        Console.WriteLine("Both operations are started BEFORE awaiting.");
        Console.WriteLine("This is the common modern scalable pattern.");
        Console.WriteLine("==================================================");

        sw.Restart();

        // IMPORTANT:
        // Calling AsyncA() starts its work and returns a Task immediately.
        // Calling AsyncB() starts its work and returns a Task immediately.
        // await DOES NOT start the work. It only waits for completion.
        var taskA = AsyncA();
        var taskB = AsyncB();

        // At this point A and B are both already in progress.
        // That is why this is concurrent.
        var a2 = await taskA;
        var b2 = await taskB;

        Console.WriteLine($"{a2}, {b2}");
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine();

        Console.WriteLine("==================================================");
        Console.WriteLine("4. Sync + Concurrent");
        Console.WriteLine("Total time ≈ longest running operation = max(A, B)");
        Console.WriteLine("Concurrent, but NOT async.");
        Console.WriteLine("Uses separate threads and blocks them.");
        Console.WriteLine("Rarely desirable for IO-bound work.");
        Console.WriteLine("==================================================");

        sw.Restart();

        string? t1Result = null;
        string? t2Result = null;

        // This IS concurrency without async/await.
        // Each thread runs blocking code.
        // The operations are in progress at the same time,
        // but they block their own threads while sleeping/working.
        var t1 = new Thread(() => t1Result = BlockingA());
        var t2 = new Thread(() => t2Result = BlockingB());

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine($"{t1Result}, {t2Result}");
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine();

        Console.WriteLine("==================================================");
        Console.WriteLine("Summary");
        Console.WriteLine("==================================================");

        Console.WriteLine("Async = non-blocking waiting.");
        Console.WriteLine("Concurrent = more than one operation in progress.");
        Console.WriteLine();
        Console.WriteLine("Async does NOT imply concurrency.");
        Console.WriteLine("Concurrency does NOT require async.");
        Console.WriteLine();
        Console.WriteLine("The practical rule:");
        Console.WriteLine("- Sync + Sequential        -> blocking, one by one");
        Console.WriteLine("- Async + Sequential       -> non-blocking, one by one");
        Console.WriteLine("- Async + Concurrent       -> non-blocking, multiple operations in progress");
        Console.WriteLine("- Sync + Concurrent        -> blocking threads, multiple operations in progress");
        Console.WriteLine();
        Console.WriteLine("Best teaching shortcut:");
        Console.WriteLine("- Sequential async  -> A + B");
        Console.WriteLine("- Concurrent async  -> max(A, B)");
    }

    // -------------------------------------------------
    // Sync + Sequential / Sync + Concurrent building blocks
    // -------------------------------------------------

    private static string BlockingA()
    {
        Console.WriteLine($"BlockingA started on thread {Environment.CurrentManagedThreadId}");
        Thread.Sleep(2000); // blocks the current thread
        Console.WriteLine($"BlockingA finished on thread {Environment.CurrentManagedThreadId}");
        return "Blocking A";
    }

    private static string BlockingB()
    {
        Console.WriteLine($"BlockingB started on thread {Environment.CurrentManagedThreadId}");
        Thread.Sleep(2000); // blocks the current thread
        Console.WriteLine($"BlockingB finished on thread {Environment.CurrentManagedThreadId}");
        return "Blocking B";
    }

    // -------------------------------------------------
    // Async + Sequential / Async + Concurrent building blocks
    // -------------------------------------------------

    private static async Task<string> AsyncA()
    {
        Console.WriteLine($"AsyncA started on thread {Environment.CurrentManagedThreadId}");

        // await means:
        // - pause this method here
        // - release the thread
        // - resume later when the awaited operation completes
        await Task.Delay(2000);

        Console.WriteLine($"AsyncA finished on thread {Environment.CurrentManagedThreadId}");
        return "Async A";
    }

    private static async Task<string> AsyncB()
    {
        Console.WriteLine($"AsyncB started on thread {Environment.CurrentManagedThreadId}");
        await Task.Delay(2000);
        Console.WriteLine($"AsyncB finished on thread {Environment.CurrentManagedThreadId}");
        return "Async B";
    }
}