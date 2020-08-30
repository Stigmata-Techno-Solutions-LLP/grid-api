using System;
using System.Threading.Tasks;

namespace GridManagement.service
{
    public class HeroAppService : IHeroAppService
    {

        async public Task<string> getString()
        {
            return await Task.Run(() => "sdfsdf") ;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // _heroRepository.Dispose();
            }
        }
    }
}
