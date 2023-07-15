using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Kognit.API.Application.Parameters
{
    public enum Direction
    {
        ASC,
        DESC
    }

    public class QueryParameter : PagingParameter
    {
        private string _fields;

        [EnumDataType(typeof(Direction), ErrorMessage = "Direction must be either \"ASC\" or \"DESC\".")]
        public virtual string OrderDirection { get; set; }
        public virtual string OrderBy { get; set; }
        public virtual string Fields
        {
            get => _fields;
            set
            {
                var fieldList = value?.Split(',').Select(f => f.Trim()).ToList();

                if (!string.IsNullOrEmpty(value) && !fieldList.Contains("id", StringComparer.OrdinalIgnoreCase))
                {
                    _fields = "id," + value;
                }
                else
                {
                    _fields = value;
                }
            }
        }
    }
}