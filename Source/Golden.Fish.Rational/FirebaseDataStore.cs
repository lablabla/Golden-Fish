using FireSharp.Core;
using FireSharp.Core.Config;
using FireSharp.Core.EventStreaming;
using FireSharp.Core.Interfaces;
using FireSharp.Core.Response;
using Golden.Fish.Core.Models;
using Golden.Fish.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dna;
using static Dna.FrameworkDI;
using System.Threading.Tasks;

namespace Golden.Fish.Rational
{
    public class FirebaseDataStore : IClientDataStore, IDisposable
    {
        private const string JOBS = "jobs";
        private const string VALVES = "valves";
        private IFirebaseClient mFirebaseClient;
        private EventStreamResponse mJobsStreamResponse;
        private EventStreamResponse mValvesStreamResponse;

        public event EventHandler JobsChanged;
        public event EventHandler ValvesChanged;

        public FirebaseDataStore()
        {
            string authSecret = string.Empty;
            using (StreamReader sr = new StreamReader("firebase_key.txt"))
            {
                authSecret = sr.ReadToEnd();
            }
            IFirebaseConfig config = new FirebaseConfig
            {
                BasePath = "https://golden-fish-9709c.firebaseio.com/",
                AuthSecret = authSecret,
            };
            mFirebaseClient = new FirebaseClient(config);
        }


        public async Task EnsureDataStoreAsync()
        {
            mJobsStreamResponse = await mFirebaseClient.OnAsync(JOBS, null, OnJobsChanged);
            mValvesStreamResponse = await mFirebaseClient.OnAsync(VALVES, null, OnVavlesChanged);
        }

        public async Task<IReadOnlyCollection<Event>> GetScheduledJobsAsync()
        {
            var result = await mFirebaseClient.GetAsync(JOBS);
            var dict = result.ResultAs<Dictionary<string, Event>>();
            return dict.Select(x => x.Value).ToList();
        }

        public async Task<IReadOnlyCollection<Valve>> GetValvesAsync()
        {
            var result = await mFirebaseClient.GetAsync(VALVES);
            var dict = result.ResultAs<Dictionary<string, Valve>>();
            return dict.Select(x => x.Value).ToList();
        }

        public async Task SetScheduledJobsAsync(ICollection<Event> jobs)
        {

            // delete given child node
            await mFirebaseClient.DeleteAsync(JOBS);
            var e = jobs.GetEnumerator();

            while (e.MoveNext())
            {
                await mFirebaseClient.PushAsync(JOBS, e.Current).ConfigureAwait(false);
            }

        }

        public async Task SetValvesAsync(ICollection<Valve> valves)
        {

            // delete given child node
            await mFirebaseClient.DeleteAsync(VALVES);

            var e = valves.GetEnumerator();
            while (e.MoveNext())
            {
                await mFirebaseClient.PushAsync(VALVES, e.Current).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            mFirebaseClient.Dispose();
            mJobsStreamResponse.Dispose();
            mValvesStreamResponse.Dispose();
        }

        private void OnJobsChanged(object sender, ValueChangedEventArgs args, object context)
        {
            Logger.LogDebugSource($"Jobs Changed! {args.Data}");
        }

        private void OnVavlesChanged(object sender, ValueChangedEventArgs args, object context)
        {
            Logger.LogDebugSource($"Valves Changed! {args.Data}");
        }
    }
}