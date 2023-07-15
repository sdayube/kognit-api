using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Kognit.API.Domain.Common
{
    /// <summary>
    ///     Representa uma entidade com propriedades dinâmicas, guardadas em um dicionário serializável.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Entidade base para delimitação dos campos dinâmicos.
    /// </typeparam>
    public class DynamicEntity<TEntity> : DynamicObject, IXmlSerializable, IDictionary<string, object> where TEntity : class
    {
        private readonly string _root = "DynamicEntity";
        private readonly IDictionary<string, object> _expando = null;

        public DynamicEntity()
        {
            _expando = new ExpandoObject();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_expando.TryGetValue(binder.Name, out object value))
            {
                result = value;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (IsFieldValid(binder.Name))
            {
                _expando[binder.Name] = value;
                return true;
            }

            return false;
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(_root);

            while (!reader.Name.Equals(_root))
            {
                string typeContent;
                Type underlyingType;
                var name = reader.Name;

                reader.MoveToAttribute("type");
                typeContent = reader.ReadContentAsString();
                underlyingType = Type.GetType(typeContent);
                reader.MoveToContent();
                _expando[name] = reader.ReadElementContentAs(underlyingType, null);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var key in _expando.Keys)
            {
                var value = _expando[key];

                writer.WriteStartElement(key);
                writer.WriteString(value.ToString());
                writer.WriteEndElement();
            }
        }

        public void Add(string key, object value)
        {
            if (IsFieldValid(key))
            {
                _expando.Add(key, value);
            }
        }

        public bool ContainsKey(string key)
        {
            return _expando.ContainsKey(key);
        }

        public ICollection<string> Keys => _expando.Keys;

        public bool Remove(string key)
        {
            return _expando.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _expando.TryGetValue(key, out value);
        }

        public ICollection<object> Values => _expando.Values;

        public object this[string key]
        {
            get => _expando[key];
            set
            {
                if (IsFieldValid(key))
                {
                    _expando[key] = value;
                }
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            if (IsFieldValid(item.Key))
            {
                _expando.Add(item);
            }
        }

        public void Clear()
        {
            _expando.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _expando.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _expando.CopyTo(array, arrayIndex);
        }

        public int Count => _expando.Count;

        public bool IsReadOnly => _expando.IsReadOnly;

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _expando.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _expando.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Verifica se o campo informado faz parte da entidade <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="fieldName">Campo a ser verificado.</param>
        /// <returns><see langword="true"/> se o campo for válido; <see langword="false"/> se não for.</returns>
        private bool IsFieldValid(string fieldName)
        {
            var validFields = typeof(TEntity).GetProperties().Select(p => p.Name);
            return validFields.Contains(fieldName);
        }
    }
}