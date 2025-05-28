using OrganizaCaixas.Dtos.Input;
using OrganizaCaixas.Dtos.Output;
using System.Collections.Generic;
using System.Threading;

namespace OrganizaCaixas.Services
{
    public interface IPackagingService
    {
        Task<PedidosWrapperOutputDto> ProcessarPedidosAsync(PedidosWrapperInputDto pedidosWrapperInput);
    }
}