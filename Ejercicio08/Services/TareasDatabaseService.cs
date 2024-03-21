using Ejercicio08.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio08.Services
{
    public class TareasDataBaseService
    {
        readonly SQLiteAsyncConnection _database;
        public TareasDataBaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TareasModels>().Wait();

        }

        public Task<List<TareasModels>> ObtenerTareasAsync()
        {
            return _database.Table<TareasModels>().OrderByDescending(x => x.Fecha).ToListAsync();
        }

        public Task<int> GuardarTareasAsync(TareasModels tareasModel)
        {
            return _database.InsertAsync(tareasModel);
        }

        public async Task BorrarTareaAsync(TareasModels tareasModels)
        {
            await _database.DeleteAsync(tareasModels);
        }

        public async Task<int> ActualizarTareaAsync(TareasModels tareasModels)
        {
            return await _database.UpdateAsync(tareasModels);
        }

        public async Task<TareasModels> ObtenerUnaTarea(int id)
        {
            return await _database.FindAsync<TareasModels>(id);
        }

    }
}
