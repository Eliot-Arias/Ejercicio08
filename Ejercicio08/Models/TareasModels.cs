using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicio08.Models
{
    public class TareasModels
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } 
        public string TareaName { get; set; }
        public string Description { get; set;}
        public DateTime Fecha { get; set; }
    }
}
