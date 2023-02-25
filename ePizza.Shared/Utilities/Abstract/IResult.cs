using ePizza.Shared.Utilities.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Shared.Utilities.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
