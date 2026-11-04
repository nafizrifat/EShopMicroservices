using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    // Command without a meaningful response
    public interface ICommand : ICommand<Unit>
    {
    }

    // Command that returns a response
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {
    }
}
