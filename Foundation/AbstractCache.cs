using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calibre.Foundation
{
    public abstract class AbstractCache<U, T>
        where U : AbstractCache<U, T>, new()
        where T : class
    {

        protected abstract T GetData();
        protected virtual TimeSpan GetLifetime() { return TimeSpan.FromMinutes(10); }

        protected AbstractCache() { }

        enum State
        {
            Empty,
            OnLine,
            Expired,
            Refreshing
        }

        static U Instance = new U();
        static T InMemoryData { get; set; }
        static volatile State CurrentState = State.Empty;
        static volatile object StateLock = new object();
        static volatile object DataLock = new object();
        static DateTime RefreshedOn = DateTime.MinValue;

        public static T Data
        {
            get
            {
                switch (CurrentState)
                {
                    case State.OnLine: // Simple check on time spent in cache vs lifetime
                        var timeSpentInCache = (DateTime.UtcNow - RefreshedOn);
                        if (timeSpentInCache > Instance.GetLifetime())
                        {
                            lock (StateLock)
                            {
                                if (CurrentState == State.OnLine) CurrentState = State.Expired;
                            }
                        }
                        break;

                    case State.Empty: // Initial load : blocking to all callers
                        lock (DataLock)
                        {
                            lock (StateLock)
                            {
                                if (CurrentState == State.Empty)
                                {
                                    InMemoryData = Instance.GetData(); // actually retrieve data from inheritor
                                    RefreshedOn = DateTime.UtcNow;
                                    CurrentState = State.OnLine;
                                }
                            }
                        }
                        break;

                    case State.Expired: // The first thread getting here launches an asynchronous refresh
                        lock (StateLock)
                        {
                            if (CurrentState == State.Expired)
                            {
                                CurrentState = State.Refreshing;
                                Task.Factory.StartNew(() => Refresh());
                            }
                        }
                        break;

                }

                lock (DataLock)
                {
                    if (InMemoryData != null) return InMemoryData;
                }

                return Data;
            }
        }

        static void Refresh()
        {
            if (CurrentState == State.Refreshing)
            {
                var dt = Instance.GetData(); // actually retrieve data from inheritor
                lock (StateLock)
                {
                    lock (DataLock)
                    {
                        RefreshedOn = DateTime.UtcNow;
                        CurrentState = State.OnLine;
                        InMemoryData = dt;
                    }
                }
            }
        }

        public static void Invalidate()
        {
            lock (StateLock)
            {
                RefreshedOn = DateTime.MinValue;
                CurrentState = State.Expired;
            }
        }
    }


}
