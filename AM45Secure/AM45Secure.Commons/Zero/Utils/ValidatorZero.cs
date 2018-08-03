using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Zero.Attributes;
using Zero.Utils.Models;

namespace Zero.Utils
{
    /// <summary>
    /// Autor: wgrifaldo
    /// Creado el 23/10/2015 13:00
    /// Librearía propiedad de WM TI Soluciones(Walther Grifaldo Zúñiga) y Vision Consulting. Copyright (C) Vision Consulting All rights reserved. Todos los derechos reservados.
    /// 
    /// Clase que realiza las validaciones de un modelo de datos y verifica que sean requeridos o cumplan con un patron definido
    /// </summary>
    /// <remarks>
    /// Utilería para validacion de campos en el back-end
    /// </remarks>
    public class ValidatorZero
    {
        private static readonly Type[] TiposNumericos = {typeof(int), typeof(long), typeof(decimal)};
        /// <summary>
        /// Valida que los campos requeridos en el modelo se encuentren con valores
        /// </summary>
        /// <param name="model">modelo de datos</param>
        /// <returns></returns>
        public static IList<Validation> Validate<TModel>(TModel model)
        {
            return Validate(model, new OptionsValidation());
        }

        /// <summary>
        /// Valida que los campos requeridos en el modelo se encuentren con valores
        /// </summary>
        /// <param name="model">modelo de datos</param>
        /// <param name="options">opciones de validacion</param>
        /// <returns></returns>
        public static IList<Validation> Validate<TModel>(TModel model, OptionsValidation options)
        {
            IList<Validation> requeridos = ValidateRequiredModel(model, options);
            IList<Validation> validaciones = ValidateModel(model, options);
            return requeridos.Union(validaciones).ToList();
        }

        /// <summary>
        /// Valida los caracteres esoeciales de un modelo de datos
        /// </summary>
        /// <typeparam name="TModel">tipo del modelo de datos</typeparam>
        /// <param name="model">modelo de datos</param>
        /// <param name="options">opciones de validacion</param>
        /// <returns></returns>
        public static IList<Validation> ValidateModel<TModel>(TModel model, OptionsValidation options)
        {
            Type tipo = model.GetType();
            IList<PropertyInfo> propiedades = tipo.GetProperties();
            IList<Validation> reqs = new List<Validation>();

            foreach (PropertyInfo info in propiedades)
            {
                Validate[] attribs = info.GetCustomAttributes(typeof(Validate), false) as Validate[];
                if (attribs != null && attribs.Any())
                {
                    Validation campoRequerido = new Validation();
                    campoRequerido.Field = info.Name;
                    campoRequerido.IdField = info.Name;
                    var value = info.GetValue(model);
                    //si el valor es nulo se ignora
                    if (value != null)
                    {
                        foreach (Validate validate in attribs)
                        {
                            string texto;
                            if (info.PropertyType == typeof(string) && !string.IsNullOrEmpty((string)value))
                            {
                                texto = (string)value;
                            }
                            else
                            {
                                texto = string.Empty + value;
                            }

                            string newText = Regex.Replace(texto, validate.RegularExpression, "");

                            if (texto != newText)
                            {
                                campoRequerido.Message += validate.Message;
                                reqs.Add(campoRequerido);
                            }
                        }
                    }

                }
            }
            return reqs;
        }
        /// <summary>
        /// Valida que los campos requeridos en el modelo se encuentren con valores
        /// </summary>
        /// <param name="model">modelo de datos</param>
        /// <returns></returns>
        public static IList<Validation> ValidateRequiredModel<TModel>(TModel model)
        {
            return ValidateRequiredModel(model, new OptionsValidation());
        }
        /// <summary>
        /// Valida que los campos requeridos en el modelo se encuentren con valores
        /// </summary>
        /// <param name="model">modelo de datos</param>
        /// <param name="options">Opciones para validar requeridos</param>
        /// <returns></returns>
        public static IList<Validation> ValidateRequiredModel<TModel>(TModel model, OptionsValidation options)
        {
            Type tipo = model.GetType();
            IList<PropertyInfo> propiedades = tipo.GetProperties();
            IList<Validation> reqs = new List<Validation>();

            foreach (PropertyInfo info in propiedades)
            {
                Required required = info.GetCustomAttribute(typeof(Required), false) as Required;
                if (required != null)
                {
                    if (options.ExcludeOptionals && required.Optional)
                    {
                        continue;
                    }

                    Validation campoRequerido = new Validation
                                                {
                                                    Field = required.FieldName,
                                                    IdField = info.Name,
                                                    Message = required.MessageRequired
                                                };
                    if (info.GetValue(model) == null)
                    {
                        reqs.Add(campoRequerido);
                    }
                    else
                    {
                        if (info.PropertyType == typeof(string) && (string.IsNullOrEmpty((string)info.GetValue(model)) || string.IsNullOrWhiteSpace((string)info.GetValue(model))))
                        {
                            reqs.Add(campoRequerido);
                        }
                        else if (typeof(IList).IsInstanceOfType(info.GetValue(model)))
                        {
                            IList lista = (IList)info.GetValue(model);
                            if (lista.Count == 0)
                            {
                                reqs.Add(campoRequerido);
                            }
                        }
                        else if (options.ValidateIntCero && TiposNumericos.Contains(info.PropertyType) )
                        {
                            decimal changeType = (decimal)ZeroUtils.ChangeType(info.GetValue(model), typeof(decimal));
                            if (changeType <= 0)
                            {
                                reqs.Add(campoRequerido);
                            }
                        }
                        else if (typeof(DateTime).IsInstanceOfType(info.GetValue(model)))
                        {
                            DateTime fecha = (DateTime)info.GetValue(model);
                            if (fecha == DateTime.MinValue)
                            {
                                reqs.Add(campoRequerido);
                            }
                        }
                    }
                }
            }
            return reqs;
        }
    }
}
