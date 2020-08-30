using System;
using System.Threading.Tasks;

namespace GridManagement.service
{
     public interface IHeroAppService: IDisposable
    {
        public Task<string> getString();
    }
}
