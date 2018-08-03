namespace Zero.Ado.Models
{
    /// <summary>
    /// Autor: wgrifaldo
    /// Creado el 23/10/2015 13:00
    /// Librearía propiedad de WM TI Soluciones(Walther Grifaldo Zúñiga) y Vision Consulting. Copyright (C) Vision Consulting All rights reserved. Todos los derechos reservados.
    /// 
    /// Clase que se usa para enviar opciones a la clase QeryZero y generar la consulta o sentencia.
    /// </summary>
    /// <remarks>
    /// Utilería que funciona para enviar parametros de comportamiento de la estructuracion de las sentencias y consultas
    /// </remarks>
    public class OptionsQueryZero
    {
        /// <summary>
        /// En caso de requerir filtros complejos se puede incluir una sentencia o condiciones para que se incluyan en el where
        /// </summary>
        public string WhereComplementary { get; set; } = string.Empty;
        /// <summary>
        /// Forza a que se realice un delete sin where
        /// </summary>
        public bool ForceDelete { get; set; } = false;
        /// <summary>
        /// Excluye los numericos por default, que tengan 0
        /// </summary>
        public bool ExcludeNumericsDefaults { get; set; } = false;
        /// <summary>
        /// Excluye los boleanos del where de una consulta
        /// </summary>
        public bool ExcludeBool { get; set; } = false;
        /// <summary>
        /// Solo para consultar, excluye cualquier clausula where
        /// </summary>
        public bool ExcludeWhere { get; set; } = false;
        /// <summary>
        /// Cambia el signo igual por un like en valores string
        /// </summary>
        public bool UseLikeForString { get; set; } = false;
    }
}
