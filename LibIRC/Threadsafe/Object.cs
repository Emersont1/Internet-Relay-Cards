using System;
using System.Threading;

namespace LibIRC {
     /// <summary>
    /// A Class to wrap a class in a threadsafe way with a mutex
    /// </summary>
    /// <typeparam name="T">The type of class to be wrapped</typeparam>
    /// <seealso cref="LibIRC.ThreadSafeStruct{T}"/>
    public class ThreadSafeObject<T> where T : class {
        private T wrapped;
        Mutex Access_Mutex;

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
        public ThreadSafeObject (T t) {
            Access_Mutex = new Mutex ();
            Set (t);
        }

        /// <summary>
        ///  Executes a Function/Lambda in a ThreadSafe manner. The lambda MUST return a value.
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