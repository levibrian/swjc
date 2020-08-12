using System.Threading;
using System.Threading.Tasks;

namespace KneatSC.Presenters
{
    public interface IMainPresenter
    {
        Task Run(CancellationToken cancellationToken);
    }
}
