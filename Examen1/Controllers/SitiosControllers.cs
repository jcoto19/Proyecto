using System;
using System.Collections.Generic;
using System.Text;
using Examen1.Models;
using System.Threading.Tasks;
using SQLite;

namespace Examen1.Controllers
{
    public class SitiosControllers
    {
        readonly SQLiteAsyncConnection conexion;

        public SitiosControllers(string dbpath)
        {
            conexion = new SQLiteAsyncConnection(dbpath);

            conexion.CreateTableAsync<SitiosVisitadoscs>().Wait();
        }

        public Task<int> SaveContacto(SitiosVisitadoscs sitios)
        {
            if (sitios.id != 0)
                return conexion.UpdateAsync(sitios);
            else
                return conexion.InsertAsync(sitios);

        }

        public Task<List<SitiosVisitadoscs>> GetListSitios()
        {
            return conexion.Table<SitiosVisitadoscs>().ToListAsync();
        }

        public Task<SitiosVisitadoscs> GetSitios(int pid)
        {
            return conexion.Table<SitiosVisitadoscs>()
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }

        public Task<int> DeleteSitios(SitiosVisitadoscs sitios)
        {
            return conexion.DeleteAsync(sitios);
        }

    }
}
