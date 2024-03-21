using Ejercicio08.Models;
using Ejercicio08.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Ejercicio08.ViewModel
{
    public class TareasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool vistaDetalle = false;
        public bool VistaDetalle
        {
            get { return vistaDetalle; }
            set 
            { 
                vistaDetalle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VistaDetalle"));
            }
        }

        private string comando = string.Empty;
        public string Comando
        {
            get { return comando; }
            set
            {
                comando = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Comando"));
            }
        }


        //----- Propiedades
        private string tareaName;
        public string TareaName
        {
            get { return tareaName; }
            set 
            { 
                tareaName = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TareaName"));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }


        private DateTime fecha = DateTime.Now;
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fecha"));
            }
        }

        private List<TareasModels> tareasList;
        public List<TareasModels> TareasList
        {
            get { return tareasList; }
            set
            {
                tareasList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TareasList"));
            }
        }

        private TareasModels tareaSeleccionada;
        public TareasModels TareaSeleccionada
        {
            get { return tareaSeleccionada; }
            set
            {
                tareaSeleccionada = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TareaSeleccionada"));
            }
        }

        //----------
        public Command cmdProcesarTarea {  get; set; }
        public Command cmdCancelarTarea { get; set; }
        ///---------
        ///
        public Command cmdAgregarTarea { get; set; }
        public Command cmdBorrarTarea { get; set; }
        public Command cmdActualizarTarea { get; set; }


        public TareasViewModel()
        {
            cmdProcesarTarea = new Command(ProcesarTarea);
            cmdCancelarTarea = new Command(CancelarTarea);
            //---
            cmdAgregarTarea = new Command(AgregarTarea);
            cmdBorrarTarea = new Command(BorrarTarea);
            cmdActualizarTarea = new Command(ActualizarTarea);
        }

        private void ActualizarTarea(Object obj)
        {
            VistaDetalle = true;
            Comando = "Actualizar";
            if (TareaSeleccionada != null)
            {
                TareaName = tareaSeleccionada.TareaName;
                Description = tareaSeleccionada.Description;
                Fecha = tareaSeleccionada.Fecha;
            }
            else
            {
                VistaDetalle = false;
                comando = string.Empty;
            }

        }

        private void BorrarTarea(Object obj)
        {
            VistaDetalle=false;
            Comando = "Borrar";
            if (tareaSeleccionada != null)
            {
                TareaName = tareaSeleccionada.TareaName;
                Description = tareaSeleccionada.Description;
                Fecha = tareaSeleccionada.Fecha;
            }
            else
            {
                VistaDetalle = false;
                comando= string.Empty;
            }

        }

        private void AgregarTarea(object obj)
        {
            VistaDetalle = true;
            Comando = "Agregar";
            TareaName = Description = string.Empty;
        }

        private void CancelarTarea(Object obj)
        {
            VistaDetalle = false;
            comando = string.Empty;
        }

        private async void ProcesarTarea(Object obj)
        {
            if (Comando == "Agregar")
            {
                var r = await App.TareasDataBase.GuardarTareasAsync(new TareasModels
                {
                    TareaName = TareaName,
                    Description = Description,
                    Fecha = Fecha,
                });
            }
            else if (Comando == "Actualizar")
            {
                tareaSeleccionada.TareaName = TareaName;
                tareaSeleccionada.Description = Description;
                tareaSeleccionada.Fecha = Fecha;
                await App.TareasDataBase.ActualizarTareaAsync(tareaSeleccionada);
            }
            else if (Comando == "Borrar")
            {
                await App.TareasDataBase.BorrarTareaAsync(TareaSeleccionada);
            }
            VistaDetalle = false;
            comando = string.Empty;
            tareaSeleccionada = null;
            ObtenerTarea();
            
        }

        public async void ObtenerTarea()
        {
            TareasList = await App.TareasDataBase.ObtenerTareasAsync();
        }




    }
}
