using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotonRx
{
    public struct FailureReason
    {
        public short ErrorCode { get; private set; }
        public string Message { get; private set; }

        public FailureReason(short errorCode, string message) : this()
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
