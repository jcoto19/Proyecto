using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Examen1.Models;
using System.Threading.Tasks;

namespace Examen1.Controllers
{
    public class ContactosControllers
    {
        
        readonly SQLiteAsyncConnection conexion;

        public ContactosControllers(string dbpath)
        {
            conexion = new SQLiteAsyncConnection(dbpath);

            conexion.CreateTableAsync<Contactos>().Wait();
        }

        public Task<int> SaveContacto(Contactos contactos) 
        {
            if(contactos.id !=0) 
                return conexion.UpdateAsync (contactos);
            else
                return conexion.InsertAsync(contactos);
            
        }

        public Task<List<Contactos>> GetListContactos() 
        {
            return conexion.Table<Contactos>().ToListAsync();
        }

        public Task<Contactos> GetContactos(int pid)
        {
            return conexion.Table<Contactos>()
                .Where(i=>i.id == pid)
                .FirstOrDefaultAsync();
        }

        public Task<int> DeleteContacto(Contactos contacto)
        {
            return conexion.DeleteAsync(contacto);
        }

        public async Task<List<Contactos>> GetContactosAsync()
        {
            return await conexion.Table<Contactos>().ToListAsync();
        }
    }
}
