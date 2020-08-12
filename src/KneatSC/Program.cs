using KneatSC.Presenters;
using KneatSC.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KneatSC
{
    class Program
    {
        private IMainPresenter mainPresenter;

        public Program(IMainPresenter myManager)
        {
            this.mainPresenter = myManager;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await mainPresenter.Run(cancellationToken);
        }
    }
}
