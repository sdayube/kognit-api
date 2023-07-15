using System.ComponentModel.DataAnnotations;

namespace Kognit.API.Application.Parameters
{
    public enum Direction
    {
        ASC,
        DESC
    }

    public class QueryParameter : PagingParameter
    {
        [EnumDataType(typeof(Direction), ErrorMessage = "Direction must be either \"ASC\" or \"DESC\".")]
        public virtual string OrderDirection { get; set; }
        public virtual string OrderBy { get; set; }
        public virtual string Fields { get; set; }
    }
}