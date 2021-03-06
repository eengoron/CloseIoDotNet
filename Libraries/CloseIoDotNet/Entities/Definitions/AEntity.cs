﻿namespace CloseIoDotNet.Entities.Definitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Fields;

    public abstract class AEntity<T> : IEntity where T : IEntity, new()
    {
        #region Instance Variables
        private IEnumerable<IEntityField> _entityFields;
        #endregion

        #region Properties
        public IEnumerable<IEntityField> EntityFields => _entityFields ?? (_entityFields = GenerateEntityFieldEnumerable());
        #endregion

        #region Methods
        private static IEnumerable<IEntityField> GenerateEntityFieldEnumerable()
        {
            var result = new List<IEntityField>();

            var properties = typeof (T).GetProperties()
                .SelectMany(property => property.GetCustomAttributes(true))
                .Where(attribute => attribute.GetType().GetInterfaces().Contains(typeof(IEntityField)))
                .ToList();

            properties.ForEach(
                entry => {
                    result.Add((IEntityField) entry);
            });

            return result;
        }
        #endregion
    }
}