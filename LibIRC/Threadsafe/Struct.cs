using System;
using System.Threading;

namespace LibIRC {
    /// <summary>
    /// A Class to wrap a struct in a threadsafe way with a mutex
    /// </summary>
    /// <typeparam name="T">The type of struct to be wrapped</typeparam>
    /// <seealso cref="LibIRC.ThreadSafeObject{T}"/>
    public class ThreadSafeStruct<T> where T : struct {
        /// <summary>
        /// The stored data
        /// </summary>
        private T wrapped;

        /// <summary>
        /// the Mutex to access the data
        /// </summary>
        private Mutex Access_Mutex;

        /// <summary>
        /// A Threadsafe way of getting the value
        /// </summary>
        /// <returns>The wrapped value</returns>
        public T Get () {
            Access_Mutex.WaitOne ();
            T t = wrapped;
            Access_Mutex.ReleaseMutex ();
            return t;
        }

        /// <summary>
        /// A Threadsafe way of setting the value
        /// </summary>
        /// <param name="t">the new value</param>
        public void Set (T t) {
            Access_Mutex.WaitOne ();
            wrapped = t;
            Access_Mutex.ReleaseMutex ();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t">The value to set the internal value to be</param>
        public ThreadSafeStruct (T t) {
            Access_Mutex = new Mutex ();
            Set (t);
        }

        /// <summary>
        ///  Executes a Function/Lambda in a ThreadSafe manner
        /// </summary>
        /// <param name="Function">A Function </param>
        /// <typeparam name="U"> The Return type of <paramref name="Function"/> </typeparam>
        /// <returns></returns>
        public U ExecuteFunction<U> (Func<T, U> Function) {
            Access_Mutex.WaitOne ();
            U u = Function (wrapped);
            Access_Mutex.ReleaseMutex ();
            return u;
        }

    }
}