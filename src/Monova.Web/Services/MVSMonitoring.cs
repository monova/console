using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Monova.Entity;

namespace Monova.Web
{
    public class MVSMonitoring : IMVService
    {
        private Task _task;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private MVDContext _db;

        public MVSMonitoring(MVDContext db)
        {
            _db = db;
        }

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _task = null;
            _task = new Task(Monitor, _token, TaskCreationOptions.LongRunning);
            _task.ConfigureAwait(false);
            _task.Start();
        }

        private void Monitor()
        {
            while (true)
            {

            }
        }
    }
}