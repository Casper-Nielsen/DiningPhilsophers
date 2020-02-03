using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dining_philsophers
{
    class Program
    {
        static void Main()
        {
            //makes the 5 philsopher
            //gives what forks it can get to
            Philsopher p1 = new Philsopher(new int[] { 0, 1 });
            Philsopher p2 = new Philsopher(new int[] { 1, 2 });
            Philsopher p3 = new Philsopher(new int[] { 2, 3 });
            Philsopher p4 = new Philsopher(new int[] { 3, 4 });
            Philsopher p5 = new Philsopher(new int[] { 4, 0 });

            //makes threads for each philsoph
            Thread t1 = new Thread(p1.Start)
            {
                //gives it a name
                Name = "Philoph 1",
                //gives it a priority
                Priority = ThreadPriority.BelowNormal
            };
            Thread t2 = new Thread(p2.Start)
            {
                //gives it a name
                Name = "Philoph 2",
                //gives it a priority
                Priority = ThreadPriority.BelowNormal
            };
            Thread t3 = new Thread(p3.Start)
            {
                //gives it a name
                Name = "Philoph 3",
                //gives it a priority
                Priority = ThreadPriority.BelowNormal
            };
            Thread t4 = new Thread(p4.Start)
            {
                //gives it a name
                Name = "Philoph 4",
                //gives it a priority
                Priority = ThreadPriority.BelowNormal
            };
            Thread t5 = new Thread(p5.Start)
            {
                //gives it a name
                Name = "Philoph 5",
                //gives it a priority
                Priority = ThreadPriority.BelowNormal
            };

            //starts all the new threads
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

            //waits 4 sec before it clears the console (for looks only)
            Thread.Sleep(4000);
            Console.Clear();
            //looks for a enter to exit the program
            Console.ReadLine();
            //removes all the threads 
            t1.Abort();
            t2.Abort();
            t3.Abort();
            t4.Abort();
            t5.Abort();
        }
    }

    //
    //saves what forks is free
    //
    class Table
    {
        public static bool[] forks = new bool[5] { true, true, true, true, true };
    }

    //
    //a philsoph need two forks to eat
    //writes when it eats, done with eating and when it cant get 2 forks
    //
    class Philsopher
    {
        //the forks it can get to
        private readonly int[] possibleForks;

        //constructor
        public Philsopher(int[] possibleForks)
        {
            this.possibleForks = possibleForks;
        }

        //the main method
        public void Start()
        {
            //used for have a bit more random selection for when they try to eat
            Random rnd = new Random();
            bool forksFree;
            //will run for ever
            while (true)
            {
                forksFree = false;
                lock (Table.forks)
                {
                    forksFree = (Table.forks[possibleForks[0]] && Table.forks[possibleForks[1]]);
                }
                //if the two forks is free
                if (forksFree)
                {
                    //lock the array so it is only that person that can edit in it
                    lock (Table.forks)
                    {
                        //takes the forks
                        Table.forks[possibleForks[0]] = false;
                        Table.forks[possibleForks[1]] = false;

                        //inform the user that it is eating
                        Console.SetCursorPosition(0, Thread.CurrentThread.ManagedThreadId);
                        Console.WriteLine("[{0}] {1} is eating           ", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
                    }
                    //eats between 1 sec and 3sec
                    Thread.Sleep(rnd.Next(1000,3001));
                    //locks the array agein
                    lock (Table.forks)
                    {
                        //gives the forks back
                        Table.forks[possibleForks[0]] = true;
                        Table.forks[possibleForks[1]] = true;

                        //inform the user that it is done eating
                        Console.SetCursorPosition(0, Thread.CurrentThread.ManagedThreadId);
                        Console.WriteLine("[{0}] {1}              ", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
                    }
                    //whats longere so the other have a bigger chance to get it
                    Thread.Sleep(rnd.Next(750, 801));
                }
                //if the to forks is not free
                else
                {
                    //inform the user that it cant eat
                    Console.SetCursorPosition(0, Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("[{0}] {1} cant eat      ", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);

                    //waits between 0.4 sec and 0.5 sec before it trys agein
                    Thread.Sleep(rnd.Next(400, 501));
                }
            }
        }
    }
}
