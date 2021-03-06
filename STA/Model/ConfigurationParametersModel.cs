﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Model
{
    class ConfigurationParametersModel
    {
        /// <summary>
        /// Cantidad de horas permitidas por horario
        /// </summary>
        public int numberHours { get; set; }
        /// <summary>
        /// Cantidad de Horarios permitidos
        /// </summary>
        public int numberschedules { get; set; }
        /// <summary>
        /// Variable de control de envio de correo con registro
        /// </summary>
        public bool sendedEMail { get; set; }
        /// <summary>
        /// Fecha en la que se inicio el software por primera vez
        /// </summary>
        public DateTime installedDate { get; set; }
        /// <summary>
        /// Tiempo de validez del sistema segun licencia contratada
        /// </summary>
        public int lisenceTime { get; set; }
        /// <summary>
        /// Construnctor de la clase vacio
        /// </summary>
        ///        
        public ConfigurationParametersModel()
        {
            this.numberHours = 60;
            this.numberschedules = 60;
        }
        /// <summary>
        /// Constructor de la clase con parametros
        /// </summary>
        /// <param name="flag"></param>
        public ConfigurationParametersModel(bool flag)
        {
            this.numberHours = 60;
            this.numberschedules = 60;
            this.sendedEMail = flag;
            this.installedDate = DateTime.Now;
            this.lisenceTime = 999999;
        }
    }
}